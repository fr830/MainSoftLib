using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Logs
{
    public class MethodsLogs
    {
        string fileName;

        public MethodsLogs(string _fileName)
        {
            fileName = _fileName;
        }

        public void WriteLog(string logText)
        {
            try
            {
                using (StreamWriter w = File.AppendText(fileName))
                {
                    w.WriteLine(DateTime.Now.ToString() + " - " + logText);
                }
            }
            catch
            {

            }
        }

        public void WriteLog(Exception ex)
        {
            try
            {
                using (StreamWriter w = File.AppendText(fileName))
                {
                    w.WriteLine("--------------------------------------------------------------------------------");
                    w.WriteLine(DateTime.Now.ToString() + " - EXCEPCION");
                    w.WriteLine("Message: " + ex.Message);
                    w.WriteLine("Source: " + ex.Source);
                    w.WriteLine("TargetSite: " + ex.TargetSite);
                    w.WriteLine("StackTrace: " + ex.StackTrace);
                    w.WriteLine("InnerException: " + ex.InnerException);
                    w.WriteLine("--------------------------------------------------------------------------------");
                }
            }
            catch
            {

            }
        }

        public void WriteLog(string Caller, string Method, TypeLog Type, Exception ex)
        {
            try
            {
                using (StreamWriter w = File.AppendText(fileName))
                {
                    w.WriteLine("--------------------------------------------------------------------------------");
                    w.WriteLine("DateTime: " + DateTime.Now.ToString());
                    w.WriteLine("Type: " + Type.ToString());
                    w.WriteLine("Caller: " + Caller);
                    w.WriteLine("Method: " + Method);
                    w.WriteLine("Message: " + ex.Message);
                    w.WriteLine("Source: " + ex.Source);
                    w.WriteLine("TargetSite: " + ex.TargetSite);
                    w.WriteLine("StackTrace: " + ex.StackTrace);
                    w.WriteLine("InnerException: " + ex.InnerException);
                    w.WriteLine("--------------------------------------------------------------------------------");
                }
            }
            catch
            {

            }
        }

        public void WriteLog(string Caller, string Method, TypeLog Type, string ex)
        {
            try
            {
                using (StreamWriter w = File.AppendText(fileName))
                {
                    w.WriteLine("--------------------------------------------------------------------------------");
                    w.WriteLine("DateTime: " + DateTime.Now.ToString());
                    w.WriteLine("Type: " + Type.ToString());
                    w.WriteLine("Caller: " + Caller);
                    w.WriteLine("Method: " + Method);
                    w.WriteLine("Message: " + ex);
                    w.WriteLine("--------------------------------------------------------------------------------");
                }
            }
            catch
            {

            }
        }
    }

    public enum TypeLog
    {
        None,
        Error,
        Info
    };
}
