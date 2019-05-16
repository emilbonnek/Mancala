using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        
        private Button[][] pitButtons;
        private Label[][] pitLabels;
        
        
        public GameWindow(Gamestate state, params Player[] players){
            this.state = state;
            this.players = players;
            
            InitializeComponent();
            
            PrepareBoard(players);
            UpdateBoard(state);
            
            
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += KeepAIawake;
            worker.RunWorkerCompleted += PresentEndGameScreen;
            worker.RunWorkerAsync();
        }

        private void PresentEndGameScreen(object sender, RunWorkerCompletedEventArgs e){
            action.Content = $"Game is over, {players[state.Winner()].name} won!";
            _0name.Background = Brushes.Transparent;
            _1name.Background = Brushes.Transparent;
            
        }

        void KeepAIawake(object sender, DoWorkEventArgs e){
            while (!state.IsOver()){
                Player currentPlayer = players[state.turn % 2];
                if (currentPlayer is AIPlayer){
                    play(((AIPlayer) currentPlayer).playTurn(state));
                }
                
                Thread.Sleep(100);
            }
        }

        void play(int n){
            Player currentPlayer = players[state.turn % 2];
            
            Dispatcher.Invoke((Action)(() => {
                action.Content = $"{currentPlayer.name} is playing...";
            }));


            // end
            state = state.DryPlay(n);
            Dispatcher.Invoke((Action)(() => {
                UpdateBoard(state);
            }));
        }


        private void PrepareBoard(Player[] players)
        {
            // Write player names
            _0name.Content = players[0].name;
            _1name.Content = players[1].name;
            
            // Create a list of all the pit labels
            pitLabels = new Label[][]{
                new []{_0r0, _0r1,_0r2, _0r3,_0r4, _0r5, _0pit},
                new []{_1r0, _1r1,_1r2, _1r3,_1r4, _1r5, _1pit}
            };
            
            // Create a list of all the pit buttons
            pitButtons = new Button[][]{
                new []{_0r0_Button, _0r1_Button,_0r2_Button, _0r3_Button,_0r4_Button, _0r5_Button},
                new []{_1r0_Button, _1r1_Button,_1r2_Button, _1r3_Button,_1r4_Button, _1r5_Button}
            };
        }

        private void UpdateBoard(Gamestate state)
        {
            for (int i = 0; i < state.rows.Length; i++){
                for (int j = 0; j < state.rows[i].Length; j++){
                    pitLabels[i][j].Content = state.rows[i][j].ToString();
                }
            }

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
            
            // show and hide the right buttons
            int[] options = state.Options();
            for (int i = 0; i < state.rows.Length; i++){
                if (i == state.turn % 2 && players[state.turn % 2] is HumanPlayer){
                    for (int j = 0; j < pitButtons[i].Length; j++){
                        if (options.Contains(j)){
                            pitButtons[i][j].Visibility = Visibility.Visible;
                        } else {
                            pitButtons[i][j].Visibility = Visibility.Hidden;
                        }
                    }
                } else{
                    for (int j = 0; j < pitButtons[i].Length; j++){
                        pitButtons[i][j].Visibility = Visibility.Hidden;
                    }
                }
            }
            
            Player currentPlayer = players[state.turn % 2];
            action.Content = $"{currentPlayer.name} is thinking...";
            
        }
        

        private void PitPicked(object sender, RoutedEventArgs e){
            play(int.Parse(((Button) sender).Tag.ToString()));
        }
    }
}
