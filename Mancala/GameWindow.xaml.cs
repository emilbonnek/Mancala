using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Gamestate state;
        private Player[] players;
        
        private int? act;
        public GameWindow(Gamestate state, params Player[] players){
            this.state = state;
            this.players = players;
            InitializeComponent();

            PrepareBoard(players);
            UpdatePitValues(state);
        }

        private void PrepareBoard(Player[] players)
        {
            _0name.Content = players[0].name;
            _1name.Content = players[1].name;

            _0r0_Button.Visibility = Visibility.Hidden;
            _0r1_Button.Visibility = Visibility.Hidden;
            _0r2_Button.Visibility = Visibility.Hidden;
            _0r3_Button.Visibility = Visibility.Hidden;
            _0r4_Button.Visibility = Visibility.Hidden;
            _0r5_Button.Visibility = Visibility.Hidden;

            _1r0_Button.Visibility = Visibility.Hidden;
            _1r1_Button.Visibility = Visibility.Hidden;
            _1r2_Button.Visibility = Visibility.Hidden;
            _1r3_Button.Visibility = Visibility.Hidden;
            _1r4_Button.Visibility = Visibility.Hidden;
            _1r5_Button.Visibility = Visibility.Hidden;
        }

        private void UpdatePitValues(Gamestate state)
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

            if (state.turn % 2 == 0)
            {
                _0name.Background = Brushes.Yellow;
                _1name.Background = Brushes.Transparent;
            }
            else
            {
                _0name.Background = Brushes.Transparent;
                _1name.Background = Brushes.Yellow;
            }
        }

        private void Button_A_Click(object sender, RoutedEventArgs e)
        {
            UpdatePitValues(state);
        }

        private async void Button_B_Click(object sender, RoutedEventArgs e){
            Player currentPlayer = players[state.turn % 2];
            
            // started turn
            action.Content = $"{currentPlayer.name} is thinking...";
            
            // Makes decision
            int decision = await Task.Run(() => MakeDecision(currentPlayer));

            action.Content = $"{currentPlayer.name} is playing...";
            // Plays decision (animate)
            
            // ended turn

            action.Content = "";
            state = state.DryPlay(currentPlayer.playTurn(state));
            UpdatePitValues(state);
        }

        private void Button_C_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        


        internal int MakeDecision(Player currentPlayer)
        {
            if (currentPlayer is AIPlayer){
                return players[state.turn % 2].playTurn(state);
            } else{
                Dispatcher.Invoke(() => {
                    switch (currentPlayer.playernumber){
                        case 0:
                            // UI operations go inside of Invoke
                            _0r0_Button.Visibility = Visibility.Visible;
                            _0r1_Button.Visibility = Visibility.Visible;
                            _0r2_Button.Visibility = Visibility.Visible;
                            _0r3_Button.Visibility = Visibility.Visible;
                            _0r4_Button.Visibility = Visibility.Visible;
                            _0r5_Button.Visibility = Visibility.Visible;
                            break;
                        case 1:
                            _1r0_Button.Visibility = Visibility.Visible;
                            _1r1_Button.Visibility = Visibility.Visible;
                            _1r2_Button.Visibility = Visibility.Visible;
                            _1r3_Button.Visibility = Visibility.Visible;
                            _1r4_Button.Visibility = Visibility.Visible;
                            _1r5_Button.Visibility = Visibility.Visible;
                            break;
                        default:
                            break;
                    }
                });

                while (this.act == null){
                    Thread.Sleep(100);
                }

                int returnValue = (int) this.act;
                this.act = null;
                return returnValue;
            }

        }

        private void PitPicked(object sender, RoutedEventArgs e){
            this.act = 4;
        }
    }
}
