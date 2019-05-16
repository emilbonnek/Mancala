using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class AIPlayer : Player{
        private int difficulty;
        public AIPlayer(string name, int playernumber, int difficulty = 8) : base(name, playernumber) {this.difficulty = difficulty}
        public override int playTurn(Gamestate state)
        {
            Console.WriteLine("{0}'s turn", name);
            int decision = Minimax(-1, state, difficulty).Item1;
            Console.WriteLine(decision + 1);
            return decision;
        }


        /* This is how the AI player makes decisions
         *
         * Closely follows the pseudocode from https://en.wikipedia.org/wiki/Minimax
         *
         * It returns a tuple that includes the action taken and the value returned from that action.
         *
         * The option parameter is used for recursiveness and keeping track of the recursion tree
         * The state parameter is a representation of the gamestate that the algorithm will work on
         * The depth parameter can be used to set the maximum search depth
         */
        private Tuple<int, int> Minimax(int option, Gamestate state, int depth)
        {
            if (state.IsOver())
            {
                // A terminal node has been reached! should be valued +/- infinity
                if (playernumber == state.Winner())
                {
                    return Tuple.Create(option, int.MaxValue);
                }
                else
                {
                    return Tuple.Create(option, int.MinValue);
                }
            }
            else if (depth == 0)
            {
                // The desired depth has been reached. We will use a heuristic to value the current node.
                return Tuple.Create(option, HeuristicValue(state));
            }


            int[] options = state.Options();

            if (state.turn % state.rows.Length == playernumber)
            {
                // Its our turn, we want to maximize
                int maxopt = options[0];
                Tuple<int, int> max_optvalue = Tuple.Create(-1, int.MinValue);
                for (int i = 0; i < options.Length; i++)
                {
                    Tuple<int, int> optvalue = Minimax(options[i], state.DryPlay(options[i]), depth - 1);
                    if (optvalue.Item2 >= max_optvalue.Item2)
                    {
                        maxopt = options[i];
                        max_optvalue = optvalue;
                    }
                }

                return Tuple.Create(maxopt, max_optvalue.Item2);
            }
            else
            {
                // Its the oppositions turn, we assume they want to minimize
                int minopt = options[0];
                Tuple<int, int> min_optvalue = Tuple.Create(-1, int.MaxValue);
                for (int i = 0; i < options.Length; i++)
                {
                    Tuple<int, int> optvalue = Minimax(options[i], state.DryPlay(options[i]), depth - 1);
                    if (optvalue.Item2 <= min_optvalue.Item2)
                    {
                        minopt = options[i];
                        min_optvalue = optvalue;
                    }
                }

                return Tuple.Create(minopt, min_optvalue.Item2);
            }
        }

        // The heuristic used is very simple. It considers the players own score according to the best player on the board ()
        private int HeuristicValue(Gamestate state)
        {
            int myScore = state.rows[playernumber][state.rows[playernumber].Length - 1];
            int maxOppositionScore = -1;
            for (int i = 0; i < state.rows.Length; i++)
            {
                if (i != playernumber && state.rows[i][state.rows[i].Length - 1] < myScore)
                {
                    maxOppositionScore = state.rows[i][state.rows[i].Length - 1];
                }
            }

            return myScore - maxOppositionScore;
        }
    }
}
