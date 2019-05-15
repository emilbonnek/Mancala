using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mancala
{
    /// <summary>
    /// Interaction logic for LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow : Window
    {
        public LauncherWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create the board representation
            Gamestate state = new Gamestate(0, new[] { 6, 6, 6, 6, 6, 6, 0 }, new[] { 6, 6, 6, 6, 6, 6, 0 });

            // Set up the players
            Player player0 = new AIPlayer("AI", 0);
            Player player1 = new HumanPlayer("Player", 1);

            Game game = new Game(state, player0, player1);
            var newMyWindow2 = new GameWindow(game);
            newMyWindow2.Show();
            this.Hide();
            newMyWindow2.Closed += new EventHandler(GameWindow_Closed);
        }

        private void GameWindow_Closed(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
