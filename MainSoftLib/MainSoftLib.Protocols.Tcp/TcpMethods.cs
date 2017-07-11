using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Tcp
{
    public class TcpMethods
    {
        public byte[] ReadStream(TcpClient client, int bufferSize)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] myReadBuffer = new byte[bufferSize];

                //while (stream.DataAvailable)
                //{                    
                int numberOfBytesRead = stream.Read(myReadBuffer, 0, bufferSize); //call stream.Read(), read until end of packet/stream/other termination indicator
                return myReadBuffer.ToList().GetRange(0, numberOfBytesRead).ToArray(); //return data read as byte array
                //}
            }
            catch (Exception) { }

            return null;
        }
    }
}
