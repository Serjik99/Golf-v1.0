﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace Golf_v1_0
{
    public class Client
    {
        private static string host = "127.0.0.1";
        private static int port = 7632;
        static TcpClient tcpClient;
        static NetworkStream network;
        public  string message;
        public  string recievedMessage;
        
        public void GetConnection()
        {

            
            new Thread(() =>
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(host, port);
                network = tcpClient.GetStream();
                if (GetMessage(network) == "Wait")
                {
                    recievedMessage = "Wait";
                    GetMessage(network);
                }
                else if (GetMessage(network)=="Connect")
                {
                    recievedMessage = "Connected";
                    while (true)
                    {
                        if (GetMessage(network) == "Your Turn")
                        {
                            SendMessage(network,message);
                        }
                        else
                        {
                            recievedMessage = GetMessage(network);
                        }

                        
                    }
                }
                
            }).Start();
            
        }
        private static string GetMessage(NetworkStream Stream)
        {
            byte[] data = new byte[64]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }
        public static void SendMessage(NetworkStream stream, string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);

        }
        public void Disconnect()
        {
            tcpClient.Close();
        }
    }
}
