using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace ClientApp.Network.ServerSide
{
    public class Server : MonoBehaviour
    {
        /****************PROPERTIES****************/
        #region PROPERTIES
        private static object locker = new object();
        public static int MaxPlayers { get; private set; }
        public static string IpAddress { get; private set; }
        public static int Port { get; private set; }
        private static TcpListener tcpListener;
        private static SortedList<int, ClientObject> Clients = new SortedList<int, ClientObject>();

        public static bool ServerIsUp
        {
            get { return serverIsUp; }
            private set { lock(locker) { serverIsUp = value; }}
        }
        private static bool serverIsUp = false;
        #endregion
        /******************************************/



        public static async void StartServer(int maxPlayers, string ip, int port)
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


        private void SendTCPData(int clientId, Package pckg)
        {
            pckg.WriteRange();
            Clients[clientId].SendData(pckg);
        }

        private void SendTCPDataToAll(Package pckg)
        {
            pckg.WriteRange();
            for(int i = 1; i < MaxPlayers; i++)
            {
                if (Clients[i].IsActive)
                {
                    Clients[i].SendData(pckg);
                }
            }
        }

        private static void SendTCPDataToAll(int exceptId, Package pckg)
        {
            pckg.WriteRange();
            for (int i = 1; i < MaxPlayers; i++)
            {
                if (Clients[i].IsActive && i != exceptId)
                {
                    Clients[i].SendData(pckg);
                }
            }
        }



        private static void HandleClient(TcpClient client)
        {
            Debug.Log($"Handling connection from {client.Client.RemoteEndPoint}");

            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (!Clients[i].IsActive)
                {
                    Clients[i].Connect(client);
                    return;
                }
            }
            Debug.LogError("Failed to connect: all slots are occupied");
        }



        private static void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                Clients.Add(i, new ClientObject(i));
            }
        }
    }
}