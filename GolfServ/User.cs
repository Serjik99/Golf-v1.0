using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;


namespace GolfServ
{
    class User
    {
        protected internal NetworkStream Stream { get; private set; }
        public int UserId;
        public Turn turn;
        static int Counter;
        public User(NetworkStream stream)
        {
            UserId = Counter;
            Counter++;
        }

    }
}
