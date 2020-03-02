using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace ClientApp.Network.ServerSide
{
    public class Server : MonoBehaviour
    {
        public int MaxPlayers { get; private set; }
        public string IpAddress { get; private set; }
        public int Port { get; private set; }
        private TcpListener tcpListener;
        private SortedList<int, ClientManager> Clients = new SortedList<int, ClientManager>();

        private bool ServerIsUp = false;

        
        public async void StartServer(int maxPlayers, string ip, int port)
        {
            MaxPlayers = maxPlayers;
            IpAddress = ip;
            Port = port;
            ServerIsUp = true;

            Debug.Log("Starting server...");
            InitializeServerData();

            IPAddress _ip = IPAddress.Parse(IpAddress);
            tcpListener = new TcpListener(_ip, Port);
            tcpListener.Start();

            Debug.Log("Server started!");
            Debug.Log($"Listening port {Port}");

            while(ServerIsUp)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                HandleClient(client);
            }
        }



        private void HandleClient(TcpClient client)
        {
            Debug.Log($"Handling connection from {client.Client.RemoteEndPoint}");

            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (Clients[i].tcpClient == null)
                {
                    Clients[i].Connect(client);
                    return;
                }
            }
            Debug.LogError("Failed to connect: all slots are occupied");
        }



        private void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                Clients.Add(i, new ClientManager(i));
            }
        }
    }
}