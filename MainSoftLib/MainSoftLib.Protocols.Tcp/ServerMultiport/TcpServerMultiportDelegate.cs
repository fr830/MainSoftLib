using MainSoftLib.Protocols.Tcp.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Tcp.ServerMultiport
{
    public delegate void TcpServersMultiportConnectionChanged(TcpServers Server, TcpServersConnection Connection);
    public delegate void TcpServersMultiportConnectionData(TcpServers Server, TcpServersConnection Connection, byte[] Data);
    public delegate void TcpServersMultiportConnectionMessage(TcpServers Server, TcpServersConnection Connection, string Message);
    public delegate void TcpServersMultiportConnectionError(TcpServers Server, Exception Ex);
    public delegate void TcpServersMultiportError(TcpServerMultiport Connection, Exception Ex);
}
