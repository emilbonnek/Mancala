using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public abstract class Player
    {
        public string name;
        public int playernumber;
        public Player(string name, int playernumber)
        {
            this.name = name;
            this.playernumber = playernumber;
        }

      
    }
}
