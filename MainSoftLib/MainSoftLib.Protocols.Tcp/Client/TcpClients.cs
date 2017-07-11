using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Tcp.Client
{
    public class TcpClients : Component
    {
        public long IdClient;
        public long IdClientTo;
        private string m_IpAddress;
        private int m_port;
        private bool m_isOpen;
        private int m_bufferSize;

        private IContainer components;
        private Thread listenThread;
        private Thread sendThread;
        public TcpClient Conexion;

        TcpMethods Methods = new TcpMethods();

        public bool IsOpen { get { return m_isOpen; } }
        public object Tag { get; set; }

        public event TcpClientsConnectionChanged OnConnect;
        public event TcpClientsConnectionChanged OnDisconect;
        public event TcpClientsConnectionMessage OnMessage;
        public event TcpClientsConnectionData OnData;
        public event TcpClientsError OnError;

        public TcpClients(string IpAddress, int Port)
        {
            this.m_IpAddress = IpAddress;
            this.m_port = Port;

            this.initialise();
        }

        public TcpClients(string IpAddress, int Port, long IdClientTo)
        {
            this.m_IpAddress = IpAddress;
            this.m_port = Port;
            this.IdClientTo = IdClientTo;

            this.initialise();
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
            this.m_bufferSize = 4096;
        }

        private void InitializeComponent()
        {

        }

        /// <summary>
        /// It is used to connect to a tcp client
        /// </summary>
        public void Connect()
        {
            lock (this)
            {
                if (!this.m_isOpen)
                {
                    if (m_port <= 0)
                    {
                        if (this.OnError != null)
                        {
                            this.OnError(this, new Exception("Invalid port"));
                        }
                    }

                    if (Conexion == null)
                    {
                        Conexion = new TcpClient();
                    }

                    try
                    {
                        Conexion.Connect(m_IpAddress, m_port);

                        m_isOpen = true;

                        if (this.OnConnect != null)
                        {
                            this.OnConnect(this);
                        }

                        this.listenThread = new Thread(new ThreadStart(this.runListener));
                        this.listenThread.Start();
                    }
                    catch (Exception ex)
                    {
                        m_isOpen = false;

                        if (this.OnError != null)
                        {
                            this.OnError(this, ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// It is used to disconnect the tcp client
        /// </summary>
        public void Disconect()
        {
            try
            {
                lock (this)
                {
                    if (this.m_isOpen)
                    {
                        if (this.OnDisconect != null)
                        {
                            this.OnDisconect(this);
                        }
                    }

                    Conexion.Client.Shutdown(SocketShutdown.Both);
                    Conexion.Client.Disconnect(false);
                    Conexion.Close();
                    Conexion = null;

                    this.m_isOpen = false;
                }

                GC.Collect();
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
        /// It is used to send a message a tcp client
        /// </summary>
        /// <param name="Message"></param>
        public void Send(string Message)
        {
            if (this.m_isOpen && (this.m_port > 0))
            {
                try
                {
                    byte[] outStream = Encoding.ASCII.GetBytes(Message);
                    Conexion.Client.Send(outStream);
                }
                catch (Exception ex)
                {
                    if (this.OnError != null)
                    {
                        this.OnError(this, ex);
                    }
                }
            }
            else if (!this.m_isOpen)
            {
                throw new Exception("Conexion closed");
            }
            else if (this.m_port <= 0)
            {
                throw new Exception("Invalid port");
            }
        }

        /// <summary>
        /// It is used to send a datas a tcp client
        /// </summary>
        /// <param name="Message"></param>
        public void Send(byte[] Data)
        {
            if (this.m_isOpen && this.m_port > 0)
            {
                try
                {
                    Conexion.Client.Send(Data);
                }
                catch (Exception ex)
                {
                    if (this.OnError != null)
                    {
                        this.OnError(this, ex);
                    }
                }
            }
            else if (!this.m_isOpen)
            {
                throw new Exception("Conexion closed");
            }
            else if (this.m_port <= 0)
            {
                throw new Exception("Invalid port");
            }
        }

        private void runListener()
        {
            while (this.m_isOpen && (this.m_port >= 0))
            {
                try
                {
                    byte[] data = Methods.ReadStream(Conexion, m_bufferSize);

                    if (data == null)
                    {
                        Disconect();
                    }
                    else
                    {
                        if (data.Length == 0)
                        {
                            Disconect();
                        }
                        else
                        {
                            if (OnData != null)
                            {
                                try
                                {
                                    this.OnData.Invoke(this, data);
                                }
                                catch (Exception ex)
                                {
                                    if (this.OnError != null)
                                    {
                                        this.OnError(this, ex);
                                    }
                                }
                            }

                            if (OnMessage != null)
                            {
                                try
                                {
                                    string Message = Encoding.ASCII.GetString(data, 0, data.Length);
                                    this.OnMessage.Invoke(this, Message);
                                }
                                catch (Exception ex)
                                {
                                    if (this.OnError != null)
                                    {
                                        this.OnError(this, ex);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.m_isOpen = false;

                    if (this.OnError != null)
                    {
                        this.OnError(this, ex);
                    }
                }

                Thread.Sleep(100);
            }
        }

        public int BufferSize
        {
            get
            {
                return this.m_bufferSize;
            }

            set
            {
                m_bufferSize = value;
            }
        }
    }
}
