﻿using MainSoftLib.Protocols.Serial.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Serial
{
    public class SerialClients : Component
    {
        private string m_port;
        private bool m_isOpen;

        private int m_BaudRate;
        private Parity m_Parity;
        private StopBits m_StopBits;
        private int m_DataBits;
        private Handshake m_Handshake;
        private bool m_RtsEnable;

        private IContainer components;
        private Thread listenThread;
        private Thread sendThread;
        private SerialPort Conexion;

        public event SerialClientsConnectionChanged OnConnect;
        public event SerialClientsConnectionChanged OnDisconect;
        public event SerialClientsConnectionMessage OnMessage;
        public event SerialClientsConnectionData OnData;
        public event SerialClientsError OnError;
        public event SerialClientsPinChangedEventArgs OnPinChanged;
        public event SerialClientsErrorReceivedEventArgs OnErrorReceived;

        public SerialClients(string Port)
        {
            this.initialise();

            m_port = Port;

            Conexion = new SerialPort(m_port);
            Conexion.DataReceived += new SerialDataReceivedEventHandler(Conexion_DataReceived);
            Conexion.PinChanged += new SerialPinChangedEventHandler(Conexion_PinChanged);
            Conexion.ErrorReceived += new SerialErrorReceivedEventHandler(Conexion_ErrorReceived);

            Conexion.BaudRate = m_BaudRate;
            Conexion.Parity = m_Parity;
            Conexion.StopBits = m_StopBits;
            Conexion.DataBits = m_DataBits;
            Conexion.Handshake = m_Handshake;
            Conexion.RtsEnable = m_RtsEnable;
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
            this.m_BaudRate = 9600;
            this.m_Parity = Parity.None;
            this.m_StopBits = StopBits.One;
            this.m_DataBits = 8;
            this.m_Handshake = Handshake.None;
            this.m_RtsEnable = true;
        }

        private void InitializeComponent()
        {

        }

        /// <summary>
        /// It is used to connect to a serial port
        /// </summary>
        public void Connect()
        {
            lock (this)
            {
                if (!this.m_isOpen)
                {
                    if (this.m_port == "")
                    {
                        throw new Exception("Invalid port");
                    }

                    try
                    {
                        Conexion.Open();

                        m_isOpen = true;

                        this.OnConnect?.Invoke(Conexion);
                    }
                    catch (Exception ex)
                    {
                        m_isOpen = false;

                        this.OnError?.Invoke(Conexion, ex);
                    }
                }
            }
        }

        /// <summary>
        /// It is used to disconnect the serial port
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

                        this.OnDisconect?.Invoke(Conexion);

                        //Conexion = null;
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
                this.OnError?.Invoke(Conexion, ex);
            }
        }

        /// <summary>
        /// It is used to send a message serial port
        /// </summary>
        /// <param name="Message"></param>
        public void Send(string Message)
        {
            if (this.m_isOpen)
            {
                try
                {
                    Conexion.DiscardOutBuffer();
                    Conexion.WriteLine(Message);
                }
                catch (Exception ex)
                {
                    this.OnError?.Invoke(Conexion, ex);
                }
            }
        }

        /// <summary>
        ///  It is used to send a data a serial port
        /// </summary>
        /// <param name="Data"></param>
        public void Send(byte[] Data)
        {
            if (this.m_isOpen)
            {
                try
                {
                    Conexion.DiscardOutBuffer();
                    Conexion.Write(Data, 0, Data.Length);
                }
                catch (Exception ex)
                {
                    this.OnError?.Invoke(Conexion, ex);
                }
            }
        }

        [Browsable(false)]
        public bool IsOpen
        {
            get
            {
                return this.m_isOpen;
            }
        }

        void Conexion_PinChanged(object sender, SerialPinChangedEventArgs ex)
        {
            this.OnPinChanged?.Invoke(sender as SerialPort, ex);
        }

        void Conexion_ErrorReceived(object sender, SerialErrorReceivedEventArgs ex)
        {
            OnErrorReceived?.Invoke(sender as SerialPort, ex);
        }

        void Conexion_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (OnMessage != null || OnData != null)
                {
                    Thread.Sleep(250);

                    if (Conexion.BytesToRead > 0)
                    {
                        int count = Conexion.BytesToRead;
                        byte[] Data = new byte[count];

                        Conexion.Read(Data, 0, count);

                        string Message = Encoding.ASCII.GetString(Data, 0, Data.Length);

                        OnData?.Invoke(Conexion, Data);
                        OnMessage?.Invoke(Conexion, Message);
                    }
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(Conexion, ex);            
            }
        }        
    }
}
