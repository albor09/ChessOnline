﻿using Chess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chess
{
    public class ChessClient
    {
        private TcpClient tcpClient;

        private Thread recieveMsgThread;

        public FigureColor myCololor;
        public bool IsConnected {get { return tcpClient != null && tcpClient.Connected; } }

        private string waitMsg;

        public async Task<bool> Connect(string ip)
        {
            try
            {
                tcpClient = new TcpClient();
                Console.WriteLine("Клиент запущен");
                await tcpClient.ConnectAsync(ip, 8888);

                if (!tcpClient.Connected)
                    return false;

                Console.WriteLine($"Подключение с {tcpClient.Client.RemoteEndPoint} установлено");

                //SendMessageToServer("Hello");
                recieveMsgThread = new Thread(ServerMessageHandler);
                recieveMsgThread.Start();
                return true;
            }
            catch
            {
                return false;
            }

        }

        private async void ServerMessageHandler()
        {
            try
            {
                var stream = tcpClient.GetStream();
                while (true)
                {
                    var buffer = new byte[1_024];
                    int received = await stream.ReadAsync(buffer, 0, 1024);

                    var message = Encoding.UTF8.GetString(buffer, 0, received);
                    waitMsg = message;
                    Console.WriteLine($"Message received: \"{message}\"");

                    if (message == "Started")
                    {
                        Board.Instance.SetDefaultPreset();
                        Board.Instance.turn = FigureColor.white;
                    }

                    if (message.Contains("Move"))
                    {
                        string[] splited = message.Split(' ');
                        (int, int) from = Helpers.NotationToCoordinates(splited[1]);
                        (int, int) to = Helpers.NotationToCoordinates(splited[2]);
                        var cellFrom = Board.Instance.cells[from.Item1][from.Item2];
                        var cellTo = Board.Instance.cells[to.Item1][to.Item2];
                        Board.Instance.Move(cellFrom, cellTo);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        private async Task<string> WaitForMessageResponse(string msg)
        {
            waitMsg = "";
            Console.WriteLine("WAITNULL");
            SendMessageToServer(msg);
            while (waitMsg == "")
            {
                await Task.Delay(25);
            }
            Console.WriteLine("WAITED");
            return waitMsg;
        }

        public async void SendMessageToServer(string msg)
        {
            try
            {
                var stream = tcpClient.GetStream();
                byte[] byteMsg = Encoding.UTF8.GetBytes(msg);
                await stream.WriteAsync(byteMsg, 0, byteMsg.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cant send message to server\nwith err: {e.Message}");
            }
        }

        public void CreateLobby(string name)
        {
            SendMessageToServer($"CreateLobby {name}");
            myCololor = FigureColor.white;
        }

        public void ConnectLobby(string name)
        {
            SendMessageToServer($"ConnectLobby {name}");
            myCololor = FigureColor.black;
        }

        public async Task<List<string>> GetLobbies()
        {
            string responce = await WaitForMessageResponse("GetLobbies");
            Console.WriteLine(responce);
            return new List<string>(responce.Split(' ')[1].Split('|'));
        }
    }
}

