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
        private Player player0;
        private Player player1;
        private Gamestate state = new Gamestate(0, new[] { 6, 6, 6, 6, 6, 6, 0 }, new[] { 6, 6, 6, 6, 6, 6, 0 });

        private String name1;
        private String name2;
        private String inputName1;
        private String inputName2;

        private bool alreadyFocused;

        public LauncherWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            name1 = _Name1.Text;
            name2 = _Name2.Text;

            // Set a variable to the Documents path.
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            // Write to files
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(projectDirectory, "Name1.txt")))
            {
                outputFile.WriteLine(name1);
            }

            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(projectDirectory, "Name2.txt")))
            {
                outputFile.WriteLine(name2);
            }

            // Set variables for path
            inputName1 = System.IO.Path.Combine(projectDirectory, "Name1.txt");
            inputName2 = System.IO.Path.Combine(projectDirectory, "Name2.txt");

            // Read from files
            using (StreamReader sr1 = new StreamReader(inputName1))
            {
                inputName1 = sr1.ReadToEnd();
                sr1.Close();
            }
            
            using (StreamReader sr2 = new StreamReader(inputName2))
            {
                inputName2 = sr2.ReadToEnd();
                sr2.Close();
            }

            if (_RadioPlayerLeft.IsChecked == true && _RadioPlayerRight.IsChecked == true)
            {
                player0 = new HumanPlayer(inputName1, 0);
                player1 = new HumanPlayer(inputName2, 1);

            }

            else if (_RadioPlayerRight.IsChecked == true && _RadioAILeft.IsChecked == true)
            {
                player0 = new AIPlayer("AI", 1);
                player1 = new HumanPlayer(inputName2, 0);
            }

            else if (_RadioPlayerLeft.IsChecked == true && _RadioAIRight.IsChecked == true)
            {
                player0 = new HumanPlayer(inputName1, 0);
                player1 = new AIPlayer("AI", 1);
            }

            else if (_RadioAIRight.IsChecked == true && _RadioAILeft.IsChecked == true)
            {
                player0 = new AIPlayer("AI", 1);
                player1 = new AIPlayer("AI", 1);
            }

            var newMyWindow2 = new GameWindow(state, player0, player1);
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
            if (_RadioAILeft.IsChecked == true)
            {
                _Name1.Text = "AI";
                _Name1.IsReadOnly = true;
            } else
            {
                _Name1.IsReadOnly = false;
            }
/*
            if (_RadioAIRight.IsChecked == true)
            {
                _Name2.Text = "AI";
                _Name2.IsReadOnly = true;
            }
            else
            {
                _Name2.IsReadOnly = false;
            }
            */
        }

        //Textbox text disapear/reappear
        private void _Name1_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (!alreadyFocused && box.Text == "Player 1 Name")
            {
                box.Text = String.Empty;
                alreadyFocused = true;
            } 
        }

        private void _Name1_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            alreadyFocused = false;
        }

        private void _Name2_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (!alreadyFocused && box.Text =="Player 2 Name")
            {
                box.Text = String.Empty;
                alreadyFocused = true;
            }
        }

        private void _Name2_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            alreadyFocused = false;
        }
    }
}
