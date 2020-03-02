using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;

namespace ClientApp.Network.ClientSide
{
    public class Client : MonoBehaviour
    {
        public string Ip { get; private set; }
        public int Port { get; private set; }
        public int SelfId { get; private set; }
        public string Username { get; private set; }
        public TCPManager tcp;


        public Client(string ip, int port, int bufferSize)
        {
            Ip = ip;
            Port = port;
            tcp = new TCPManager(ip, port, bufferSize);
        }

        public void ConnectToServer()
        {
            tcp.Connect();
        }
    }
}