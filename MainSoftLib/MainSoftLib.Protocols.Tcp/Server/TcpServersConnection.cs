using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Tcp.Server
{
    public class TcpServersConnection
    {
        public string To;
        public string From;
        public bool SnifferServer;
        internal string m_IdConection;

        private int attemptCount;
        private Encoding m_encoding;
        private DateTime m_lastVerifyTime;
        private TcpClient m_socket;
        private Thread m_thread_Call;
        private Thread m_threadMessage;
        private List<byte[]> messagesToSend;

        public TcpServersConnection(TcpClient Sock, Encoding Encoding)
        {
            this.m_socket = Sock;
            this.messagesToSend = new List<byte[]>();
            this.attemptCount = 0;
            this.m_lastVerifyTime = DateTime.UtcNow;
            this.m_encoding = Encoding;
        }

        private bool canStartNewThread()
        {
            return ((this.m_thread_Call == null) || (((this.m_thread_Call.ThreadState & (ThreadState.Aborted | ThreadState.Stopped)) != ThreadState.Running) && ((this.m_thread_Call.ThreadState & ThreadState.Unstarted) == ThreadState.Running)));
        }

        private bool canStartNewThreadMessage()
        {
            return ((this.m_threadMessage == null) || (((this.m_threadMessage.ThreadState & (ThreadState.Aborted | ThreadState.Stopped)) != ThreadState.Running) && ((this.m_threadMessage.ThreadState & ThreadState.Unstarted) == ThreadState.Running)));
        }

        public bool connected()
        {
            try
            {
                return this.m_socket.Connected;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void forceDisconnect()
        {
            lock (this.m_socket)
            {
                this.m_socket.Close();
            }
        }

        public bool hasMoreWork()
        {
            return ((this.messagesToSend.Count > 0) || ((this.Socket.Available > 0) && this.canStartNewThread()));
        }

        public bool processOutgoing(int maxSendAttempts)
        {
            lock (this.m_socket)
            {
                if (!this.m_socket.Connected)
                {
                    this.messagesToSend.Clear();
                    return false;
                }

                if (this.messagesToSend.Count == 0)
                {
                    return false;
                }

                NetworkStream stream = this.m_socket.GetStream();

                try
                {
                    stream.Write(this.messagesToSend[0], 0, this.messagesToSend[0].Length);

                    lock (this.messagesToSend)
                    {
                        this.messagesToSend.RemoveAt(0);
                    }

                    this.attemptCount = 0;
                }
                catch (IOException)
                {
                    this.attemptCount++;

                    if (this.attemptCount >= maxSendAttempts)
                    {
                        lock (this.messagesToSend)
                        {
                            this.messagesToSend.RemoveAt(0);
                        }

                        this.attemptCount = 0;
                    }
                }
                catch (ObjectDisposedException)
                {
                    this.m_socket.Close();
                    return false;
                }
            }
            return (this.messagesToSend.Count != 0);
        }

        public void sendData(string data)
        {
            byte[] bytes = this.m_encoding.GetBytes(data);

            lock (this.messagesToSend)
            {
                this.messagesToSend.Add(bytes);
            }
        }

        public void sendData(byte[] bytes)
        {
            lock (this.messagesToSend)
            {
                this.messagesToSend.Add(bytes);
            }
        }

        public bool verifyConnected()
        {
            bool flag = ((this.m_socket.Client.Available != 0) || !this.m_socket.Client.Poll(1, SelectMode.SelectRead)) || (this.m_socket.Client.Available != 0);
            this.m_lastVerifyTime = DateTime.UtcNow;
            return flag;
        }

        public Thread CallbackThread
        {
            get
            {
                return this.m_thread_Call;
            }
            set
            {
                if (!this.canStartNewThread())
                {
                    throw new Exception("Cannot override TcpServerConnection Callback Thread. The old thread is still running.");
                }

                this.m_thread_Call = value;
            }
        }

        public Thread CallbackThreadMessage
        {
            get
            {
                return this.m_threadMessage;
            }
            set
            {
                if (!this.canStartNewThreadMessage())
                {
                    throw new Exception("Cannot override TcpServerConnection Callback Thread. The old thread is still running.");
                }

                this.m_threadMessage = value;
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
                this.m_encoding = value;
            }
        }

        public string IdConection
        {
            get
            {
                return this.m_IdConection;
            }
        }

        public DateTime LastVerifyTime
        {
            get
            {
                return this.m_lastVerifyTime;
            }
        }

        public TcpClient Socket
        {
            get
            {
                return this.m_socket;
            }

            set
            {
                this.m_socket = value;
            }
        }
    }
}
