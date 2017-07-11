using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainSoftLib.Extension
{
    public static class ExtensionConversion
    {
        #region String

        /// <summary>
        /// Quita solos espacios en Blanco al Inicio, al Final y las espacios adicionales Intermedios.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringNorma(this string value)
        {
            string Resp = "";

            string[] Texto = value.Trim().Split(' ');

            for (int i = 0; i < Texto.Length; i++)
            {
                if (Texto[i] != "")
                {
                    Resp += Texto[i] + " ";
                }
            }

            return Resp.Trim();
        }

        /// <summary>
        /// Convierte a un DateTime en una Cadena en Formato "yyyy-MM-dd HH:mm:ss". (Usar para DB)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortDateTimeString(this DateTime value)
        {
            try
            {
                return value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string ToShortDateTimeStartString(this DateTime value)
        {
            try
            {
                return value.ToString("yyyy-MM-dd " + "00:00:00", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Convierte a un DateTime en una Cadena en Formato "yyyy-MM-dd". (Usar para DB)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortDateStringDB(this DateTime value)
        {
            try
            {
                return value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Convierte a un DateTime en una Cadena en Formato "HH:mm:ss". (Usar para DB)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortTimeStringDB(this DateTime value)
        {
            try
            {
                return value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Convierte a un DateTime en una Cadena en Formato "yyyy-MM-dd HH:mm:ss". (Usar para DB)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDateTimeStringDB(this DateTime value)
        {
            try
            {
                return value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Convierte a un DateTime en una Cadena en Formato "yyyy-MM-dd HH:mm:ss". (Usar para DB)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDateTimeStringDB(this DateTime? value)
        {
            try
            {
                return (value == null ? null : value.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Convierte a un DateTime en una Cadena en Formato "yyyy-MM-dd HH:mm:ss". (Usar para DB)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDate(this DateTime? value)
        {
            try
            {
                return (value == null ? null : value.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        ///  Convierte a un DateTime en una Cadena en Formato "dd-MM-yyyy HH:mm:ss".
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortDateTimeStringCo(this DateTime value)
        {
            return value.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///  Convierte a un DateTime en una Cadena en Formato "dd-MM-yyyy HH:mm:ss".
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortDateTimeStringCo(this DateTime? value)
        {
            return (value == null ? null : value.Value.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
        }


        /// <summary>
        /// Convierte a un DateTime en una Cadena en Formato "MM-dd-yyyy HH:mm:ss".
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortDateTimeString2(this DateTime value)
        {
            return value.ToString("MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static string ToShortDateTimeFF9String(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd ", CultureInfo.InvariantCulture) + value.TimeOfDay;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableAgeString(this TimeSpan span)
        {
            return string.Format("{0:0}", span.Days / 365.25);
        }

        /// <summary>
        /// Convierte un TimeSpan a una Cadena con Formato
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} Day{1}, ", span.Days, span.Days == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} Hour{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} Minute{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} Second{1}", span.Seconds, span.Seconds == 1 ? String.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", "))
                formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted))
                formatted = "0 Seconds";

            return formatted;
        }

        /// <summary>
        /// Convierte la Cadena al Formato Tipo Titulo
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFormat(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        public static string ToReadString(this TimeSpan span)
        {
            string formatted;

            try
            {
                formatted = string.Format("{0}{1}{2}{3}",
                                span.Duration().Days > 0 ? string.Format("{0}D, ", span.Days) : string.Empty,
                                span.Duration().Hours > 0 || span.Duration().Days > 0 ? string.Format("{0:00}:", span.Hours) : string.Empty,
                                span.Duration().Minutes > 0 || span.Duration().Hours > 0 ? string.Format("{0:00}:", span.Minutes) : string.Empty,
                                span.Duration().Seconds > 0 || span.Duration().Minutes > 0 ? string.Format("{0:00}", span.Seconds) : "00");

                if (formatted.EndsWith(", "))
                    formatted = formatted.Substring(0, formatted.Length - 2);

                if (string.IsNullOrEmpty(formatted))
                    formatted = "00";
            }
            catch (Exception)
            {
                formatted = "00";
            }

            return formatted;
        }

        /// <summary>
        /// Convierte un Entero Boleano (0,1) a una Cadena Boleana (N,Y)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToInt32SN(this int value)
        {
            if (value == 1)
            {
                return "Y";
            }
            else if (value == 0)
            {
                return "N";
            }
            else //Otros
            {
                return "N";
            }

        }

        /// <summary>
        /// Convierte una Objet a un string Nulable.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringNull(this object value)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }
                else
                {
                    return value.ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Convierte una Objet a un string Nulable DB.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringNullDB(this object value)
        {
            try
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    return "NULL";
                }
                else
                {
                    return "\"" + value.ToString() + "\"";
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Convierte una Objet a un string Nulable.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringNoNull(this object value)
        {
            try
            {
                if (value == null)
                {
                    return "";
                }
                else
                {
                    return value.ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string[] ToStringArray(this object value)
        {
            var collection = value as System.Collections.IEnumerable;

            if (collection != null)
            {
                return collection
                  .Cast<object>()
                  .Select(x => x.ToString())
                  .ToArray();
            }

            if (value == null)
            {
                return new string[] { };
            }

            return new string[] { value.ToString() };
        }

        #endregion

        #region Int

        /// <summary>
        /// Convierte una Objet a un Int.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt32(this object value)
        {
            Int32 Num;
            try
            {
                Num = Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                Num = 0;
            }
            return Num;
        }

        /// <summary>
        /// Convierte una Cadena a un Entero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt32(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Convierte una Cadena a un Entero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? ToInt32Null(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Convierte un Double a un Entero.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt32(this double value)
        {
            int Num = 0;
            try
            {
                Num = Convert.ToInt32(value);
            }
            catch (Exception)
            {

            }
            return Num;
        }

        /// <summary>
        /// Convertir un Entero a horas string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHourString(this int value)
        {
            try
            {
                int horas = (value / 60);
                int minutos = (value - (horas * 60));
                return horas + ":" + minutos + ":" + "00";
            }
            catch (Exception)
            {
                return "01:00:00";
            }
        }


        /// <summary>
        /// Convierte un Decimal a un Entero.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt32(this decimal value)
        {
            int Num = 0;
            try
            {
                Num = Convert.ToInt32(value);
            }
            catch (Exception)
            {

            }
            return Num;
        }

        /// <summary>
        /// Convierte una Cadena Boleana (N,No,Y,Yes) a un Entero Boleano (0,1);
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToSNInt32(this string value)
        {
            if (value == "y" || value == "Y" || value == "Yes" || value == "YES")
            {
                return 1;
            }
            else if (value == "y" || value == "N" || value == "No" || value == "NO")
            {
                return 0;
            }
            else //Otros
            {
                return -1;
            }
        }

        /// <summary>
        /// Convierte un Booleano a Entero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt32(this bool value)
        {
            return Convert.ToInt32(value);
        }

        #endregion

        #region Double 

        /// <summary>
        /// Convierte una Cadena a un Double.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(this string value)
        {
            double Num;
            try
            {
                Num = Convert.ToDouble(value);
            }
            catch (Exception)
            {
                Num = 0;
            }
            return Num;
        }

        /// <summary>
        /// Convierte una Objet a un Double.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(this object value)
        {
            double Num;
            try
            {
                Num = Convert.ToDouble(value);
            }
            catch (Exception)
            {
                Num = 0;
            }
            return Num;
        }

        /// <summary>
        /// Convierte un Entero a Double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(this int value)
        {
            return Convert.ToDouble(value);
        }

        #endregion

        #region Decimal

        /// <summary>
        /// Convierte una Objet a un Decimal.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object value)
        {
            decimal Num;
            try
            {
                Num = Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                Num = 0;
            }
            return Num;
        }

        /// <summary>
        /// Convierte una Cadena a un Decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value)
        {
            decimal Num;

            try
            {
                Num = Convert.ToDecimal(value.ToString());
            }
            catch (Exception)
            {
                Num = 0;
            }
            return Num;
        }

        /// <summary>
        /// Convierte un Entero a Decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this int value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                return 0;
            }

        }

        #endregion

        #region Long

        /// <summary>
        /// Convierte una Fecha en formato DateTime a UxiTimestamp
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(this DateTime target)
        {
            try
            {
                var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
                return Convert.ToInt64((target - date).TotalMilliseconds);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static long ToLong(this object value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #region Bool

        /// <summary>
        /// Convierte un String Entero a Booleano
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Convierte un Entero a Booleano
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this int value)
        {
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// Convierte un Objeto a Booleano
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region DateTime

        /// <summary>
        /// Convierte una fecha en formato UxiTimestamp a DateTime
        /// </summary>
        /// <param name="target">Hora Universal Cordinada (UTC)</param>
        /// <param name="UnixTimestamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this DateTime target, long UnixTimestamp)
        {
            TimeZoneInfo Zona = TimeZoneInfo.Local;
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            dateTime = dateTime.AddSeconds(UnixTimestamp);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, Zona);
        }

        /// <summary>
        /// Convierte una Cadena con Formato "yyyy-MM-dd HH:mm:ss" a un DateTime.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            DateTime parsedDate = new DateTime();
            try
            {
                if (value.Length == 0)
                {
                    parsedDate = new DateTime();
                }
                else
                {
                    if (value.Contains("-"))
                    {
                        DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out parsedDate);
                    }
                    else if (value.Contains("/"))
                    {
                        DateTime.TryParseExact(value, "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out parsedDate);
                    }
                    else
                    {
                        parsedDate = new DateTime();
                    }

                    if (parsedDate == default(DateTime))
                    {
                        parsedDate = Convert.ToDateTime(value);
                    }
                }
            }
            catch (Exception ex)
            {
                parsedDate = new DateTime();
            }
            return parsedDate;
        }

        /// <summary>
        /// Convierte una Cadena con Formato "yyyy-MM-dd HH:mm:ss" a un DateTime.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeNull(this string value)
        {
            try
            {
                DateTime parsedDate = new DateTime();

                if (value.Length == 0)
                {
                    parsedDate = new DateTime();
                }
                else
                {
                    if (value.Contains("-"))
                    {
                        DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out parsedDate);
                    }
                    else if (value.Contains("/"))
                    {
                        DateTime.TryParseExact(value, "yyyy/MM/dd HH:mm:ss", null, DateTimeStyles.None, out parsedDate);
                    }
                    else
                    {
                        parsedDate = new DateTime();
                    }

                    if (parsedDate == default(DateTime))
                    {
                        parsedDate = Convert.ToDateTime(value);
                    }
                }

                return parsedDate;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///  Convierte una Cadena con Formato "HH:mm:ss" a un DateTime.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToTime(this string value)
        {
            DateTime parsedDate;
            string Sistema = DateTime.Now.ToString("dd/MM/yyyy");

            try
            {
                DateTime.TryParseExact(Sistema + " " + value, "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out parsedDate);
            }
            catch (Exception)
            {
                parsedDate = new DateTime();
            }
            return parsedDate;
        }

        /// <summary>
        /// Convierte una Cadena con Formato a un DateTime.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeAll(this string value)
        {
            DateTime parsedDate;
            try
            {
                parsedDate = Convert.ToDateTime(value);
            }
            catch (Exception)
            {
                parsedDate = new DateTime();
            }
            return parsedDate;
        }

        #endregion

        #region Color

        /// <summary>
        /// Convierte un Color en un String en el formato R-G-B
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToRBG(this Color value)
        {
            try
            {
                return value.R + "-" + value.G + "-" + value.B;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Convierte un String en el formato R-G-B a un Color
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color ToColor(this string value)
        {
            try
            {
                string[] Sep = value.Split('-');

                if (Sep.Count() == 3)
                {
                    return Color.FromArgb(Sep[0].ToInt32(), Sep[1].ToInt32(), Sep[2].ToInt32());
                }
                else if (value != "")
                {
                    return Color.FromArgb(value.ToInt32());
                }
                else
                {
                    return Color.FromArgb(0, 0, 0);
                }
            }
            catch (Exception)
            {
                return Color.FromArgb(0, 0, 0);
            }
        }

        public static Color GetDarkerColor(this Color color)
        {
            try
            {
                int increase = 60;

                int r = ((color.R - increase < 0) ? 0 : color.R - increase);
                int g = ((color.G - increase < 0) ? 0 : color.G - increase);
                int b = ((color.B - increase < 0) ? 0 : color.B - increase);

                return Color.FromArgb(r, g, b);
            }
            catch (Exception)
            {
                return color;
            }
        }

        public static Color GetLigthColor(this Color color)
        {
            try
            {
                int increase = 80;

                int r = ((color.R + increase > 255) ? 255 : color.R + increase);
                int g = ((color.G + increase > 255) ? 255 : color.G + increase);
                int b = ((color.B + increase > 255) ? 255 : color.B + increase);

                return Color.FromArgb(r, g, b);
            }
            catch (Exception)
            {
                return color;
            }
        }

        public static Color GetLigthColorSwitch(this Color color)
        {
            try
            {
                int increase = 120;

                int r = ((color.R + increase > 255) ? 255 : color.R + increase);
                int g = ((color.G + increase > 255) ? 255 : color.G + increase);
                int b = ((color.B + increase > 255) ? 255 : color.B + increase);

                return Color.FromArgb(r, g, b);
            }
            catch (Exception)
            {
                return color;
            }
        }

        public static Color GetLigthColorButton(this Color color)
        {
            try
            {
                int increase = 30;

                int r = ((color.R + increase > 255) ? 255 : color.R + increase);
                int g = ((color.G + increase > 255) ? 255 : color.G + increase);
                int b = ((color.B + increase > 255) ? 255 : color.B + increase);

                return Color.FromArgb(r, g, b);
            }
            catch (Exception)
            {
                return color;
            }
        }

        public static Color GetDarkerColor1(this Color color)
        {
            try
            {
                int increase = 60;

                int r = ((color.R - increase < 0) ? 0 : color.R + increase);
                int g = ((color.G - increase < 0) ? 0 : color.G + increase);
                int b = ((color.B - increase < 0) ? 0 : color.B + increase);

                return Color.FromArgb(r, g, b);
            }
            catch (Exception)
            {
                return color;
            }
        }

        #endregion

        #region Byte 

        /// <summary>
        /// Método que convierte el mensaje de string a Bytes
        /// </summary>
        /// <param name="Message">Mensaje que se va a convertir a Bytes</param>
        /// <returns>Retorna una lista de bytes con el mensaje</returns>
        public static byte[] ToByte(this string Message)
        {
            byte[] NuevoMensaje = null;
            try
            {
                ASCIIEncoding Convertir = new ASCIIEncoding();
                NuevoMensaje = Convertir.GetBytes(Message);

            }
            catch { NuevoMensaje = null; }
            return NuevoMensaje;
        }

        #endregion
    }
}
