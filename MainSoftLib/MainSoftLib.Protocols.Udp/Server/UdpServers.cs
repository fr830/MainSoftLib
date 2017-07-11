using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Udp.Server
{
    public class UdpServers : Component
    {
        private int m_port;
        private bool m_isOpen;

        private IContainer components;
        private Thread listenThread;
        private UdpClient Conexion;

        public event UdpServersConnectionChanged OnConnect;
        public event UdpServersConnectionChanged OnDisconect;
        public event UdpServersConnectionMessage OnMessage;
        public event UdpServersError OnError;

        public UdpServers(int Port)
        {
            m_port = Port;
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

        }

        [Browsable(false)]
        public bool IsOpen
        {
            get
            {
                return this.m_isOpen;
            }
            set
            {
                if (this.m_isOpen != value)
                {
                    if (value)
                    {
                        Connect();
                    }
                    else
                    {
                        Disconect();
                    }
                }
            }
        }

        private void InitializeComponent()
        {

        }

        /// <summary>
        /// It is used to connect to a udp client
        /// </summary>
        public void Connect()
        {
            lock (this)
            {
                if (!this.m_isOpen)
                {
                    if (this.m_port < 0)
                    {
                        throw new Exception("Invalid port");
                    }

                    try
                    {
                        Conexion = new UdpClient(m_port);

                        m_isOpen = true;

                        if (this.OnConnect != null)
                        {
                            this.OnConnect(Conexion);
                        }

                        this.listenThread = new Thread(new ThreadStart(this.runListener));
                        this.listenThread.Start();

                    }
                    catch (Exception ex)
                    {
                        m_isOpen = false;

                        if (this.OnError != null)
                        {
                            this.OnError(Conexion, ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// It is used to send a message a upd server
        /// </summary>
        /// <param name="Message"></param>
        public void Send(string Message, IPAddress IpAddress, int Port)
        {
            if (this.m_isOpen && (this.m_port >= 0))
            {
                try
                {
                    byte[] outStream = System.Text.Encoding.ASCII.GetBytes(Message);
                    Conexion.Send(outStream, outStream.Length, new IPEndPoint(IpAddress, Port));
                }
                catch (Exception ex)
                {
                    if (this.OnError != null)
                    {
                        this.OnError(Conexion, ex);
                    }
                }
            }
            else
            {
                throw new Exception("Invalid port");
            }
        }

        /// <summary>
        /// It is used to send a message a upd server
        /// </summary>
        /// <param name="Message"></param>
        public void Send(byte[] Data, IPAddress IpAddress, int Port)
        {
            if (this.m_isOpen && (this.m_port >= 0))
            {
                try
                {
                    Conexion.Send(Data, Data.Length, new IPEndPoint(IpAddress, Port));
                }
                catch (Exception ex)
                {
                    if (this.OnError != null)
                    {
                        this.OnError(Conexion, ex);
                    }
                }
            }
            else
            {
                throw new Exception("Invalid port");
            }
        }

        public void Disconect()
        {
            try
            {
                if (this.m_isOpen)
                {
                    lock (this)
                    {
                        this.m_isOpen = false;

                        Conexion.Close();

                        if (this.OnDisconect != null)
                        {
                            this.OnDisconect(Conexion);
                        }

                        //Conexion = null;
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.OnError != null)
                {
                    this.OnError(Conexion, ex);
                }
            }
        }

        private void runListener()
        {
            while (this.m_isOpen && (this.m_port >= 0))
            {
                try
                {
                    IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);

                    byte[] data = Conexion.Receive(ref remoteIPEndPoint);

                    if (data == null)
                    {
                        this.m_isOpen = false;
                    }
                    else
                    {
                        if (data.Length == 0)
                        {
                            this.m_isOpen = false;

                            if (OnDisconect != null)
                            {
                                this.OnDisconect(Conexion);
                            }
                        }
                        else
                        {
                            if (OnMessage != null)
                            {
                                this.OnMessage(Conexion, data);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    this.m_isOpen = false;
                }

                Thread.Sleep(100);
            }
        }
    }
}
