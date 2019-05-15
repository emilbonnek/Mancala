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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Game game;
        public GameWindow(Game game)
        {
            this.game = game;
            InitializeComponent();
        }

        private void UpdateView(Gamestate state)
        {
            // Alt det her børe sættes i en løkke
            _0r0.Content = state.rows[0][0].ToString();
            _0r1.Content = state.rows[0][1].ToString();
            _0r2.Content = state.rows[0][2].ToString();
            _0r3.Content = state.rows[0][3].ToString();
            _0r4.Content = state.rows[0][4].ToString();
            _0r5.Content = state.rows[0][5].ToString();
            _0pit.Content = state.rows[0][6].ToString();

            _1r0.Content = state.rows[1][0].ToString();
            _1r1.Content = state.rows[1][1].ToString();
            _1r2.Content = state.rows[1][2].ToString();
            _1r3.Content = state.rows[1][3].ToString();
            _1r4.Content = state.rows[1][4].ToString();
            _1r5.Content = state.rows[1][5].ToString();
            _1pit.Content = state.rows[1][6].ToString();
        }

        private void Button_A_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(game.currentState);
        }

        private void Button_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
