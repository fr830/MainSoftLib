using MainSoftLib.Logs;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace MainSoftLib.Database.MySql
{
    public class MethodsMySql
    {
        private MethodsLogs _log = new MethodsLogs("MainSoftLib.Database.MySql.log");

        public string ConnectionString { get; set; }

        public MethodsMySql(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public object ExecuteExcalar(string query)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    object Resp = null;

                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandTimeout = 900000; // timeout de la conexion
                        Resp = cmd.ExecuteScalar(); // Execute command
                        cmd.Dispose();
                    }

                    connection.Close();

                    return Resp;
                }
            }
            catch (Exception ex)
            {
                _log.WriteLog(ex);
                return null;                
            }            
        }

        public bool ExecuteNonQuery(string query)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    bool Resp = false;

                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 4 * 60 * 1000; // timeout de la conexion

                        Resp = cmd.ExecuteNonQuery() > 0 ? true : false;

                        return Resp;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.WriteLog(ex);
                return false;
            }            
        }

        public DataSet GetDataSet(string query)
        {
            try
            {
                DataSet ds = null;

                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandTimeout = 900000; // timeout de la conexion
                        cmd.CommandType = CommandType.Text;

                        MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd); // Obtener columnas de la tabla

                        ds = new DataSet();

                        adaptador.Fill(ds, "Tabla"); // Volcamos los resultados en el Dataset
                    }
                }

                return ds;
            }
            catch (Exception ex)
            {
                _log.WriteLog(ex);
                return null;
            }
        }
    }
}
