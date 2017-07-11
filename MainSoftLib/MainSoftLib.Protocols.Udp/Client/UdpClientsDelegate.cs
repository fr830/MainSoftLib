using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Udp.Client
{
    public delegate void UdpClientsConnectionChanged(UdpClient Connection);

    public delegate void UdpClientsConnectionMessage(UdpClient Connection, byte[] Data);

    public delegate void UdpClientsError(UdpClient Connection, Exception Ex);
}
