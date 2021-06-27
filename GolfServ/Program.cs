using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace GolfServ
{
    class Program
    {
        static ServerObj server; // сервер
        static Thread listenThread; // потока для прослушивания
        static void Main(string[] args)
        {
            try
            {
                server = new ServerObj();
                listenThread = new Thread(new ThreadStart(server.Start));
                listenThread.Start(); //старт потока
            }
            catch
            {

            }
            
        }
    }
}
