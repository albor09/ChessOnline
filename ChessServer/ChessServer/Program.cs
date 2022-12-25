using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ChessServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessTcpServer server = new ChessTcpServer();
            Console.ReadKey();
        }

        
    }
}
