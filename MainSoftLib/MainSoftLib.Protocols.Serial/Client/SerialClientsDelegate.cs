using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Serial.Client
{
    public delegate void SerialClientsConnectionChanged(SerialPort Connection);

    public delegate void SerialClientsConnectionMessage(SerialPort Connection, string Message);

    public delegate void SerialClientsError(SerialPort Connection, Exception Ex);

    public delegate void SerialClientsPinChangedEventArgs(SerialPort Connection, SerialPinChangedEventArgs Ex);

    public delegate void SerialClientsErrorReceivedEventArgs(SerialPort Connection, SerialErrorReceivedEventArgs Ex);
}
