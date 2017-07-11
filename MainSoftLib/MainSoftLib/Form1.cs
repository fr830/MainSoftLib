using MainSoftLib.Database.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSoftLib
{
    public partial class Form1 : Form
    {
        static string DbRuta = "Data Source=H:\\prueba.db; Version=3; New=True; Compress=True;";

        SQLite MiDb = new SQLite(DbRuta);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string consulta = "select * from ciudad";
            string creacion = "CREATE TABLE ciudad3 (codigo VARCHAR(2) PRIMARY KEY, nombre VARCHAR(30));";

            //DataSet ds = MiDb.GetDataSet(consulta);

           string Resp = MiDb.ExecuteNonQuery(creacion);

        }
    }
}
