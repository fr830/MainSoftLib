using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Database.Sqlite
{
    public class SQLite
    {
        string connectionString;

        public SQLite(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataSet GetDataSet(string query)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        SQLiteDataAdapter data = new SQLiteDataAdapter(cmd);

                        DataSet ds = new DataSet();
                        data.Fill(ds);

                        cmd.Dispose();
                        connection.Close();

                        return ds;
                    }                    
                }
            }
            catch (Exception ex)
            {
                return null;        
            }
        }

        public DataTable GetDataTable(string query)
        {
            try
            {
                DataSet ds = GetDataSet(query);

                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string ExecuteNonQuery(string query)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        int resp = cmd.ExecuteNonQuery();
                        connection.Close();

                        return "OK:" + resp.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "ERROR:" + ex.Message;
            }
        }

        public string ExecuteExcalar(string query)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {                        
                        object resp = cmd.ExecuteScalar();
                        connection.Close();

                        return (resp == null ? "OK" : "OK:" + resp.ToString());
                    }                    
                }
            }
            catch (Exception ex)
            {
                return "ERROR:" + ex.Message;                
            }
        }

        
    }
}
