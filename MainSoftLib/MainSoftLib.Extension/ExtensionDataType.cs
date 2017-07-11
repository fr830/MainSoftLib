using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Extension
{
    public static class ExtensionDataType
    {
        private static char[] HexDigits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };


        public static byte[] StringToByteArray(this string hex)
        {
            try
            {
                return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string[] ObjectToStringArray(this object arg)
        {
            var collection = arg as System.Collections.IEnumerable;
            if (collection != null)
            {
                return collection
                  .Cast<object>()
                  .Select(x => x.ToString())
                  .ToArray();
            }

            if (arg == null)
            {
                return new string[] { };
            }

            return new string[] { arg.ToString() };
        }

        public static List<byte> StringArregloByte(this string cadena)
        {
            String[] tokens = cadena.Split(' '); // asumo que los bytes estan separados por espacio
            List<byte> bytes = new List<byte>();

            for (int i = 0; i < tokens.Length; i++)
            {
                bytes.Add(Convert.ToByte(tokens[i], 16));
            }

            return bytes;
        }


        public static string EB_String(this int entero)
        {
            //La máscara y el # de iteraciones
            const uint mascara = 0x80000000;
            const int iteraciones = 32;
            //el contador y el resultado
            int contador = 0;
            string resultado = "";

            //Se recorren los 32 bit
            while (contador++ < iteraciones)
            {
                /*Si el entero and la mascara = 0 quiere decir
                 *que el bit 1 esta apagado*/
                if ((entero & mascara) == 0)
                    resultado += "0";
                else
                    resultado += "1";

                /*correr un bit a la izquierda para poner
                 *el siguiente bit en la posicion del primero*/
                entero = entero << 1;
            }
            return resultado;
        }


        public static string IntToBinary(byte b, byte size)
        {
            try
            {
                return Convert.ToString(b, 2).PadLeft(size, '0');
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string IntToHex(this byte[] bytes)
        {
            try
            {
                char[] chArray = new char[bytes.Length * 2];

                for (int i = 0; i < bytes.Length; i++)
                {
                    int num2 = bytes[i];
                    chArray[i * 2] = HexDigits[num2 >> 4];
                    chArray[(i * 2) + 1] = HexDigits[num2 & 15];
                }
                return new string(chArray);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string IntToHex(this byte b)
        {
            try
            {
                return (HexDigits[b >> 4].ToString() + HexDigits[b & 15].ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string IntToHex(byte[] bytes, int index, int count)
        {
            try
            {
                char[] chArray = new char[count * 2];

                for (int i = 0; i < count; i++)
                {
                    int num2 = bytes[index + i];
                    chArray[i * 2] = HexDigits[num2 >> 4];
                    chArray[(i * 2) + 1] = HexDigits[num2 & 15];
                }

                return new string(chArray);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static byte BinaryToInt(this string s)
        {
            try
            {
                return Convert.ToByte(s, 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string BinaryToDecimal(this string n)
        {
            try
            {
                return Convert.ToString(Convert.ToInt32(n.ToString(), 2));
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static byte[] HexToInt(this string s)
        {
            try
            {
                byte[] buffer = new byte[s.Length / 2];

                for (int i = 0; i < (s.Length / 2); i++)
                {
                    string str = s.Substring(i * 2, 2);
                    buffer[i] = Convert.ToByte(str, 0x10);
                }

                return buffer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string HexToBinary(this string Text)
        {
            try
            {
                return Convert.ToString(Convert.ToInt32(Text, 16), 2);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static bool IsHexDigit(this char c)
        {
            try
            {
                char ch = char.ToUpper(c);

                foreach (char ch2 in HexDigits)
                {
                    if (ch == ch2)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsHexString(this string s)
        {
            try
            {
                if (s.Length == 0)
                {
                    return false;
                }

                for (int i = 0; i < s.Length; i++)
                {
                    if (!IsHexDigit(s[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
