using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MainSoftLib.Validation
{
    public class MethodValidationEmail
    {
        bool Email_Invalido = false;

        public bool IsValidEmail(string Correo)
        {
            Email_Invalido = false;
            if (String.IsNullOrEmpty(Correo))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                Correo = Regex.Replace(Correo, @"(@)(.+)$", this.DomainMapper, RegexOptions.None);
            }
            catch (Exception)
            {
                return false;
            }

            if (Email_Invalido)
            {
                return false;
            }

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(Correo, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$", RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;

            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                Email_Invalido = true;
            }

            return match.Groups[1].Value + domainName;
        }
    }
}
