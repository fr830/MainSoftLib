using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Encryption
{
    public class EncryptionSha1
    {
        /// Una cadena pseudoaleatoria de donde se generara la encriptacion. Puede ser de cualquier tamaño

        public static string Password { get; set; }
        public static string Salt { get; set; }
        public static string Hash { get; set; }
              
        public EncryptionSha1(string password, string salt, string hash)
        {
            Password = password;
            Salt = salt;
            Hash = hash;
        }

        /// <summary>
        ///  Encripta con el algoritmo SHA1
        /// </summary>
        /// <param name="PreCode">PreCode</param>
        /// <param name="Code">Code</param>
        /// <param name="Cadena">Cadena a encriptar</param>
        /// <returns>Cadena encriptada</returns>
        public string Encriptar(string Cadena)
        {
            try
            {
                // Generamos los arrays de Bytes de nuestras cadenas. Como iVectorInicial y iValor son cadenas
                // normales solo usamos Encoding.ASCII
                byte[] aVectorInicial = Encoding.ASCII.GetBytes(Hash);
                byte[] aValorRand = Encoding.ASCII.GetBytes(Salt);

                // Dado que cadena puede contener caracteres UNICODE, usaremos UTF8
                byte[] aCadena = Encoding.UTF8.GetBytes(Cadena);

                // Generemos la contraseña
                PasswordDeriveBytes cont = new PasswordDeriveBytes(Password, aValorRand, "SHA1", 2);

                // Obtengamos el array de la llave. Dividido en Bytes. (8 bits)
                byte[] aLlave = cont.GetBytes(256 / 8);

                // Usemos la clase Rijndael para la llave simetrica y usemos el modo Cipher Block Chaining (CBC)
                RijndaelManaged llaveSimetrica = new RijndaelManaged() { Mode = CipherMode.CBC };

                // Generemos el encriptador
                ICryptoTransform enc = llaveSimetrica.CreateEncryptor(aLlave, aVectorInicial);

                // Definamos donde tendremos los datos encriptados
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, enc, CryptoStreamMode.Write);

                // Encriptemos
                cs.Write(aCadena, 0, aCadena.Length);

                // Terminemos
                cs.FlushFinalBlock();

                // Bajemos nuestros datos encriptados a un array de bytes
                byte[] aCipher = ms.ToArray();

                // Liberar la memoria de nuestros datos encriptados
                ms.Close();
                cs.Close();

                // Regresmos nuestro dato encriptado como una cadena base64
                return Convert.ToBase64String(aCipher);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Descripta con el algoritmo TripleDES
        /// </summary>
        /// <param name="Cadena">Cadena a desencriptar</param>
        /// <returns>Cadena desencriptada</returns>
        public string Desencriptar(string Cadena)
        {
            try
            {
                // Generamos los arrays de Bytes de nuestras cadenas. Como iVectorInicial y iValor son cadenas
                // normales solo usamos Encoding.ASCII
                byte[] aVectorInicial = Encoding.ASCII.GetBytes(Hash);
                byte[] aValorRand = Encoding.ASCII.GetBytes(Salt);

                // Convertimos nuesta cadena encriptada (cipher) a un arrar
                byte[] aCipher = Convert.FromBase64String(Cadena);

                // Generemos la contraseña
                PasswordDeriveBytes cont = new PasswordDeriveBytes(Password, aValorRand, "SHA1", 2);

                // Obtengamos el array de la llave. Dividido en Bytes. (8 bits)
                byte[] aLlave = cont.GetBytes(256 / 8);

                // Usemos la clase Rijndael para la llave simetrica y usemos el modo Cipher Block Chaining (CBC)
                RijndaelManaged llaveSimetrica = new RijndaelManaged() { Mode = CipherMode.CBC };

                // Generemos el desencriptador
                ICryptoTransform desenc = llaveSimetrica.CreateDecryptor(aLlave, aVectorInicial);

                // Definamos donde tendremos los datos encriptados
                MemoryStream ms = new MemoryStream(aCipher);
                CryptoStream cs = new CryptoStream(ms, desenc, CryptoStreamMode.Read);

                // Definamos el arrar donde se colocaran nuestros datos desencriptados
                byte[] aCadena = new byte[aCipher.Length];

                // Comenzamos a desencriptar
                int tamB = cs.Read(aCadena, 0, aCadena.Length);

                // Liberemos la memoria
                ms.Close();
                cs.Close();

                // regresemos nuestra cadena desecriptada usando UTF8
                return Encoding.UTF8.GetString(aCadena, 0, tamB);
            }
            catch (Exception)
            {
                return null;
            }
        }

       
    }
}
