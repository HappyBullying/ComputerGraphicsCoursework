using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;

namespace ClientApp.Network.ServerSide
{
    public class ClientManager
    {
        public int ClientId { get; private set; }
        public string Username { get; private set; }
        public int DataBufferSize = 8192;
        public TcpClient tcpClient;
        private NetworkStream nstream;
        private byte[] receiveBuffer;

        public ClientManager(int clientId)
        {
            ClientId = clientId;
        }



        public async void Connect(TcpClient _client)
        {
            tcpClient = _client;
            tcpClient.ReceiveBufferSize = DataBufferSize;
            tcpClient.SendBufferSize = DataBufferSize;

            nstream = tcpClient.GetStream();
            receiveBuffer = new byte[DataBufferSize];
            
            try
            {
                while(nstream.CanRead && nstream.CanWrite)
                {
                    if (nstream.DataAvailable)
                    {
                        int bytesReceived = await nstream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);
                        byte[] data = new byte[bytesReceived];
                        Array.Copy(receiveBuffer, data, bytesReceived);
                        
                        string text = System.Text.Encoding.UTF8.GetString(data, 0, bytesReceived);
                        Debug.Log(text);
                    }
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Data);
            }
            finally
            {
                nstream.Close();
                _client.Close();
                _client = null;
            }
        }
    }
}