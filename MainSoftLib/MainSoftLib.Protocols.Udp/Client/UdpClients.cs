using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Udp.Client
{
    public class UdpClients : Component
    {
        private string m_IpAddress;
        private int m_port;
        private int m_port_listen;
        private bool m_isOpen;

        private IContainer components;
        private Thread listenThread;
        private UdpClient Conexion;

        public event UdpClientsConnectionChanged OnConnect;
        public event UdpClientsConnectionChanged OnDisconect;
        public event UdpClientsConnectionMessage OnMessage;
        public event UdpClientsError OnError;

        public UdpClients(string IpAddress, int Port)
        {
            try
            {
                m_IpAddress = IpAddress;
                m_port = Port;

                Conexion = new UdpClient();

                this.initialise();
            }
            catch (Exception)
            {

            }
        }

        public UdpClients(string IpAddress, int Port, int Port_Listen)
        {
            try
            {
                m_IpAddress = IpAddress;
                m_port = Port;
                m_port_listen = Port_Listen;

                this.initialise();
            }
            catch (Exception)
            {

            }
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
                        if (m_port_listen == 0)
                        {
                            Conexion = new UdpClient();
                        }
                        else
                        {
                            Conexion = new UdpClient(m_port_listen);
                        }

                        IPAddress Address = IPAddress.Parse(m_IpAddress);
                        Conexion.Connect(Address, m_port);

                        m_isOpen = true;

                        if (this.OnConnect != null)
                        {
                            this.OnConnect(Conexion);
                        }

                        if (m_port_listen != 0)
                        {
                            this.listenThread = new Thread(new ThreadStart(this.runListener));
                            this.listenThread.Start();
                        }
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
        /// It is used to disconnect the udp client
        /// </summary>
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

        /// <summary>
        /// It is used to send a message a upd client
        /// </summary>
        /// <param name="Message"></param>
        public void Send(string Message)
        {
            if (this.m_isOpen && (this.m_port >= 0))
            {
                try
                {
                    byte[] outStream = System.Text.Encoding.ASCII.GetBytes(Message);
                    Conexion.Send(outStream, outStream.Length);
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
        /// It is used to send a message a upd client
        /// </summary>
        /// <param name="Message"></param>
        public void Send(byte[] Data)
        {
            if (this.m_isOpen && (this.m_port >= 0))
            {
                try
                {
                    Conexion.Send(Data, Data.Length);
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
            }
        }
    }
}
