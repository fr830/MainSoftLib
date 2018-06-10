using MainSoftLib.Logs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSoftLib.Web
{
    public class WebMethods
    {
        static MethodsLogs Log = new MethodsLogs("WebMethods.log");


        public static string GetResquest(string Url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
                    client.Encoding = Encoding.UTF8;

                    byte[] response = client.DownloadData(Url);
                    return Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                return null;
            }
        }

        public static string GetResquest(string Url, CookieContainer Cookie)
        {
            try
            {
                using (CookieWebClient client = new CookieWebClient(Cookie))
                {
                    client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
                    client.Encoding = Encoding.UTF8;

                    byte[] response = client.DownloadData(Url);
                    return Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception ex)
            {              
                Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                return null;
            }
        }

        public static string PostResquest(string Url, NameValueCollection PostData)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
                    client.Encoding = Encoding.UTF8;

                    byte[] response = client.UploadValues(Url, PostData);
                    return Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                return null;
            }
        }

        public static string PostResquest(string Url, NameValueCollection PostData, CookieContainer Cookie)
        {
            try
            {
                using (CookieWebClient client = new CookieWebClient(Cookie))
                {
                    client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
                    client.Encoding = Encoding.UTF8;

                    byte[] response = client.UploadValues(Url, PostData);
                    return Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                return null;
            }
        }

        /// <summary>
        /// GetResquest
        /// </summary>
        /// <param name="Url">Url</param>
        /// <param name="TimeOut">Sec</param>
        /// <returns></returns>
        public static Task<string> GetResquest(string Url, int TimeOut)
        {
            Task<string> task = Task.Run(() =>
            {
                string CodigoTexto = null;

                try
                {
                    using (WebBrowser Navegador = new WebBrowser())
                    {
                        EventWaitHandle WaitRequest = new EventWaitHandle(false, EventResetMode.AutoReset);

                        Navegador.Tag = WaitRequest;
                        Navegador.DocumentCompleted += Navegador_DocumentCompleted;

                        Navegador.Navigate(Url);
                        WaitRequest.WaitOne(TimeOut * 1000); // Espera Maximo 30sg

                        Navegador.Invoke((MethodInvoker)delegate
                        {
                            CodigoTexto = (Navegador != null && Navegador.Document != null) ? Navegador.Document.Body.InnerHtml : null;
                        });

                        return CodigoTexto;
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                    return CodigoTexto;
                }
            });
            return task;
        }

        private static void Navegador_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser Nav = sender as WebBrowser;

            if (Nav!=null)
            {
                EventWaitHandle WaitRequest = (Nav.Tag as EventWaitHandle);

                if (WaitRequest!=null)
                {
                    WaitRequest.Set();
                }
            }
        }

        public static bool ObtenerArchivo(string UrlCapcha, string PathCapcha)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(UrlCapcha, PathCapcha);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                return false;
            }
        }

        public static List<string> BuscarEtiquetas(string Etiqueta, bool Cierre, string Texto)
        {
            try
            {
                List<string> Resultado = new List<string>();

                string pattern = "";

                if (Cierre)
                {
                    pattern = "(<)(" + Etiqueta + ").*?>.*?(<).*?(" + Etiqueta + ")(>)";
                }
                else
                {
                    pattern = "(<)(" + Etiqueta + ").*?>";
                }

                MatchCollection matches = Regex.Matches(Texto, pattern);

                foreach (Match item in matches)
                {
                    Resultado.Add(item.Value);
                }

                return Resultado;
            }
            catch (Exception ex)
            {
                Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                return null;
            }
        }

        public static List<string> BuscarEtiqueta(string Etiqueta, string Texto)
        {
            try
            {
                List<string> Resultado = new List<string>();
                string pattern = pattern = "(<)(" + Etiqueta + ").*?.*?(<).*?(" + Etiqueta + ")(>)";

                MatchCollection matches = Regex.Matches(Texto, pattern);

                foreach (Match item in matches)
                {
                    Resultado.Add(item.Value);
                }

                return Resultado;
            }
            catch (Exception ex)
            {
                Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                return null;
            }
        }

        public static List<string> ObtenerValor_Etiquetas(string Texto)
        {
            try
            {
                string pattern = "(>).*?(<)";
                List<string> Lista = new List<string>();

                MatchCollection matches = Regex.Matches(Texto, pattern);

                foreach (Match item in matches)
                {
                    string Temp = item.Value;
                    Temp = Temp.Remove(0, 1);
                    Temp = Temp.Remove(Temp.Length - 1, 1);
                    Temp = Temp.Trim();
                    Lista.Add(Temp);
                }

                return Lista;
            }
            catch (Exception ex)
            {
                Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                return null;
            }
        }

        public static List<string> ObtenerValor_Etiqueta_Indiv(string Etiqueta, string Texto)
        {
            try
            {
                string pattern = "(" + Etiqueta + ").*(/>)";
                List<string> Lista = new List<string>();

                MatchCollection matches = Regex.Matches(Texto, pattern);

                foreach (Match item in matches)
                {
                    string Temp = item.Value;
                    Temp = Temp.Replace(Etiqueta + "=\"", "");
                    Temp = Temp.Replace("\" />", "");
                    Lista.Add(Temp);
                }

                return Lista;
            }
            catch (Exception ex)
            {
                Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                return null;
            }
        }

        public static List<string> ObtenerValor_Etiqueta_IndivConCierre(string Etiqueta, string Texto)
        {
            try
            {
                string pattern = "(" + Etiqueta + ").*?(" + Etiqueta + ")(>)";
                List<string> Lista = new List<string>();

                MatchCollection matches = Regex.Matches(Texto, pattern);

                foreach (Match item in matches)
                {
                    string Temp = item.Value;
                    Temp = Temp.Replace(Etiqueta + "=\"", "");
                    Temp = Temp.Replace("\" />", "");
                    Lista.Add(Temp);
                }

                return Lista;
            }
            catch (Exception ex)
            {
                Log.WriteLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, TypeLog.Error, ex);
                return null;
            }
        }
    }
}
