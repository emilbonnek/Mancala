using System;
using System.Collections.Generic;
using System.Linq;

namespace Mancala
{
    public class Gamestate
    {
        public int[][] rows;
        public int turn;

        public Gamestate(int turn, params int[][] rows)
        {
            this.rows = rows;
            this.turn = turn;
        }


        // Should return true if the game is over.
        public bool IsOver()
        {
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows[i].Length - 1; j++)
                {
                    if (rows[i][j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Should return the index if the winner, can only be called when game is over
        public int Winner()
        {
            if (!IsOver())
            {
                throw new Exception("Game is not over");
            }

            int winner = -1;
            int winnerscore = -1;
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i][rows[i].Length - 1] > winnerscore)
                {
                    winner = i;
                    winnerscore = rows[i][rows[i].Length - 1];
                }
            }

            return winner;
        }

        // Clones the object
        private Gamestate Clone()
        {
            int[][] cloneRows = new int[rows.Length][];
            for (int i = 0; i < rows.Length; i++)
            {
                cloneRows[i] = (int[])rows[i].Clone();
            }
            return new Gamestate(turn, cloneRows);
        }

        // Returns a list of all possible options for the player whos turn it is
        public int[] Options()
        {
            int currentrow = turn % rows.Length;
            List<int> options = new List<int>();

            for (int i = 0; i < rows[0].Length - 1; i++)
            {
                if (rows[currentrow][i] > 0)
                {
                    options.Add(i);
                }
            }

            return options.ToArray();
        }

        // Returns a new gamestate that represents how the gamestate will be after a given move.
        // This is where the 'rules' are implemented
        public Gamestate DryPlay(int pocket)
        {
            Gamestate workingcopy = Clone();

            // picking from an empty pocket should result in no changes to the gamestate
            if (workingcopy.rows[workingcopy.turn % workingcopy.rows.Length][pocket] == 0)
            {
                Console.WriteLine("Picked from empty pocket, try again");
                return workingcopy;
            }

            // Pick up the stones
            int hand = workingcopy.rows[workingcopy.turn % 2][pocket];
            workingcopy.rows[workingcopy.turn % 2][pocket] = 0;
            int i_row = workingcopy.turn % 2;
            int i_pocket = pocket;

            // Go around the board putting stones in the pockets
            while (hand > 0)
            {
                i_pocket++;
                if (i_pocket == workingcopy.rows[i_row].Length - 1 && (workingcopy.turn % 2 != i_row))
                {
                    i_row = (i_row + 1) % 2;
                    i_pocket = 0;
                }

                if (i_pocket == workingcopy.rows[i_row].Length)
                {
                    i_row = (i_row + 1) % 2;
                    i_pocket = 0;
                }

                workingcopy.rows[i_row][i_pocket]++;
                hand--;
            }

            // Last stone was put into empty pocket on players own side (AND its not that players store)
            if (workingcopy.turn % 2 == i_row && workingcopy.rows[i_row][i_pocket] == 1 && i_pocket != workingcopy.rows[i_row].Length - 1)
            {

                // Calculate the pocket opposite
                int o_row = (i_row + workingcopy.rows.Length / 2) % workingcopy.rows.Length;
                int o_pocket = workingcopy.rows[o_row].Length - 2 - i_pocket;

                workingcopy.rows[i_row][workingcopy.rows[i_row].Length - 1] += workingcopy.rows[i_row][i_pocket];
                workingcopy.rows[i_row][workingcopy.rows[i_row].Length - 1] += workingcopy.rows[o_row][o_pocket];
                workingcopy.rows[i_row][i_pocket] = 0;
                workingcopy.rows[o_row][o_pocket] = 0;
            }

            // one row is completely empty. Player across gets to put all his stone in his store
            for (int i = 0; i < workingcopy.rows.Length; i++)
            {
                int sum = 0;
                for (int j = 0; j < workingcopy.rows[i].Length - 1; j++)
                {
                    sum += workingcopy.rows[i][j];
                }

                if (sum == 0)
                {
                    sum = 0;
                    int o_row = (i + workingcopy.rows.Length / 2) % workingcopy.rows.Length;
                    for (int j = 0; j < workingcopy.rows[o_row].Length - 1; j++)
                    {
                        sum += workingcopy.rows[o_row][j];
                        workingcopy.rows[o_row][j] = 0;
                    }

                    workingcopy.rows[o_row][workingcopy.rows[o_row].Length - 1] += sum;
                }
            }

            // Last stone was put into players own store
            if (i_pocket == workingcopy.rows[i_row].Length - 1 && workingcopy.turn % 2 == i_row)
            {
                return workingcopy;
            }


            workingcopy.turn++;
            return workingcopy;
        }




        // The following method are used solely to present the gameboard in the console
        public void show()
        {
            Console.WriteLine("   " + string.Join(" - ", dd(rows[0].Take(rows[0].Length - 1).Reverse())));
            Console.WriteLine("{0}{1}{2}", dd(rows[0][rows[0].Length - 1]), new string(' ', rows[0].Length - 2 + 4 * (rows[0].Length - 1)), dd(rows[1][rows[1].Length - 1]));
            Console.WriteLine("   " + string.Join(" - ", dd(rows[1].Take(rows[1].Length - 1))));
        }

        // The following method are used solely to present the gameboard in the console
        private string[] dd(IEnumerable<int> amounts)
        {
            int[] amts = amounts.ToArray();
            string[] numbers = new string[amts.Length];
            for (int i = 0; i < amts.Length; i++)
            {
                numbers[i] = dd(amts[i]);
            }
            return numbers;
        }
        // The following method are used solely to present the gameboard in the console
        private string dd(int amount)
        {
            if (amount >= 10)
            {
                return amount.ToString();
            }
            else
            {
                return " " + amount;
            }
        }
    }
}