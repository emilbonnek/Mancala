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
        public override int playTurn(Gamestate state)
        {

            if (state.rows[playernumber].Length - 1 <= 9)
            {
                // There is less than 9 pockets so we will receive inputs as chars and react straight away
                char input;
                while (true)
                {
                    Console.WriteLine("{0}'s turn: ({1}-{2})", name, 1, state.rows[playernumber].Length - 1);
                    input = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    if (input >= 49 && input <= 49 + state.rows[playernumber].Length - 2)
                    {
                        Console.WriteLine();
                        return input - 49;
                    }
                }
            }
            else
            {
                // There is too many pockets, so we have to receive a string and have the player press enter
                string input;
                int number;
                while (true)
                {
                    try
                    {
                        Console.WriteLine("{0}'s turn: ({1}-{2})", name, 1, state.rows[playernumber].Length - 1);
                        input = Console.ReadLine();
                        Console.WriteLine();
                        number = Int32.Parse(input);
                        if (number >= state.rows[playernumber].Length || number <= 0)
                        {
                            throw new Exception();
                        }
                        return number - 1;
                    }
                    catch (Exception e) { }
                }
            }
        }
    }
}
