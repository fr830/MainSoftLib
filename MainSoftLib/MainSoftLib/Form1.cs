using MainSoftLib.Database.Sqlite;
using MainSoftLib.Protocols.Tcp.Server;
using MainSoftLib.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSoftLib
{
    public partial class Form1 : Form
    {
        static string DbRuta = "Data Source=H:\\prueba.db; Version=3; New=True; Compress=True;";

        TcpServers Server;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Server = new TcpServers(9001);

            Server.OnConnect += Server_OnConnect;
            Server.OnData += Server_OnData;
            Server.OnMessage += Server_OnMessage;
            Server.OnDisconect += Server_OnDisconect;

            Server.Open();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string Cedula = "21128030";
            string Url = "http://www.cne.gob.ve/web/registro_electoral/ce.php?nacionalidad=V&cedula=" + Cedula;

            string result = await WebMethods.GetResquest(Url, 10);

        }

        #region Server
        private void Server_OnConnect(TcpServersConnection Connection)
        {

        }

        private void Server_OnMessage(TcpServersConnection Connection, string Message)
        {
            if (true)
            {
                //ExtraerFosyga();
            }
        }

        private void Server_OnData(TcpServersConnection Connection, byte[] Data)
        {

        }

        private void Server_OnDisconect(TcpServersConnection Connection)
        {

        }

        #endregion
    }
}
