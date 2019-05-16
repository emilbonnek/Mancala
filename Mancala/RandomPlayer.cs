using System;

namespace Mancala{
    public class RandomPlayer : AIPlayer{
        public RandomPlayer(string name, int playernumber) : base(name, playernumber){}

        public override int playTurn(Gamestate state){
            Random random = new Random();
            int[] options = state.Options();
            return options[random.Next(options.Length)];
        }
    }
}