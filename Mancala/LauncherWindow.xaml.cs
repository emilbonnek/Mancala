using System;
using System.Collections.Generic;
using System.IO;
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
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
        private Player player0;
        private Player player1;
        private Gamestate state = new Gamestate(0, new[] { 6, 6, 6, 6, 6, 6, 0 }, new[] { 6, 6, 6, 6, 6, 6, 0 });
        private Game game;

        private String name1;
        private String name2;

        public LauncherWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            name1 = _Name1.Text;
            name2 = _Name2.Text;

            // Set a variable to the Documents path.
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Write to files
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, "Name1.txt")))
            {
                outputFile.WriteLine(name1);
            }

            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, "Name2.txt")))
            {
                outputFile.WriteLine(name2);
            }


            var newMyWindow2 = new GameWindow(game);
            newMyWindow2.Show();
            this.Hide();
            newMyWindow2.Closed += new EventHandler(GameWindow_Closed);
        }

        private void GameWindow_Closed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void _RadioPlayer_Checked(object sender, RoutedEventArgs e)
        {

            if (_RadioPlayer.IsChecked == true)
            {
                player0 = new HumanPlayer("Player", 0);
                player1 = new HumanPlayer("Player", 1);
                game = new Game(state, player0, player1);

            }
            else if (_RadioAI.IsChecked == true)
            {
                player0 = new AIPlayer("AI", 0);
                player1 = new HumanPlayer("Player", 1);
                game = new Game(state, player0, player1);
            }
        }
    }
}
