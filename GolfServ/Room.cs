using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfServ
{
    public enum Turn
    {
        Player1, Player2, None
    }
    class Room
    {
        public User user1;
        public User user2;
        public Turn turn = Turn.None;
    }
}
