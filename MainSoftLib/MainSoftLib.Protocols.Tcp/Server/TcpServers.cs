using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Tcp.Server
{
    public class TcpServers : Component
    {
        public bool SnifferMode;
        private int activeThreads;
        private object activeThreadsLock;
        private IContainer components = null;
        private List<TcpServersConnection> connections;
        private TcpListener listener;
        private Thread listenThread;
        private Encoding m_encoding;
        private int m_idleTime;
        private bool m_isOpen;
        private int m_maxCallbackThreads;
        private int m_maxSendAttempts;
        private string m_IpAddress;
        private int m_port;
        private int m_verifyConnectionInterval;
        private SemaphoreSlim sem;
        private Thread sendThread;
        private bool waiting;
        private int m_maxConexionClients;
        private int m_bufferSize;
        TcpMethods Methods = new TcpMethods();

        public event TcpServersMultiPortConnectionChanged OnConnectMultiPort;

        public event TcpServersConnectionChanged OnConnect;
        public event TcpServersConnectionChanged OnDisconect;
        public event TcpServersConnectionData OnData;
        public event TcpServersConnectionMessage OnMessage;
        public event TcpServersError OnError;

        public TcpServers()
        {
            this.activeThreadsLock = new object();
            this.InitializeComponent();
            this.initialise();
        }

        public TcpServers(int port)
        {
            this.activeThreadsLock = new object();
            this.InitializeComponent();
            this.initialise();
            this.Port = port;
        }

        public TcpServers(string ipAddress, int port)
        {
            this.activeThreadsLock = new object();
            this.InitializeComponent();
            this.initialise();
            this.IpAddress = ipAddress;
            this.Port = port;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void initialise()
        {
            this.connections = new List<TcpServersConnection>();
            this.listener = null;
            this.listenThread = null;
            this.sendThread = null;
            this.m_port = -1;
            this.m_maxSendAttempts = 3;
            this.m_isOpen = false;
            this.m_idleTime = 50;
            this.m_maxCallbackThreads = 100;
            this.m_verifyConnectionInterval = 100;
            this.m_encoding = Encoding.ASCII;
            this.sem = new SemaphoreSlim(0);
            this.waiting = false;
            this.activeThreads = 0;
            this.m_maxConexionClients = 1000;
            this.m_bufferSize = 4096;
        }

        private void InitializeComponent()
        {

        }

        /// <summary>
        /// Open the listen port on the tcp server
        /// </summary>
        public void Open()
        {
            lock (this)
            {
                if (!m_isOpen)
                {
                    if (m_port < 0)
                    {
                        OnError?.Invoke(this, new Exception("Invalid port"));
                    }
                    else
                    {
                        try
                        {
                            if (this.listener != null)
                                this.listener.Stop();

                            if (!string.IsNullOrWhiteSpace(this.m_IpAddress))
                            {
                                try
                                {
                                    IPAddress ip = IPAddress.Parse(this.m_IpAddress);
                                    this.listener = new TcpListener(ip, this.m_port);
                                }
                                catch
                                {
                                    throw new Exception("Ip format is invalid.");
                                }
                            }
                            else
                                this.listener = new TcpListener(IPAddress.Any, this.m_port);

                            this.listener.Start(5);

                            m_isOpen = true;
                            listenThread = new Thread(new ThreadStart(this.runListener));
                            listenThread.Start();
                            sendThread = new Thread(new ThreadStart(this.runSender));
                            sendThread.Start();
                        }
                        catch (Exception ex)
                        {
                            OnError?.Invoke(this, ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Close the listen port on the tcp server
        /// </summary>
        public void Close()
        {
            if (this.m_isOpen)
            {
                lock (this)
                {
                    this.m_isOpen = false;

                    foreach (TcpServersConnection connection in this.connections)
                    {
                        connection.forceDisconnect();
                    }

                    try
                    {
                        if (this.listenThread.IsAlive)
                        {
                            this.listenThread.Interrupt();
                            Thread.Yield();

                            if (this.listenThread.IsAlive)
                            {
                                this.listenThread.Abort();
                            }
                        }
                    }
                    catch (SecurityException ex)
                    {
                        this.OnError?.Invoke(this, ex);
                    }

                    try
                    {
                        if (this.sendThread.IsAlive)
                        {
                            this.sendThread.Interrupt();
                            Thread.Yield();

                            if (this.sendThread.IsAlive)
                            {
                                this.sendThread.Abort();
                            }
                        }
                    }
                    catch (SecurityException ex)
                    {
                        OnError?.Invoke(this, ex);
                    }
                }

                this.listener.Stop();

                lock (this.connections)
                {
                    this.connections.Clear();
                }

                this.listenThread = null;
                this.sendThread = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// Send a message to all connected tcp clients 
        /// </summary>
        /// <param name="data"></param>
        public void Send(string data)
        {
            try
            {
                lock (this.sem)
                {
                    foreach (TcpServersConnection connection in this.connections)
                    {
                        connection.sendData(data);
                    }

                    Thread.Yield();

                    if (this.waiting)
                    {
                        this.sem.Release();
                        this.waiting = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.OnError != null)
                {
                    this.OnError(this, ex);
                }
            }
        }

        /// <summary>
        /// Send a message to all connected tcp clients 
        /// </summary>
        /// <param name="data"></param>
        public void Send(string IdConection, string data)
        {
            try
            {
                lock (this.sem)
                {
                    TcpServersConnection connection = this.connections.FirstOrDefault(x => x.IdConection == IdConection);

                    if (connection != null)
                    {
                        connection.sendData(data);
                    }

                    Thread.Yield();

                    if (this.waiting)
                    {
                        this.sem.Release();
                        this.waiting = false;
                    }
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, ex);
            }
        }

        private bool processConnection(TcpServersConnection conn)
        {
            ThreadStart start = null;
            bool flag = false;

            if (conn.processOutgoing(this.m_maxSendAttempts))
            {
                flag = true;
            }

            if (((OnData != null || OnMessage != null) && (this.activeThreads < this.m_maxCallbackThreads)) && (conn.Socket.Available > 0))
            {
                lock (this.activeThreadsLock)
                {
                    this.activeThreads++;
                }

                if (start == null)
                {
                    start = delegate
                    {
                        byte[] data = Methods.ReadStream(conn.Socket, conn.Socket.ReceiveBufferSize);
                        string Message = Encoding.ASCII.GetString(data, 0, data.Length);

                        if (data != null)
                        {
                            if (conn.To != null)
                            {
                                List<TcpServersConnection> list_connection = connections.Where(x => x.From == conn.To).ToList();

                                if (list_connection != null)
                                {
                                    foreach (TcpServersConnection connection in list_connection)
                                    {
                                        connection.sendData(data);
                                    }
                                }
                                else
                                {
                                    OnError?.Invoke(this, new Exception(string.Format("The recipient {0} is not found.", conn.To)));
                                }
                            }

                            if (this.OnData != null && ((SnifferMode && conn.SnifferServer) || conn.To == null))
                            {
                                try
                                {
                                    OnData.Invoke(conn, data);
                                }
                                catch (Exception ex)
                                {
                                    OnError?.Invoke(this, ex);
                                }
                            }

                            if (this.OnMessage != null && (SnifferMode || conn.To == null))
                            {
                                try
                                {
                                    OnMessage.Invoke(conn, Message);
                                }
                                catch (Exception ex)
                                {
                                    OnError?.Invoke(this, ex);
                                }
                            }
                        }
                        else
                        {
                            OnError?.Invoke(this, new Exception("processConnection-Empty-Message"));
                        }
                    };
                }

                conn.CallbackThread = new Thread(start);
                conn.CallbackThread.Start();
                Thread.Yield();
            }

            return flag;
        }

        private void runListener()
        {
            while (m_isOpen && (m_port >= 0))
            {
                try
                {
                    if (this.listener.Pending())
                    {
                        TcpClient sock = listener.AcceptTcpClient();
                        TcpServersConnection conn = new TcpServersConnection(sock, m_encoding);

                        if (connections.Count < m_maxConexionClients)
                        {
                            lock (connections)
                            {
                                conn.m_IdConection = Guid.NewGuid().ToString();
                                connections.Add(conn);
                            }

                            if (OnConnect != null)
                            {
                                lock (activeThreadsLock)
                                {
                                    activeThreads++;
                                }

                                OnConnect?.Invoke(conn);
                            }

                            OnConnectMultiPort?.Invoke(this, conn);

                            continue;
                        }
                        else
                        {
                            byte[] outStream = Encoding.ASCII.GetBytes("$MaxConexionClients$");
                            conn.Socket.Client.Send(outStream);

                            this.OnError?.Invoke(this, new Exception("MaxConexionClients"));

                            conn.forceDisconnect();
                        }
                    }

                    Thread.Sleep(m_idleTime);
                    continue;
                }
                catch (ThreadInterruptedException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    if (m_isOpen && (OnError != null))
                    {
                        OnError(this, ex);
                    }

                    continue;
                }
            }
        }

        private void runSender()
        {
            while (this.m_isOpen && (m_port >= 0))
            {
                try
                {
                    bool flag = false;

                    for (int i = 0; i < connections.Count; i++)
                    {
                        if (connections[i].CallbackThread != null)
                        {
                            try
                            {
                                connections[i].CallbackThread = null;

                                lock (activeThreadsLock)
                                {
                                    activeThreads--;
                                }
                            }
                            catch (Exception ex)
                            {
                                OnError?.Invoke(this, ex);
                            }
                        }

                        if (this.connections[i].CallbackThread == null)
                        {
                            if (this.connections[i].connected() && ((connections[i].LastVerifyTime.AddMilliseconds((double)m_verifyConnectionInterval) > DateTime.UtcNow) || this.connections[i].verifyConnected()))
                            {
                                flag = flag || processConnection(connections[i]);
                            }
                            else
                            {
                                lock (connections)
                                {
                                    OnDisconect?.Invoke(connections[i]);

                                    connections.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                    }

                    if (flag)
                    {
                        continue;
                    }

                    Thread.Yield();

                    lock (this.sem)
                    {
                        foreach (TcpServersConnection connection in connections)
                        {
                            if (connection.hasMoreWork())
                            {
                                flag = true;
                                goto Label_0198;
                            }
                        }
                    }

                    Label_0198:
                    if (!flag)
                    {
                        waiting = true;
                        sem.Wait(m_idleTime);
                        waiting = false;
                    }

                    continue;
                }
                catch (ThreadInterruptedException)
                {
                    continue;
                }
                catch (Exception exception)
                {
                    if (m_isOpen && (OnError != null))
                    {
                        OnError(this, exception);
                    }

                    continue;
                }
            }
        }

        public void setEncoding(Encoding encoding, bool changeAllClients)
        {
            this.m_encoding = encoding;

            if (changeAllClients)
            {
                foreach (TcpServersConnection connection in connections)
                {
                    connection.Encoding = m_encoding;
                }
            }
        }

        public List<TcpServersConnection> Connections
        {
            get
            {
                List<TcpServersConnection> list = new List<TcpServersConnection>();
                list.AddRange(connections);
                return list;
            }
        }

        [Browsable(false)]
        public bool IsOpen
        {
            get
            {
                return m_isOpen;
            }
            set
            {
                if (this.m_isOpen != value)
                {
                    if (value)
                    {
                        Open();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
        }

        public int MaxConexionClients
        {
            get
            {
                return m_maxConexionClients;
            }

            set
            {
                m_maxConexionClients = value;
            }
        }

        public Encoding Encoding
        {
            get
            {
                return this.m_encoding;
            }
            set
            {
                Encoding encoding = m_encoding;
                m_encoding = value;

                foreach (TcpServersConnection connection in connections)
                {
                    if (connection.Encoding == encoding)
                    {
                        connection.Encoding = m_encoding;
                    }
                }
            }
        }

        public int MaxCallbackThreads
        {
            get
            {
                return m_maxCallbackThreads;
            }
            set
            {
                m_maxCallbackThreads = value;
            }
        }

        public int MaxSendAttempts
        {
            get
            {
                return m_maxSendAttempts;
            }
            set
            {
                m_maxSendAttempts = value;
            }
        }

        public string IpAddress
        {
            get
            {
                return m_IpAddress;
            }
            set
            {
                m_IpAddress = value;
            }
        }

        public int Port
        {
            get
            {
                return m_port;
            }

            set
            {
                if ((value >= 0) && (m_port != value))
                {
                    if (m_isOpen)
                    {
                        throw new Exception("Invalid attempt to change port while still open.\nPlease close port before changing.");
                    }

                    m_port = value;

                    if (listener == null)
                    {
                        if (!string.IsNullOrWhiteSpace(m_IpAddress))
                        {
                            try
                            {
                                IPAddress ip = IPAddress.Parse(m_IpAddress);
                                listener = new TcpListener(ip, m_port);
                            }
                            catch
                            {
                                throw new Exception("Ip format is invalid.");
                            }
                        }
                        else
                            listener = new TcpListener(IPAddress.Any, m_port);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(m_IpAddress))
                        {
                            try
                            {
                                IPAddress ip = IPAddress.Parse(m_IpAddress);
                                listener = new TcpListener(ip, m_port);
                            }
                            catch
                            {
                                throw new Exception("Ip format is invalid.");
                            }
                        }
                        else
                            listener.Server.Bind(new IPEndPoint(IPAddress.Any, this.m_port));
                    }
                }
            }
        }

        public int BufferSize
        {
            get
            {
                return m_bufferSize;
            }

            set
            {
                m_bufferSize = value;
            }
        }

        public int VerifyConnectionInterval
        {
            get
            {
                return m_verifyConnectionInterval;
            }
            set
            {
                this.m_verifyConnectionInterval = value;
            }
        }

        public int IdleTime
        {
            get
            {
                return m_idleTime;
            }
            set
            {
                this.m_idleTime = value;
            }
        }
    }
}
