using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, int playernumber) : base(name, playernumber) { }
        // Will return what the human player will do in his turn
        public override int playTurn(Gamestate state){
            return 2;
        }
    }
}
