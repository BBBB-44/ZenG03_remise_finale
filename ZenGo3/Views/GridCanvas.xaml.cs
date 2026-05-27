using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ZenGo3.ViewModels;
using static ZenGo3.GameViewVM;

namespace ZenGo3
{
    public partial class GridCanvas : UserControl
    {
        public GameStats GameStats { get; set; }

        public int[,] Grid
        {
            get => GameStats?.Grid;
            set
            {
                if (GameStats != null)
                {
                    GameStats.Grid = value;
                    RenderGrid();
                }
            }
        }

        private readonly int _cellSize = 50;

        public int[,] TestGrid()
        {
            string[] layout = new string[]
                {
                    "000000000",
                    "012012012",
                    "222111000",
                    "000000000",
                    "000000000",
                    "000000000",
                    "000000000",
                    "000000000",
                    "000000000"
                };

            int[,] Grid = new int[9, 9];

            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    // Convertit le caractère ('0', '1', '2') en sa valeur numérique
                    Grid[r, c] = (int)char.GetNumericValue(layout[r][c]);
                }
            }
            return Grid;
        }

        public int[,] SuicideTestGrid()
        {
            string[] layout = new string[]
                {
                    "021000000",
                    "210000000",
                    "000000000",
                    "000000000",
                    "000000000",
                    "000000000",
                    "000000000",
                    "000000000",
                    "000000000"
                };

            int[,] Grid = new int[9, 9];

            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    // Convertit le caractère ('0', '1', '2') en sa valeur numérique
                    Grid[r, c] = (int)char.GetNumericValue(layout[r][c]);
                }
            }

            GameStats.Grid = Grid;
            return Grid;
        }
        public void ResetGrid()
        {
            //Grid = new int[9, 9];

            GameStats.Grid = new int[9, 9];

            //Grid = GameStats.Grid;

            int CurrentPlayer = GameStats.ActivePlayer;
            GameReset?.Invoke(this, EventArgs.Empty);
        }

        public void SkipTurn()
        {
            GameStats.ActivePlayer = GameStats.ActivePlayer== 1 ? 2 : 1;
        }

        public GridCanvas()
        {
            InitializeComponent();

            GridCanvasX.MouseLeftButtonDown += OnCellClick;

        }
        public void Initialize()
        {
            if (GameStats != null && GameStats.Grid == null)
            {
            }
            RenderGrid();
        }


        // ====================== INPUT ======================
        private void OnCellClick(object sender, MouseButtonEventArgs e)
        {
            if (GameStats?.Grid == null) return;

            Point pos = e.GetPosition(GridCanvasX);
            int col = (int)Math.Round(pos.X / _cellSize);
            int row = (int)Math.Round(pos.Y / _cellSize);

            if (row < 0 || row >= Grid.GetLength(0) || col < 0 || col >= GameStats?.Grid.GetLength(1))
                return;
            if (GameStats?.Grid[row, col] != 0)
                return;

            // Try to make the move
            int capturingPlayer = GameStats.ActivePlayer;
            if (TryMakeMove(GameStats?.Grid, row, col, capturingPlayer, out int[,] newGrid, out int captures))
            {

                GameStats.Grid = newGrid;

                if (captures > 0)
                    StonesCaptured?.Invoke(this, (capturingPlayer, captures));

                GameStats.ActivePlayer = GameStats.ActivePlayer == 1 ? 2 : 1;

                GameStats.OnPropertyChanged(nameof(GameStats.ActivePlayer));

                var (scoreNoir, scoreBlanc, gagnant, blackTerritories, whiteTerritories) = CalculerScore(GameStats.Grid, GameStats.Komi, scoringLogic.Territory, GameStats.BlackCaptures, GameStats.WhiteCaptures);

                GameStats.BlackTerritories = blackTerritories;
                GameStats.WhiteTerritories = whiteTerritories;

            }


            Clicked?.Invoke(this, (row, col));
            e.Handled = true;
        }

        // ====================== GAME LOGIC ======================
        /// <summary>
        /// Returns a new grid with the move applied (and captures done), or false if invalid.
        /// </summary>
        private bool TryMakeMove(int[,] originalGrid, int row, int col, int player, out int[,] result, out int captures)
        {
            result = null;
            captures = 0;
            int[,] grid = (int[,])originalGrid.Clone(); // Work on a copy

            if (!IsValidMove(grid, row, col, player))
                return false;

            if (IsKo(grid, row, col, player))
            {
                //MessageBox.Show("allo");
                return false;
            }

            // Store current state before move
            GameStats.PreviousGrid = (int[,])grid.Clone();

            //if (!IsKo(grid, row, col, player))
            //{
            //    MessageBox.Show("alloooo");
            //}
            // Apply move
            grid[row, col] = player;


            // Remove captured opponent stones
            captures = RemoveCapturedStones(grid, row, col, player);

            //MessageBox.Show(captures.ToString());

            if (captures != 0)
            {

                if (GameStats.ActivePlayer == 1)
                    GameStats.BlackCaptures += captures;
                else
                    GameStats.WhiteCaptures += captures;
            }
            GameStats.SkippedTurns = 0;
            result = grid;
            return true;
        }

        private bool PlaceStone(int[,] grid, int row, int col, int player)
        {
            //int[,] grid = (int[,])originalGrid.Clone(); // Work on a copy

            // Apply move
            grid[row, col] = player;

            return true;
        }


        public void PlaceHandicap2(int handicap)
        {
            if (handicap <= 0) return;

            // 1. Liste de vos coordonnées prédéfinies (X, Y)
            var coordinates = new List<(int X, int Y)>()
                {
                    (2, 2),
                    (2, 6),
                    (4, 4),
                    (6, 2),
                    (6, 6)
                };

            // 2. Générateur de nombres aléatoires
            Random random = new Random();

            // Ajuste le nombre de boucles si le handicap demandé dépasse les coordonnées disponibles
            int stonesToPlace = Math.Min(handicap, coordinates.Count);

            for (int i = 0; i < stonesToPlace; i++)
            {
                // 3. Sélectionne un index aléatoire dans la liste
                int randomIndex = random.Next(coordinates.Count);
                var chosenCoord = coordinates[randomIndex];

                // 4. Supprime la coordonnée pour ne pas la piocher deux fois
                coordinates.RemoveAt(randomIndex);

                //// 5. Affiche le message et place la pierre
                //MessageBox.Show($"Placing handicap stone {i + 1} of {stonesToPlace} at ({chosenCoord.X}, {chosenCoord.Y})");




                PlaceStone(GameStats.Grid, chosenCoord.X, chosenCoord.Y, 1);

                RenderGrid();
            }
        }

        private bool IsKo(int[,] grid, int row, int col, int player)
        {

            if (GameStats.PreviousGrid == null) return false;

            // Simulate the move
            int[,] testGrid = (int[,])grid.Clone();
            testGrid[row, col] = player;

            // Check for captures
            int opponent = player == 1 ? 2 : 1;
            int capturedCount = 0;
            (int captureRow, int captureCol) = (-1, -1);

            foreach (var (dr, dc) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int r = row + dr, c = col + dc;
                if (r >= 0 && r < grid.GetLength(0) && c >= 0 && c < grid.GetLength(1))
                {
                    if (testGrid[r, c] == opponent && !HasLibertyForGroup(testGrid, r, c))
                    {
                        // This would be a single stone capture - potential Ko
                        var group = GetGroup(testGrid, r, c);
                        if (group.Count == 1)
                        {
                            capturedCount++;
                            (captureRow, captureCol) = (r, c);
                        }
                    }
                }
            }

            // Ko condition: single stone captured, and board returns to previous state
            if (capturedCount == 1)
            {
                // Remove the captured stone from test grid
                testGrid[captureRow, captureCol] = 0;

                // Compare with previous board state
                if (MemoryMarshal.CreateReadOnlySpan(ref testGrid[0, 0], testGrid.Length)
               .SequenceEqual(MemoryMarshal.CreateReadOnlySpan(ref GameStats.PreviousGrid[0, 0], GameStats.PreviousGrid.Length)))
                {
                    MessageBox.Show("This is Ko!");
                    return true; // This is Ko!
                }
            }

            return false;
        }


        private List<(int row, int col)> GetGroup(int[,] grid, int row, int col)
        {
            var group = new List<(int, int)>();
            var visited = new bool[grid.GetLength(0), grid.GetLength(1)];
            int player = grid[row, col];

            GetGroupRecursive(grid, row, col, player, visited, group);
            return group;
        }

        private void GetGroupRecursive(int[,] grid, int row, int col, int player,
                                        bool[,] visited, List<(int, int)> group)
        {
            if (row < 0 || row >= grid.GetLength(0) || col < 0 || col >= grid.GetLength(1))
                return;
            if (visited[row, col] || grid[row, col] != player)
                return;

            visited[row, col] = true;
            group.Add((row, col));

            GetGroupRecursive(grid, row - 1, col, player, visited, group);
            GetGroupRecursive(grid, row + 1, col, player, visited, group);
            GetGroupRecursive(grid, row, col - 1, player, visited, group);
            GetGroupRecursive(grid, row, col + 1, player, visited, group);
        }

        public bool IsValidMove(int[,] grid, int row, int col, int player)
        {

            int opponent = player == 1 ? 2 : 1;

            grid[row, col] = player; // temporary

            bool hasLiberty = HasLibertyForGroup(grid, row, col);

            if (!hasLiberty)
            {
                // Check if this move captures opponent
                foreach (var (dr, dc) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    int r = row + dr, c = col + dc;
                    if (r >= 0 && r < grid.GetLength(0) && c >= 0 && c < grid.GetLength(1))
                    {
                        if (grid[r, c] == opponent && !HasLibertyForGroup(grid, r, c))
                        {
                            
                            hasLiberty = true;
                            break;
                        }
                    }
                }
            }


            //if (hasLiberty == false)

            //{
            //    MessageBox.Show("Suicide invalide");
            //}

            grid[row, col] = 0; // revert temporary placement
            return hasLiberty;
        }

        private bool HasLibertyForGroup(int[,] grid, int row, int col)
        {
            if (grid[row, col] == 0) return false;

            var visited = new bool[grid.GetLength(0), grid.GetLength(1)];
            return CountLiberties(grid, row, col, grid[row, col], visited) > 0;
        }

        private int CountLiberties(int[,] grid, int row, int col, int player, bool[,] visited)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            if (row < 0 || row >= rows || col < 0 || col >= cols || visited[row, col])
                return 0;

            if (grid[row, col] == 0) return 1;           // Liberty
            if (grid[row, col] != player) return 0;      // Opponent

            visited[row, col] = true;

            int count = 0;
            count += CountLiberties(grid, row - 1, col, player, visited);
            count += CountLiberties(grid, row + 1, col, player, visited);
            count += CountLiberties(grid, row, col - 1, player, visited);
            count += CountLiberties(grid, row, col + 1, player, visited);

            return count;
        }


        private int RemoveCapturedStones(int[,] grid, int lastRow, int lastCol, int player)
        {
            int opponent = player == 1 ? 2 : 1;
            int removed = 0;

            foreach (var (dr, dc) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int r = lastRow + dr;
                int c = lastCol + dc;

                if (r >= 0 && r < grid.GetLength(0) && c >= 0 && c < grid.GetLength(1))
                {
                    if (grid[r, c] == opponent && !HasLibertyForGroup(grid, r, c))
                    {
                        removed += RemoveGroup(grid, r, c);
                    }
                }
            }
            return removed;
        }

        private int RemoveGroup(int[,] grid, int row, int col)
        {
            if (grid[row, col] == 0) return 0;

            int player = grid[row, col];
            var visited = new bool[grid.GetLength(0), grid.GetLength(1)];
            return RemoveGroupRecursive(grid, row, col, player, visited);
        }

        private int RemoveGroupRecursive(int[,] grid, int row, int col, int player, bool[,] visited)
        {
            if (row < 0 || row >= grid.GetLength(0) || col < 0 || col >= grid.GetLength(1))
                return 0;
            if (visited[row, col] || grid[row, col] != player)
                return 0;

            visited[row, col] = true;
            grid[row, col] = 0;

            int n = 1;
            n += RemoveGroupRecursive(grid, row - 1, col, player, visited);
            n += RemoveGroupRecursive(grid, row + 1, col, player, visited);
            n += RemoveGroupRecursive(grid, row, col - 1, player, visited);
            n += RemoveGroupRecursive(grid, row, col + 1, player, visited);
            return n;
        }

        // ====================== RENDER ======================
        public void RenderGrid()
        {
            if (GameStats?.Grid == null) return;

            int rows = GameStats.Grid.GetLength(0);
            int cols = GameStats.Grid.GetLength(1);

            GridCanvasX.Children.Clear();

            // Horizontal lines
            for (int r = 0; r < rows; r++)
            {
                Line line = new Line
                {
                    X1 = 0,
                    Y1 = r * _cellSize,
                    X2 = (cols - 1) * _cellSize,
                    Y2 = r * _cellSize,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                GridCanvasX.Children.Add(line);
            }

            // Vertical lines
            for (int c = 0; c < cols; c++)
            {
                Line line = new Line
                {
                    X1 = c * _cellSize,
                    Y1 = 0,
                    X2 = c * _cellSize,
                    Y2 = (rows - 1) * _cellSize,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                GridCanvasX.Children.Add(line);
            }

            // Stones
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (GameStats?.Grid[r, c] == 0) continue;

                    Ellipse stone = new Ellipse
                    {
                        Width = _cellSize * 0.85,
                        Height = _cellSize * 0.85,
                        Fill = GameStats?.Grid[r, c] == 1 ? Brushes.Black : Brushes.White,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1.5
                    };

                    Canvas.SetLeft(stone, c * _cellSize - stone.Width / 2);
                    Canvas.SetTop(stone, r * _cellSize - stone.Height / 2);
                    GridCanvasX.Children.Add(stone);
                }
            }

            // Resize canvas
            double boardSize = (Math.Max(rows, cols) - 1) * _cellSize;
            GridCanvasX.Width = boardSize;
            GridCanvasX.Height = boardSize;
        }

        public event Action<object, (int row, int col)> Clicked;
        public event Action<object, int> CurrentPlayerChanged;
        public event Action<object, int> HandicapChanged;
        /// <summary>Player who just played (1 = black, 2 = white), number of opponent stones removed.</summary>
        public event Action<object, (int capturingPlayer, int count)> StonesCaptured;
        public event EventHandler GameReset;


    }
}