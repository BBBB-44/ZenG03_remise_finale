using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace ZenGo3
{
    /// <summary>
    /// json structure
    /// </summary>
    public class GameStats : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int[,] _grid = new int[9, 9];
        [JsonIgnore]
        public int[,] Grid
        {
            get => _grid;
            set
            {
                if (_grid != value)
                {
                    _grid = value;
                    OnPropertyChanged(nameof(Grid));
                }
            }
        }

        // Only needed to save a jagged grid in a custom json format. Cannot save a [,], it explicitly need a jagged array [][]
        public int[][] JaggedGrid { get; set; } = null!;

        [JsonProperty("blackSurnom")]
        private string _blackSurnom;
        public string BlackSurnom
        {
            get => _blackSurnom;
            set
            {
                if (_blackSurnom != value)
                {
                    _blackSurnom = value ?? string.Empty;
                    OnPropertyChanged(nameof(BlackSurnom));
                }
            }
        }

        [JsonProperty("whiteSurnom")]
        private string _whiteSurnom;
        public string WhiteSurnom
        {
            get => _whiteSurnom;
            set
            {
                if (_whiteSurnom != value)
                {
                    _whiteSurnom = value ?? string.Empty; OnPropertyChanged(nameof(WhiteSurnom));
                }
            }
        }

        [JsonProperty("blackCaptures")]
        private int _blackCaptures;
        public int BlackCaptures
        {
            get => _blackCaptures;
            set
            {
                if (_blackCaptures != value)
                {
                    _blackCaptures = value;
                    OnPropertyChanged(nameof(BlackCaptures));
                }
            }
        }

        [JsonProperty("whiteCaptures")]
        private int _whiteCaptures;
        public int WhiteCaptures
        {
            get => _whiteCaptures;
            set
            {
                if (_whiteCaptures != value)
                {
                    _whiteCaptures = value;
                    OnPropertyChanged(nameof(WhiteCaptures));

                }
            }
        }





        private TimeSpan _blackPlayTimeSeconds;
        public TimeSpan BlackPlayTimeSeconds
        {
            get
                => BlackStopwatch.Elapsed;

            set
            {
                if (_blackPlayTimeSeconds != value)
                {
                    _blackPlayTimeSeconds = value;
                    OnPropertyChanged(nameof(BlackPlayTimeSeconds));
                }
            }
        }

        [JsonProperty("whitePlayTimeSeconds")]
        private TimeSpan _whitePlayTimeSeconds;
        public TimeSpan WhitePlayTimeSeconds
        {
            get
                => WhiteStopwatch.Elapsed;

            set
            {
                if (_whitePlayTimeSeconds != value)
                {
                    _whitePlayTimeSeconds = value;
                    OnPropertyChanged(nameof(WhitePlayTimeSeconds));
                }
            }
        }


        public Stopwatch BlackStopwatch { get; private set; } = new Stopwatch();
        public Stopwatch WhiteStopwatch { get; private set; } = new Stopwatch();


        [JsonProperty("totalTime")]
        private TimeSpan _totalTime;

        public TimeSpan TotalTime
        {
            get
                => BlackStopwatch.Elapsed + WhiteStopwatch.Elapsed;

            set
            {
                if (_totalTime != value)
                {
                    _totalTime = value;
                    OnPropertyChanged(nameof(TotalTime));
                }
            }
        }

        [JsonProperty("blackScore")]
        private double _blackScore;
        public double BlackScore
        {
            get
                => _blackScore;
            set
            {
                if (_blackScore != value)
                {
                    _blackScore = value;
                    OnPropertyChanged(nameof(BlackScore));
                }

            }
        }

        [JsonProperty("whiteScore")]
        private double _whiteScore;
        public double WhiteScore
        {
            get
                => _whiteScore;
            set
            {
                if (_whiteScore != value)
                {
                    _whiteScore = value;
                    OnPropertyChanged(nameof(WhiteScore));
                }

            }
        }

        [JsonProperty("DatePlayed")]
        public DateTime CurrentTime => DateTime.Now;

        [JsonProperty("komi")]
        private double _komi = 0.5;
        public double Komi
        {
            get => _komi;

            set
            {
                if (_komi != value)
                {
                    _komi = value;
                    OnPropertyChanged(nameof(Komi));

                }
            }
        }

        [JsonProperty("handicap")]
        private int _handicap;
        public int Handicap
        {
            get => _handicap;
            set
            {
                if (_handicap != value)
                {
                    _handicap = value;
                    OnPropertyChanged(nameof(Handicap));

                }
            }
        }

        private int _activePlayer = 1;
        [JsonIgnore]
        public int ActivePlayer
        {
            get =>
                _activePlayer;
            set
            {
                if (_activePlayer != value)
                {
                    _activePlayer = value;
                    OnPropertyChanged(nameof(ActivePlayer));
                }
            }
        }

        private int _skippedTurns = 0;
        [JsonIgnore]
        public int SkippedTurns
        {
            get => _skippedTurns;
            set
            {
                if (_skippedTurns != value)
                {
                    _skippedTurns = value;
                    OnPropertyChanged(nameof(SkippedTurns));
                }
            }
        }

        private int _blackTerritories = 0;

        public int BlackTerritories
        {
            get => _blackTerritories;
            set
            {
                if (_blackTerritories != value)
                {
                    _blackTerritories = value;
                    OnPropertyChanged(nameof(BlackTerritories));
                }
            }
        }

        private int _whiteTerritories = 0;

        public int WhiteTerritories
        {
            get => _whiteTerritories;
            set
            {
                if (_whiteTerritories != value)
                {
                    _whiteTerritories = value;
                    OnPropertyChanged(nameof(WhiteTerritories));
                }
            }
        }

        [JsonIgnore]
        public int[,] PreviousGrid { get; set; }
        [JsonIgnore]
        public (int row, int col)? LastCapturePosition { get; set; }



        public GameStats()
        {
            Grid = _grid;

            BlackSurnom = _whiteSurnom;

            WhiteSurnom = _whiteSurnom;

            BlackPlayTimeSeconds = _blackPlayTimeSeconds;
            WhitePlayTimeSeconds = _whitePlayTimeSeconds;

            TotalTime = _totalTime;

            Komi = _komi;

            Handicap = _handicap;

            ActivePlayer = _activePlayer;

            BlackScore = _blackScore;
            WhiteScore = _whiteScore;

            SkippedTurns = _skippedTurns;

            BlackTerritories = _blackTerritories;
            WhiteTerritories = _whiteTerritories;

        }


    }
}