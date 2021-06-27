 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace GolfServ
{
    class ServerObj
    {
        static TcpListener tcpListener;
        List<Room> rooms = new List<Room>();
        public void Start()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 13000);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    new Thread(() => {
                        NetworkStream stream = tcpClient.GetStream();
                        User user = new User(stream);
                        Room room;
                        if (user.UserId%2==0)
                        {
                            room = new Room();
                            SendMessage(stream, "Wait");
                            rooms.Add(room);
                            room.user1 = user;
                            user.turn = Turn.Player1;
                        }
                        else
                        {
                            room = rooms[rooms.Count - 1];
                            room.user2 = user;
                            SendMessage(room.user1.Stream,"Connected");
                            user.turn = Turn.Player1;
                        }
                        while (true)
                        {
                            if (room.turn == user.turn)
                            {
                                SendMessage(user.Stream, "Your Turn");
                                if (user == room.user1)
                                {
                                    SendMessage(room.user2.Stream, GetMessage(user.Stream));
                                }
                                else
                                {
                                    SendMessage(room.user1.Stream, GetMessage(user.Stream));
                                }
                            }
                            
                        }
                    }).Start();


                    
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        private string GetMessage(NetworkStream Stream)
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
    }
}
