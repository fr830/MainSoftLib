using System;
using System.Net.Sockets;

namespace MainSoftLib.Protocols.Udp.Server
{
    public delegate void UdpServersConnectionChanged(UdpClient Connection);

    public delegate void UdpServersConnectionMessage(UdpClient Connection, byte[] Data);

    public delegate void UdpServersError(UdpClient Connection, Exception Ex);
}
