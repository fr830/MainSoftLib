using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Encryption
{
    public class EncryptionRijndael
    {
        public static string MasterKey { get; set; }
              

        public EncryptionRijndael(string Key)
        {
            MasterKey = Key;
        }

        /// <summary>
        /// Metodo para encriptar
        /// </summary>
        /// <param name="chain"></param>
        /// <returns></returns>
        public string Encrypt(string chain)
        {
            try
            {
                // Convierto la cadena y la clave en arreglos de bytes
                // para poder usarlas en las funciones de encriptacion
                byte[] cadenaBytes = Encoding.UTF8.GetBytes(chain);
                byte[] claveBytes = Encoding.UTF8.GetBytes(MasterKey);

                // Creo un objeto de la clase Rijndael
                RijndaelManaged rij = new RijndaelManaged();

                // Configuro para que utilice el modo ECB
                rij.Mode = CipherMode.ECB;

                // Configuro para que use encriptacion de 256 bits.
                rij.BlockSize = MasterKey.Length * 8;

                // Declaro que si necesitara mas bytes agregue ceros.
                rij.Padding = PaddingMode.Zeros;

                // Declaro un encriptador que use mi clave secreta y un vector
                // de inicializacion aleatorio
                ICryptoTransform encriptador;
                encriptador = rij.CreateEncryptor(claveBytes, rij.IV);

                // Declaro un stream de memoria para que guarde los datos
                // encriptados a medida que se van calculando
                MemoryStream memStream = new MemoryStream();

                // Declaro un stream de cifrado para que pueda escribir aqui
                // la cadena a encriptar. Esta clase utiliza el encriptador
                // y el stream de memoria para realizar la encriptacion
                // y para almacenarla
                CryptoStream cifradoStream;
                cifradoStream = new CryptoStream(memStream, encriptador, CryptoStreamMode.Write);

                // Escribo los bytes a encriptar. A medida que se va escribiendo
                // se va encriptando la cadena
                cifradoStream.Write(cadenaBytes, 0, cadenaBytes.Length);

                // Aviso que la encriptación se terminó
                cifradoStream.FlushFinalBlock();

                // Convert our encrypted data from a memory stream into a byte array.
                byte[] cipherTextBytes = memStream.ToArray();

                // Cierro los dos streams creados
                memStream.Close();
                cifradoStream.Close();

                // Convierto el resultado en base 64 para que sea legible
                // y devuelvo el resultado
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Metodo para desencriptar
        /// </summary>
        /// <param name="chain"></param>
        /// <returns></returns>
        public string Decrypt(string chain)
        {
            try
            {
                // Convierto la cadena y la clave en arreglos de bytes
                // para poder usarlas en las funciones de encriptacion
                // En este caso la cadena la convierta usando base 64
                // que es la codificacion usada en el metodo encriptar
                byte[] cadenaBytes = Convert.FromBase64String(chain);
                byte[] claveBytes = Encoding.UTF8.GetBytes(MasterKey);

                // Creo un objeto de la clase Rijndael
                RijndaelManaged rij = new RijndaelManaged();

                // Configuro para que utilice el modo ECB
                rij.Mode = CipherMode.ECB;

                // Configuro para que use encriptacion de 256 bits.
                rij.BlockSize = MasterKey.Length * 8;

                // Declaro que si necesitara mas bytes agregue ceros.
                rij.Padding = PaddingMode.Zeros;

                // Declaro un desencriptador que use mi clave secreta y un vector
                // de inicializacion aleatorio
                ICryptoTransform desencriptador;
                desencriptador = rij.CreateDecryptor(claveBytes, rij.IV);

                // Declaro un stream de memoria para que guarde los datos
                // encriptados
                MemoryStream memStream = new MemoryStream(cadenaBytes);

                // Declaro un stream de cifrado para que pueda leer de aqui
                // la cadena a desencriptar. Esta clase utiliza el desencriptador
                // y el stream de memoria para realizar la desencriptacion
                CryptoStream cifradoStream;
                cifradoStream = new CryptoStream(memStream, desencriptador, CryptoStreamMode.Read);

                // Declaro un lector para que lea desde el stream de cifrado.
                // A medida que vaya leyendo se ira desencriptando.
                StreamReader lectorStream = new StreamReader(cifradoStream);

                // Leo todos los bytes y lo almaceno en una cadena
                string result = lectorStream.ReadToEnd();

                result = result.Replace("\0", "");

                // Cierro los dos streams creados
                memStream.Close();
                cifradoStream.Close();

                // Devuelvo la cadena
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

       
    }
}
