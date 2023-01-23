using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer
{
    public class LobbyMember
    {
        public readonly TcpClient tcpClient;
        public readonly FigureColor color;

        public LobbyMember(TcpClient tcpClient, FigureColor color)
        {
            this.tcpClient = tcpClient;
            this.color = color;
        }
    }

    public enum FigureColor
    {
        White,
        Black
    }
}
