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

        private String projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        private String name1;
        private String name2;
        private String inputName1;
        private String inputName2;


        public LauncherWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Set variables for path
            inputName1 = System.IO.Path.Combine(projectDirectory, "Name1.txt");
            inputName2 = System.IO.Path.Combine(projectDirectory, "Name2.txt");

            // Read from files
            using (StreamReader sr1 = new StreamReader(inputName1))
            {
                inputName1 = String.Join(" ", File.ReadAllLines(inputName1));
                sr1.Close();
            }

            using (StreamReader sr2 = new StreamReader(inputName2))
            {
                inputName2 = String.Join(" ", File.ReadAllLines(inputName2));
                sr2.Close();
            }

            _Name1.Text = inputName1;
            _Name2.Text = inputName2;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            name1 = _Name1.Text;
            name2 = _Name2.Text;

            // Write to files
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(projectDirectory, "Name1.txt")))
            {
                outputFile.WriteLine(name1);
                outputFile.Close();
            }

            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(projectDirectory, "Name2.txt")))
            {
                outputFile.WriteLine(name2);
                outputFile.Close();
            }

            if (_RadioPlayerLeft.IsChecked == true)
            {
                player0 = new HumanPlayer(name1, 0);
            } else
            {
                int dif = Convert.ToInt32(_SliderLeft.Value);
                player0 = GenerateAIPlayerOfDif(dif);
            }

            if (_RadioPlayerRight.IsChecked == true)
            {
                player1 = new HumanPlayer(name2, 1);
            }
            else
            {
                int dif = Convert.ToInt32(_SliderRight.Value);
                player1 = GenerateAIPlayerOfDif(dif);
            }


            var newMyWindow2 = new GameWindow(state, player0, player1);
            newMyWindow2.Show();
            this.Hide();
            newMyWindow2.Closed += new EventHandler(GameWindow_Closed);
        }

        private AIPlayer GenerateAIPlayerOfDif(int dif)
        {
            if (dif == 1)
            {
                return new RandomPlayer("AI", 1);
            }
            else
            {
                return new MiniMaxPlayer("AI", 1, dif);
            }
        }


        private void GameWindow_Closed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void _RadioLeft_Checked(object sender, RoutedEventArgs e)
        {
            if (_RadioAILeft.IsChecked == true)
            {
                _Name1.Text = "AI";
                _Name1.IsReadOnly = true;
                _SliderLeft.Visibility = Visibility.Visible;
                _DifficultyLeft.Visibility = Visibility.Visible;
            }
            else
            {
                _Name1.IsReadOnly = false;
                _Name1.Text = "Player 1";
                _SliderLeft.Visibility = Visibility.Hidden;
                _DifficultyLeft.Visibility = Visibility.Hidden;

            }
        }

        private void _RadioRight_Checked(object sender, RoutedEventArgs e)
        {
            if (_RadioAIRight.IsChecked == true)
            {
                _Name2.Text = "AI";
                _Name2.IsReadOnly = true;
                _SliderRight.Visibility = Visibility.Visible;
                _DifficultyRight.Visibility = Visibility.Visible;

            }
            else
            {
                _Name2.IsReadOnly = false;
                _Name2.Text = "Player 2";
                _SliderRight.Visibility = Visibility.Hidden;
                _DifficultyRight.Visibility = Visibility.Hidden;


            }
        }

        private void _SliderLeft_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_SliderLeft.Value >= 1 && _SliderLeft.Value <= 3){

                _DifficultyLeft.Text = "Easy";
            } else if (_SliderLeft.Value >= 4 && _SliderLeft.Value <= 6){

                _DifficultyLeft.Text = "Medium";
            } else if (_SliderLeft.Value >= 7 && _SliderLeft.Value <= 9){

                _DifficultyLeft.Text = "Hard";
            }
        }

            private void _SliderRight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            {
            if (_SliderRight.Value >= 1 && _SliderRight.Value <= 3)
            {

                _DifficultyRight.Text = "Easy";
            }
            else if (_SliderRight.Value >= 4 && _SliderRight.Value <= 6)
            {

                _DifficultyRight.Text = "Medium";
            }
            else if(_SliderRight.Value >= 7 && _SliderRight.Value <= 9)
            {

                _DifficultyRight.Text = "Hard";

            }
        }
    }
}
