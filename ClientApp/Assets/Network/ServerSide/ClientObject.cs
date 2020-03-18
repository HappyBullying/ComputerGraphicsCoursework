using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using ClientApp.Network;
using System.Collections.Generic;

namespace ClientApp.Network.ServerSide
{
    public class ClientObject
    {
        /****************PROPERTIES****************/
        #region PROPERTIES
        public int ClientId { get; private set; }
        public string Username { get; private set; }
        public int DataBufferSize = 8192;
        public bool IsActive 
        {
            get { return isActive; }
            set { isActive = value; }
        }
        private bool isActive = false;
        public ulong LastHandledPackageId { get; private set; } = 0;
        private TcpClient tcpClient;
        private NetworkStream nstream;
        private Queue<Package> toSend = new Queue<Package>();
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
            isActive = true;
            tcpClient = _client;
            tcpClient.ReceiveBufferSize = DataBufferSize;
            tcpClient.SendBufferSize = DataBufferSize;

            nstream = tcpClient.GetStream();
            receiveBuffer = new byte[DataBufferSize];
            
            try
            {
                while(isActive)
                {
                    if (nstream.CanRead && nstream.DataAvailable)
                    {
                        int bytesReceived = await nstream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);
                        byte[] data = new byte[bytesReceived];
                        Array.Copy(receiveBuffer, data, bytesReceived);
                        
                        string text = System.Text.Encoding.UTF8.GetString(data, 0, bytesReceived);
                        Debug.Log(text);
                    }

                    if (nstream.CanWrite)
                    {
                        // send
                    }
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Data);
            }
            finally
            {
                isActive = false;
                nstream.Close();
                _client.Close();
                _client = null;
            }
        }
        #endregion
        /******************************************/
    }
}