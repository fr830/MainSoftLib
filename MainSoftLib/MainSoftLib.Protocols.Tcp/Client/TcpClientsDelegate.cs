using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Tcp.Client
{
    public delegate void TcpClientsConnectionChanged(TcpClients Client);

    public delegate void TcpClientsConnectionData(TcpClients Client, byte[] Data);

    public delegate void TcpClientsConnectionMessage(TcpClients Client, string Message);

    public delegate void TcpClientsError(TcpClients Client, Exception Ex);
}
