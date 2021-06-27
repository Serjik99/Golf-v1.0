﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace GolfChat
{
    class Program
    {
        
        static string userName;
        public static string host;
        public static int port;
        static TcpClient client;
        static NetworkStream stream;
 
        static void Main(string[] args)
        {
            Console.WriteLine("Введите айпи сервера");
            host = Console.ReadLine();
            Console.WriteLine("Введите порт");
            port = Convert.ToInt32(Console.ReadLine());
            while (true)
            {
                Console.Write("Введите свое имя: ");
                userName = Console.ReadLine();
                client = new TcpClient();

                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток
 
                string message = userName;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
                
                // запускаем новый поток для получения данных
                Byte[] readingData = new Byte[1];
                string responseData = String.Empty;
                StringBuilder completeMessage = new StringBuilder();
                int numberOfBytesRead = 0;
                do
                {
                    numberOfBytesRead = stream.Read(readingData, 0,readingData.Length);
                    completeMessage.AppendFormat("{0}", Encoding.UTF8.GetString(readingData, 0, numberOfBytesRead));
                }
                while (stream.DataAvailable);
                responseData = completeMessage.ToString();
                
                if (responseData== "1")
                {
                    break;
                }
                else
                {
                    client.Dispose();
                }
                    
            }
            try
            {
                
 
                string message = userName;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
 
                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
                Console.WriteLine("Добро пожаловать, {0}", userName);
                SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }
        // отправка сообщений
        static void SendMessage()
        {
            Console.WriteLine("Введите сообщение: ");
            Console.WriteLine("Перед закрытием приложения пожалуйста отключитесь от сервера с помощью команды $disconnect");
            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
                if (message == "$disconnect")
                {
                    Disconnect();
                  
                }
                
                
            }
        }
        // получение сообщений
        static void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
 
                    string message = builder.ToString();
                    Console.WriteLine(message);//вывод сообщения
                }
                catch
                {
                    Console.WriteLine("Подключение прервано!"); //соединение было прервано
                    Console.ReadLine();
                    Disconnect();
                }
            }
        }
 
        static void Disconnect()
        {
            if(stream!=null)
                stream.Close();//отключение потока
            if(client!=null)
                client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        
        }
    }
}
