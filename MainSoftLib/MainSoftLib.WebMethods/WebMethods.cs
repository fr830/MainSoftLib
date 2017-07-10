using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MainSoftLib.Web
{
    public class WebMethods
    {
        public static string ObtenerHtml_GetResquest(string Url, CookieContainer Cookie)
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
            catch (Exception)
            {
                return null;
            }
        }

        public static string ObtenerHtml_PostResquest(string Url, NameValueCollection PostData, CookieContainer Cookie)
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
                return null;
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
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
                return null;
            }
        }
    }
}
