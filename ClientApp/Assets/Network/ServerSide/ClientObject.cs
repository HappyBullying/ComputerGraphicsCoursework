using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using ClientApp.Network;

namespace ClientApp.Network.ServerSide
{
    public class ClientObject
    {
        /****************PROPERTIES****************/
        #region PROPERTIES
        public int ClientId { get; private set; }
        public string Username { get; private set; }
        public int DataBufferSize = 8192;
        public bool IsActive { get; private set;}
        private TcpClient tcpClient;
        private NetworkStream nstream;
        private byte[] receiveBuffer;
        #endregion
        /******************************************/




        /*****************METHODS******************/
        #region METHODS
        public ClientObject(int clientId)
        {
            ClientId = clientId;
        }
        


        public void SendData(Package pckg)
        {
            // make async??????
            try
            {
                if (nstream.CanWrite)
                {
                    nstream.Write(pckg.ToArray(), 0, pckg.Length);
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Data);
            }
        }


        public async void Connect(TcpClient _client)
        {
            IsActive = true;
            tcpClient = _client;
            tcpClient.ReceiveBufferSize = DataBufferSize;
            tcpClient.SendBufferSize = DataBufferSize;

            nstream = tcpClient.GetStream();
            receiveBuffer = new byte[DataBufferSize];
            
            try
            {
                while(nstream.CanRead)
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
                IsActive = false;
                nstream.Close();
                _client.Close();
                _client = null;
            }
        }
        #endregion
        /******************************************/
    }
}