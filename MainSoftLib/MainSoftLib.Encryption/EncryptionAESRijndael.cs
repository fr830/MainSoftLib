using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Encryption
{
    public class EncryptionAESRijndael
    {
        public static string MasterKey { get; set; }
        public static string MasterIV { get; set; }

        public EncryptionAESRijndael()
        {

        }

        public EncryptionAESRijndael(string Key, string IV)
        {
            MasterKey = Key;
            MasterIV = IV;
        }

        public string Encrypt(string inputText)
        {
            byte[] _key = Encoding.ASCII.GetBytes(MasterKey);
            byte[] _iv = Encoding.ASCII.GetBytes(MasterIV);
            byte[] inputBytes = Encoding.ASCII.GetBytes(inputText);
            byte[] encripted;

            RijndaelManaged cripto = new RijndaelManaged();

            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(_key, _iv), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }

        public string Decrypt(string inputText)
        {
            byte[] _key = Encoding.ASCII.GetBytes("12EstaClave34es56dificil489ssswf");
            byte[] _iv = Encoding.ASCII.GetBytes("Devjoker7.37hAES");
            byte[] inputBytes = Convert.FromBase64String(inputText);
            byte[] resultBytes = new byte[inputBytes.Length];
            string textoLimpio = String.Empty;
            RijndaelManaged cripto = new RijndaelManaged();

            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(_key, _iv), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(objCryptoStream, true))
                    {
                        textoLimpio = sr.ReadToEnd();
                    }
                }
            }
            return textoLimpio;
        }
    }
}
