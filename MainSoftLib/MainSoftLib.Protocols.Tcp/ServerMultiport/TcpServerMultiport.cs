using MainSoftLib.Protocols.Tcp.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Tcp.ServerMultiport
{
    public class TcpServerMultiport : Component
    {
        private bool m_isOpen;
        private IContainer components = null;

        public event TcpServersMultiportConnectionChanged OnConnect;
        public event TcpServersMultiportConnectionChanged OnDisconect;
        public event TcpServersMultiportConnectionData OnData;
        public event TcpServersMultiportConnectionMessage OnMessage;
        public event TcpServersMultiportConnectionError OnError;

        public event TcpServersMultiportError OnErrorServerMultiPort;

        List<TcpServers> ListServer = new List<TcpServers>();

        public TcpServerMultiport(List<int> PortList)
        {
            if (PortList != null && PortList.Count > 0)
            {
                for (int i = 0; i < PortList.Count; i++)
                {
                    TcpServers Server = new TcpServers(PortList[i]);
                    Server.OnConnect += Server_OnConnect;
                    Server.OnData += Server_OnData;
                    Server.OnMessage += Server_OnMessage;
                    Server.OnError += Server_OnError;
                    //Server.OnDisconect += Server_OnDisconect;

                    ListServer.Add(Server);
                }
            }
        }

        private void initialise()
        {
            m_isOpen = false;
        }

        public void Open()
        {
            lock (this)
            {
                if (!m_isOpen)
                {
                    if (ListServer != null && ListServer.Count > 0)
                    {
                        try
                        {
                            for (int i = 0; i < ListServer.Count; i++)
                            {
                                ListServer[i].Open();
                            }

                            m_isOpen = true;
                        }
                        catch (Exception ex)
                        {
                            m_isOpen = false;
                            OnErrorServerMultiPort?.Invoke(this, ex);

                            Close();
                        }
                    }
                }
            }
        }


        public void Close()
        {
            if (this.m_isOpen)
            {
                lock (this)
                {
                    this.m_isOpen = false;

                    if (ListServer != null && ListServer.Count > 0)
                    {
                        for (int i = 0; i < ListServer.Count; i++)
                        {
                            try
                            {
                                ListServer[i].Close();
                            }
                            catch (Exception ex)
                            {
                                this.OnError?.Invoke(ListServer[i], ex);
                            }
                        }

                        GC.Collect();
                    }
                }
            }
            else
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

        #region Eventos

        private void Server_OnConnect(TcpServersConnection Connection)
        {

        }

        private void Server_OnData(TcpServersConnection Connection, byte[] Data)
        {
            //this.OnData?.Invoke(Connection, Data);
        }

        private void Server_OnMessage(TcpServersConnection Connection, string Message)
        {
            //this.OnMessage?.Invoke(Connection, Message);
        }

        private void Server_OnError(TcpServers Connection, Exception Ex)
        {
            this.OnError?.Invoke(Connection, Ex);
        }

        private void Server_OnDisconect(TcpServers Server, TcpServersConnection Connection)
        {
            this.OnDisconect?.Invoke(Server, Connection);
        }

        #endregion

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
                        this.Open();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }
    }
}
