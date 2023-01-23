using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer
{
    class CommandHandler
    {
        private static Commands c;
        private static List<MethodInfo> commands;

        public static void InitCommandHandler(ChessTcpServer server)
        {
            c = new Commands(server);
            commands = new List<MethodInfo>(c.GetType().GetMethods());
        }
        public static void Handler(string msg, TcpClient sender)
        {
            string[] splited = msg.Split(' ');
            string[] args = splited.Length == 2 ? splited[1].Split('&'): new string[] { };
            string command = splited[0];
            foreach (var method in commands)
            {
                foreach (var attr in method.GetCustomAttributes(false))
                {
                    if (attr is ClientCommandAttribute commandAttribute)
                    {
                        if (commandAttribute.Command == command)
                        {
                            var describedMethodParams = method.GetParameters();
                            object[] methodParams = new object[describedMethodParams.Length];
                            methodParams[0] = sender;
                            if (describedMethodParams.Length > 1)
                            {
                                for (int i = 1; i < describedMethodParams.Length; i++)
                                {
                                    if (describedMethodParams[i].ParameterType.Name == "String")
                                        methodParams[i] = args[i-1];
                                    else if (describedMethodParams[i].ParameterType.Name == "Int")
                                        methodParams[i] = int.Parse(args[i-1]);
                                }
                            }
                            method.Invoke(c, methodParams);
                            return;
                        }
                    }
                }
            }
            Console.WriteLine($"not handled message: {msg}");
        }
    }


    public class Commands
    {
        private ChessTcpServer _server;
        public Commands(ChessTcpServer server)
        {
            _server = server;
        }

        [ClientCommand("Move")]
        public void Move(TcpClient sender, string from, string to)
        {
            var lob = _server.Lobbies.Find(x => x.player1.tcpClient == sender || x.player2.tcpClient == sender);
            lob.MoveHandler(sender, from, to);
        }

        [ClientCommand("GetLobbies")]
        public void GetLobbies(TcpClient sender)
        {
            Console.WriteLine("Lol");
            _server.SendMessageToClient(sender, $"Lobbies {string.Join("|", _server.GetAvailableLobbies())}");
        }

        [ClientCommand("CreateLobby")]
        public void CreateLobby(TcpClient sender, string name)
        {
            _server.CreateLobby(sender, name);
        }

        [ClientCommand("ConnectLobby")]
        public void ConnectLobby(TcpClient sender, string name)
        {
            var lob = _server.Lobbies.Find(x => x.lobbyName == name);
            if (lob.isStarted)
                return;
            lob.SecondPlayerConnect(sender);
        }
    }

    public class ClientCommandAttribute : Attribute
    {
        public string Command { get; }  
        public ClientCommandAttribute(string command) { Command = command; }

    }
}
