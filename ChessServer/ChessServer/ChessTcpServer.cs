using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessServer
{
    class ChessTcpServer
    {
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();

        private Thread connectionHandler;

        private List<Thread> messageHandlers = new List<Thread>();

        private List<GameLobby> lobbies = new List<GameLobby>();
        public ChessTcpServer()
        {
            StartServer();
            Console.ReadKey();
        }

        private void StartServer()
        {
            server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            Console.WriteLine("Server has been started on " + IPAddress.Any.ToString());
            connectionHandler = new Thread(HandleClientConnection);
            connectionHandler.Start();
        }

        private async void HandleClientConnection()
        {
            while (true)
            {
                var tcpClient = await server.AcceptTcpClientAsync();
                Console.WriteLine($"Входящее подключение: {tcpClient.Client.RemoteEndPoint}");
                clients.Add(tcpClient);

                Thread msgHandler = new Thread(() => HandleMessagesFromClient(tcpClient));
                msgHandler.Name = tcpClient.Client.RemoteEndPoint.ToString();
                msgHandler.Start();
                messageHandlers.Add(msgHandler);

            }
        }

        private async void HandleMessagesFromClient(TcpClient client)
        {
            try
            {
                var stream = client.GetStream();
                while (true)
                {
                    var buffer = new byte[1_024];
                    int received = await stream.ReadAsync(buffer, 0, 1024);

                    var message = Encoding.UTF8.GetString(buffer, 0, received);
                    Console.WriteLine($"Message received: \"{message}\" from: {client.Client.RemoteEndPoint}");

                    if (message.Contains("CreateLobby"))
                    {
                        CreateLobby(client, message.Split(' ')[1]);
                    }
                    else if (message == "GetLobbies")
                    {
                        SendMessageToClient(client, $"Lobbies {string.Join("|", lobbies.FindAll(x => !x.isStarted).Select(x => x.lobbyName).ToArray())}");
                    }
                    else if (message.Contains("ConnectLobby"))
                    {
                        string name = message.Split(' ')[1];
                        var lob = lobbies.Find(x => x.lobbyName == name);
                        if (lob.isStarted)
                            return;
                        lob.SecondPlayerConnect(client);
                    }
                    else if (message.Contains("Move"))
                    {
                        var lob = lobbies.Find(x => x.player1.tcpClient == client || x.player2.tcpClient == client);
                        string[] splited = message.Split(' ');
                        lob.MoveHandler(client, splited[1], splited[2]);
                    }
                    else if (message.Contains("Result"))
                    {

                    }
                }
            }
            catch
            {
                Console.WriteLine(client.Client.RemoteEndPoint + " Has been disconnected");
                clients.Remove(client);
            }

        }

        public async void SendMessageToClient(TcpClient client, string msg)
        {
            try
            {
                var stream = client.GetStream();
                byte[] byteMsg = Encoding.UTF8.GetBytes(msg);
                await stream.WriteAsync(byteMsg, 0, byteMsg.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cant send message to {client.Client.RemoteEndPoint}\nwith err: {e.Message}");
            }
        }

        public void CreateLobby(TcpClient creator, string name)
        {
            GameLobby newLobby = new GameLobby(this, creator, name, lobbies.Count);
            Console.WriteLine($"Created Lobby {name} by {creator.Client.RemoteEndPoint}");
            lobbies.Add(newLobby);
        }
    }
}
