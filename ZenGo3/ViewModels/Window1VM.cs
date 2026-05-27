using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZenGo3.ViewModels;
using static ZenGo3.GameViewVM;
using ZenGo3.Services;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Threading;
using System.Text.Json;
using Newtonsoft.Json;

namespace ZenGo3.ViewModels
{
   public class Window1VM : INotifyPropertyChanged
    {

        private Window1 _window1;

        GameStats _gameStats = new GameStats();

        private GameViewVM _gameViewVM;

        private DispatcherTimer _uiTimer;


        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(nameof(_currentView)); }
        }

        //public int _skippedTurns = 0;

        // The string property bound to the UI
        private string _gridText;
        public string GridText
        {
            get => _gridText;
            set
            {
                _gridText = value;
                UpdateGridSize(_gridText);

            }
        }

        private void UpdateGridSize(string sizeText)
        {
            switch (sizeText)
            {
                case "9x9":
                    GameStats.Grid = new int[9, 9];
                    break;
                case "13x13":
                    GameStats.Grid = new int[13, 13];
                    break;
                case "19x19":
                    GameStats.Grid = new int[19, 19];
                    break;
            }
        }


        public ICommand PlayCommand { get; }

        public ICommand AbandonCommand { get; }

        public ICommand NewGameCommand { get; }

        public ICommand MenuCommand { get; }

        public ICommand PasserTourCommand { get; }
        public ICommand SuicideTestCommand { get; }

        public GameStats GameStats
        {
            get { return _gameStats; }
            set
            {
                _gameStats = value;
                OnPropertyChanged(nameof(GameStats));
            }
        }

        // DESIGN-TIME CONSTRUCTOR
        public Window1VM()
        {
            _gameViewVM = new GameViewVM();
        }

        public Window1VM(Window1 window1)
        {
            PlayCommand = new RelayCommand(OnPlay);

            AbandonCommand = new RelayCommand(OnAbandon);

            NewGameCommand = new RelayCommand(OnNewGame);

            MenuCommand = new RelayCommand(OnMenu);

            PasserTourCommand = new RelayCommand(OnPasserTour);

            SuicideTestCommand = new RelayCommand(OnSuicideTest);

            _window1 = window1;

            _gameViewVM = new GameViewVM();



            // Pass the shared GameStats to GridCanvas
            if (_window1.GameView?.BoardCanvas != null)
            {
                _window1.GameView.BoardCanvas.GameStats = _gameStats;
                _window1.GameView.BoardCanvas.Initialize();

                // Subscribe to property changes
                _gameStats.PropertyChanged += (s, e) =>
                {

                    if (e.PropertyName == nameof(GameStats.ActivePlayer))
                    {
                        SwitchTimer();
                    }
                    if (e.PropertyName == nameof(GameStats.Grid))
                    {
                        _window1.GameView.BoardCanvas.RenderGrid();
                    }
                };

            }

            InitializeTimer();
        }

        #region Timer System

        private void InitializeTimer()
        {
            _uiTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200) // Smooth UI updates
            };
            _uiTimer.Tick += UiTimer_Tick;
        }

        private void UiTimer_Tick(object sender, EventArgs e)
        {
            // Just notify UI to refresh time bindings
            OnPropertyChanged(nameof(GameStats));
        }

        public void StartPlayerTimer()
        {
            if (GameStats.ActivePlayer == 1) // Black
            {
                GameStats.BlackStopwatch.Start();
                GameStats.WhiteStopwatch.Stop();
            }
            else // White
            {
                GameStats.WhiteStopwatch.Start();
                GameStats.BlackStopwatch.Stop();
            }

            _uiTimer.Start();
        }

        public void StopPlayerTimer()
        {
            GameStats.BlackStopwatch.Stop();
            GameStats.WhiteStopwatch.Stop();
            _uiTimer.Stop();
        }

        public void SwitchTimer()
        {
            StopPlayerTimer();
            StartPlayerTimer();
        }

        private void ResetTimers()
        {
            StopPlayerTimer();
            GameStats.BlackStopwatch.Reset();
            GameStats.WhiteStopwatch.Reset();
        }

        #endregion

        private void ResetModel()
        {
            StopPlayerTimer();

            _gameStats.BlackSurnom = "";
            _gameStats.WhiteSurnom = "";

            _gameStats.ActivePlayer = 1;

            _gameStats.Handicap = 0;
            _gameStats.Komi = 0.5;
            _gameStats.Grid = null;

            //// Reset accumulated timers
            //_blackAccumulated = TimeSpan.Zero;
            //_whiteAccumulated = TimeSpan.Zero;

            // Reset scores 
            _gameStats.BlackScore = 0;
            _gameStats.WhiteScore = 0;
            _gameStats.BlackCaptures = 0;
            _gameStats.WhiteCaptures = 0;

            _gameStats.SkippedTurns = 0;

            _gameStats.BlackTerritories = 0;
            _gameStats.WhiteTerritories = 0;

            // Re-assign to BoardCanvas
            if (_window1.GameView?.BoardCanvas != null)
            {
                _window1.GameView.BoardCanvas.GameStats = _gameStats;
                _window1.GameView.BoardCanvas.ResetGrid();   
            }

            OnPropertyChanged(nameof(GameStats));   // This notifies UI
        }


        private void OnPlay(object parameter)
        {

            if (!string.IsNullOrWhiteSpace(GameStats.BlackSurnom) && !string.IsNullOrWhiteSpace(GameStats.WhiteSurnom))
            {
                _window1.GameView.BoardCanvas.PlaceHandicap2(_gameStats.Handicap);
                this._window1.SettingsUC.Visibility = Visibility.Hidden;
                this._window1.SettingsUC.IsEnabled = false;

                this._window1.GameView.Visibility = Visibility.Visible;
                this._window1.GameView.IsEnabled = true;

                StartPlayerTimer();
            }
            else
                MessageBox.Show("Veuillez saisir un surnom pour commencer");
            return;
        }

        private void OnAbandon(object parameter)
        {
            StopPlayerTimer();


            MessageBoxResult result = MessageBox.Show(
            "Voulez-vous vraiment abandonner la partie ?",
            "Confirmation abandonner",
            MessageBoxButton.OKCancel);

            if (result != MessageBoxResult.OK)
                return;


            //this._gameViewVM.StopPlaySession();

            var (scoreNoir, scoreBlanc, gagnant, blackTerritories, whiteTerritories) = CalculerScore(_gameStats.Grid, _gameStats.Komi, scoringLogic.Territory, GameStats.BlackCaptures, GameStats.WhiteCaptures);

            GameStats.BlackScore = scoreNoir;

            GameStats.WhiteScore = scoreBlanc;

            GameStats.BlackTerritories = blackTerritories;

            GameStats.WhiteTerritories = whiteTerritories;


            QuitAndSave();

            this._window1.GameView.BoardCanvas.ResetGrid();


            GameStats.Handicap = 0;

            this._gameViewVM.ResetPlayerStats();
            this._window1.GameView.Visibility = Visibility.Hidden;
            this._window1.GameView.IsEnabled = false;


            this._window1.EndGameUC.Visibility = Visibility.Visible;
            this._window1.EndGameUC.IsEnabled = true;

        }


        private void OnNewGame(object parameter)
        {
            ResetModel();
            ResetTimers();

            this._window1.GameView.Visibility = Visibility.Visible;
            this._window1.GameView.IsEnabled = true;


            //this._gameViewVM.StartPlaySession();


            this._window1.EndGameUC.Visibility = Visibility.Hidden;

            this._window1.EndGameUC.IsEnabled = false;


           

            CurrentView = this._window1.GameView;

            //GameStats.ActivePlayer = 1; // Black starts
            StartPlayerTimer();
        }


        private void OnMenu(object parameter)
        {
            ResetModel();

            this._window1.SettingsUC.Visibility = Visibility.Visible; this._window1.SettingsUC.IsEnabled = true;


            this._window1.EndGameUC.Visibility = Visibility.Hidden;

            this._window1.EndGameUC.IsEnabled = false;
        }

        public void OnPasserTour(object parameter)
        {

            var (scoreNoir, scoreBlanc, gagnant, blackTerritories, whiteTerritories) = CalculerScore(_gameStats.Grid, GameStats.Komi, scoringLogic.Territory, GameStats.BlackCaptures, GameStats.WhiteCaptures);

            GameStats.BlackScore = scoreNoir;

            GameStats.WhiteScore = scoreBlanc;

            GameStats.BlackTerritories = blackTerritories;
            GameStats.WhiteTerritories = whiteTerritories;

            //_skippedTurns++;

            GameStats.SkippedTurns++;

            //MessageBox.Show("PasserTour cliqué via le ViewModel");
            _window1.GameView.BoardCanvas.SkipTurn();

            SwitchTimer();

            if (GameStats.SkippedTurns >= 2)
            {
                StopPlayerTimer();

                GameStats.SkippedTurns = 0;
                //this._gameViewVM.StopPlaySession();

                MessageBox.Show("Les deux joueurs ont passé leur tour. Fin de la partie.");

                QuitAndSave();

                this._window1.GameView.BoardCanvas.ResetGrid();

                this._gameViewVM.ResetPlayerStats();
                this._window1.GameView.Visibility = Visibility.Hidden;
                this._window1.GameView.IsEnabled = false;


                this._window1.EndGameUC.Visibility = Visibility.Visible;
                this._window1.EndGameUC.IsEnabled = true;
            }
        }


        public void QuitAndSave()
        {
            SaveToJson saveToJson = new SaveToJson();
            GameStats.JaggedGrid = saveToJson.ArrayToJaggedArray(_gameStats.Grid, 9);

            string jsonText = JsonConvert.SerializeObject(GameStats);
            saveToJson.Save(jsonText);

            ResetTimers();

            StopPlayerTimer();
        }

        public void OnSuicideTest(object parameter)
        {
            _window1.GameView.BoardCanvas.SuicideTestGrid();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}
