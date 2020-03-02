using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace ClientApp.Network.ClientSide
{
    public class TCPManager
    {
        public ConcurrentQueue<string> msgs = new ConcurrentQueue<string>();
        public TcpClient client;
        private NetworkStream nstream;
        private bool ClientOnline = false;
        private byte[] receiveBuffer;
        public int DataBufferSize { get; set; }
        public string IpAddress { get; private set; }
        public int Port { get; set; }

        public TCPManager(string ip, int port, int bfSize)
        {
            IpAddress = ip;
            Port = port;
            DataBufferSize = bfSize;
            receiveBuffer = new byte[DataBufferSize];
        }




        public async void Connect()
        {
            client = new TcpClient
            {
                ReceiveBufferSize = DataBufferSize,
                SendBufferSize = DataBufferSize
            };
            
            ClientOnline = true;

            try
            {
                await client.ConnectAsync(IpAddress, Port);

                while(!client.Connected)
                {
                    continue;
                }
                
                nstream = client.GetStream();

                while(ClientOnline && nstream.CanWrite)
                {
                    if (nstream.DataAvailable)
                    {
                        // Read Input Data
                        receiveBuffer = new byte[DataBufferSize];
                        int bytesRead = await nstream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);
                        byte[] data = new byte[bytesRead];
                        Array.Copy(receiveBuffer, data, bytesRead);
                    }

                    if (msgs.Count > 0)
                    {
                        // Write Data
                        string toSend = "A@";
                        for (int i = 0; i < 10; i++)
                        {
                            if (msgs.Count <= 0)
                                break;
                            string tmp = "";
                            msgs.TryDequeue(out tmp);
                            toSend += tmp;
                        }
                        byte[] msg = System.Text.Encoding.UTF8.GetBytes(toSend);
                        await nstream.WriteAsync(msg, 0, msg.Length);
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