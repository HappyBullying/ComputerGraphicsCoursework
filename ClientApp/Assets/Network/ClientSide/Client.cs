using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace ClientApp.Network.ClientSide
{
    public class Client : MonoBehaviour
    {
        /****************PROPERTIES****************/
        #region PROPERTIES
        private object locker = new object();
        public string Ip { get; private set; }
        public int SelfId { get; private set; }
        public string Username { get; private set; }
        private TcpClient client;
        private Package receivedPackage;
        private NetworkStream nstream;
        public bool ClientOnline
        {
            get { return clientOnline; }
            private set { lock(locker) { clientOnline = value; }}
        }
        private bool clientOnline = false;
        private byte[] receiveBuffer;
        public int DataBufferSize { get; private set; }
        public string IpAddress { get; private set; }
        public int Port { get; private set; }
        #endregion
        /******************************************/
        



        public Client(string ip, int port, int bufferSize)
        {
            Ip = ip;
            Port = port;
            receivedPackage = new Package();
        }

        public void ConnectToServer()
        {
            Connect();
        }


        private bool HandleData(byte[] data)
        {
            int packageLength = 0;
            receivedPackage.SetBytes(data);

            if (receivedPackage.UnreadLength >= 4)
            {
                packageLength = receivedPackage.ReadInt();
                if (packageLength <= 0)
                {
                    return true;
                }
            }

            while(packageLength > 0 && packageLength <= receivedPackage.UnreadLength)
            {
                byte[] packageBytes = receivedPackage.ReadBytes(packageLength);
            }
        }



        public void Connect()
        {
            client = new TcpClient
            {
                ReceiveBufferSize = DataBufferSize,
                SendBufferSize = DataBufferSize
            };
            ClientOnline = true;

            try
            {
                client.Connect(IpAddress, Port);
                
                nstream = client.GetStream();
                while(ClientOnline)
                {
                    if (nstream.CanRead && nstream.DataAvailable)
                    {
                        // Read Incoming Data
                        receiveBuffer = new byte[DataBufferSize];
                        int bytesRead = nstream.Read(receiveBuffer, 0, receiveBuffer.Length);
                        byte[] data = new byte[bytesRead];
                        Array.Copy(receiveBuffer, data, bytesRead);
                        receivedPackage.Reset(HandleData(data));
                    }

                    if (nstream.CanWrite)
                    {
                        // Send to server
                        Package[] packages = TwoWayBuffer.GetAllFromSendSequence();
                        for (int i = 0; i < packages.Length; i++)
                        {
                            nstream.Write(packages[i].ToArray(), 0, packages[i].Length);
                            //packages.Dispose();
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Data);
                client.Close();
            }
        }
    }
}