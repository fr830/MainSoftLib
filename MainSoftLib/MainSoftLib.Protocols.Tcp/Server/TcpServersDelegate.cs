using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Protocols.Tcp.Server
{
    public delegate void TcpServersMultiPortConnectionChanged(TcpServers Server, TcpServersConnection Connection);
    public delegate void TcpServersConnectionChanged(TcpServersConnection Connection);
    public delegate void TcpServersConnectionData(TcpServersConnection Connection, byte[] Data);
    public delegate void TcpServersConnectionMessage(TcpServersConnection Connection, string Message);
    public delegate void TcpServersError(TcpServers Server, Exception Ex);
}
