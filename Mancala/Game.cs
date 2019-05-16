using System;

namespace Mancala
{
    public class Game
    {
        public Gamestate currentState;
        public Player[] players;
        // these constructors should share code
        public Game()
        {
            // Create the board representation
            currentState = new Gamestate(0, new[] { 6, 6, 6, 6, 6, 6, 0 }, new[] { 6, 6, 6, 6, 6, 6, 0 });

            // Set up the players
            Player player1 = new HumanPlayer("Player", 0);
            Player player2 = new AIPlayer("AI", 1);
            players = new[] { player1, player2 };
        }

        public Game(Gamestate state, params Player[] players)
        {
            // TODO should compain if the numbers dont match up

            // Create the board representation
            this.currentState = state;

            // Set up the players
            this.players = players;
        }

        
        public int playTurn()
        {
            Player currentplayer = players[currentState.turn % players.Length];
            currentState.show();
            int decision = currentplayer.playTurn(currentState);
            currentState = currentState.DryPlay(decision);
            Console.WriteLine("-----------------------------------------------");
            return decision;
        }

        
        public void finalpresentation()
        {
            Console.WriteLine("{0} won!", Winner());
            currentState.show();
        }

        public bool IsOver()
        {
            return currentState.IsOver();
        }

        // Shows the name of the winning player, can only be called once game is over
        private string Winner()
        {
            if (!IsOver())
            {
                throw new Exception("Game is not over");
            }

            return players[currentState.Winner()].name;
        }
    }
}