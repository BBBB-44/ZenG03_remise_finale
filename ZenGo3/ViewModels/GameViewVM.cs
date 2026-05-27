using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ZenGo3.Services;
using ZenGo3.ViewModels;
using System.Windows.Documents;
using System.Numerics;

namespace ZenGo3
{
    public class GameViewVM
    {
        public GameStats GameStats { get; set; }

        const int Taille = 9;

        const char Vide = (char)0;

        const char Noir = (char)1;

        const char Blanc = (char)2;

        public enum scoringLogic
        {
            Area,
            Territory
        }

        public GameViewVM()
        {
            GameStats = new GameStats();       
        }
   
        private void OnBoardGameReset(object sender, EventArgs e)
        {
            ResetPlayerStats();
        }

        public void ResetPlayerStats()
        {
            GameStats.BlackPlayTimeSeconds = TimeSpan.Zero;
            GameStats.WhitePlayTimeSeconds = TimeSpan.Zero;
            GameStats.BlackCaptures = 0;
            GameStats.WhiteCaptures = 0;

            GameStats = new GameStats();
        }      

        // CALCULER SCORE - AREA AND TERRITORY SCORING
        public static (double scoreNoir, double scoreBlanc, string gagnant, int blackTerritories, int whiteTerritories) CalculerScore(int[,] plateau, double komi, scoringLogic scoring, int captureTotaleNoir, int captureTotaleBlanc)
        {

            double scoreNoir = 0.0;
            double scoreBlanc = 0.0;

            scoreNoir += captureTotaleNoir;
            scoreBlanc += captureTotaleBlanc;

            int whiteTerritories = 0;
            int blackTerritories = 0;

            // Count stones
            int stonesNoir = 0;
            int stonesBlanc = 0;
            for (int i = 0; i < Taille; i++)
            {
                for (int j = 0; j < Taille; j++)
                {
                    if (plateau[i, j] == Noir) stonesNoir++;
                    else if (plateau[i, j] == Blanc) stonesBlanc++;
                }
            }


            //Area scoring in Go counts the empty points you surround plus the stones you still have on the board at the end; captured stones are returned to the bowl and not counted as points.
            if (scoring == scoringLogic.Area)
            {
                // ===== AREA SCORING (Chinese) =====
                // Score = Stones + Territory
                scoreNoir = stonesNoir;
                scoreBlanc = stonesBlanc;

                bool[,] visited = new bool[Taille, Taille];

                for (int i = 0; i < Taille; i++)
                {
                    for (int j = 0; j < Taille; j++)
                    {
                        if (plateau[i, j] == Vide && !visited[i, j])
                        {
                            var (size, owner) = DeterminerTerritorieArea(plateau, i, j, visited);

                            Console.WriteLine($"\nzone : {size}\n");

                            if (owner == Noir)
                                scoreNoir += size;
                            else if (owner == Blanc)
                                scoreBlanc += size;
                        }
                    }
                }
            }

            //Territory scoring counts the empty points you surround, then adds your captured stones/ prisoners as points; stones already on the board are not counted directly.
            else
            {
                // ===== TERRITORY SCORING (Japanese) =====
                // Score = Territory only
                bool[,] visited = new bool[Taille, Taille];

                for (int i = 0; i < Taille; i++)
                {
                    for (int j = 0; j < Taille; j++)
                    {
                        if (plateau[i, j] == Vide && !visited[i, j])
                        {
                            var (size, owner) = DeterminerTerritorieArea(plateau, i, j, visited);

                            if (owner == Noir)
                            {
                                scoreNoir += size;
                                blackTerritories += size;
                            }
                            else if (owner == Blanc)
                            {
                                scoreBlanc += size;
                                whiteTerritories += size;
                            }
                        }
                    }
                }
            }


            //A quick example: if you own 20 empty points and have 30 living stones on the board, area scoring gives you 50 points, while territory scoring would focus on the 20 empty points and your prisoners instead.


            // Apply komi (White compensation)
            scoreBlanc += komi;

            // Determine winner
            string gagnant = "";
            if (scoreNoir > scoreBlanc)
                gagnant = "Noir";
            else if (scoreBlanc > scoreNoir)
                gagnant = "Blanc";
            else
                gagnant = "Égalité";

            Console.WriteLine("");
            Console.WriteLine($"Pierre blanche placée {stonesBlanc}");
            Console.WriteLine($" noir capturé: {captureTotaleNoir}");
            Console.WriteLine($"komi : {komi}");
            //Console.WriteLine($"{}");
            Console.WriteLine($"Pierre noire placée {stonesNoir}");
            Console.WriteLine($" blanc capturé : {captureTotaleBlanc}");

            //GameStats.BlackScore = scoreNoir;


            return (scoreNoir, scoreBlanc, gagnant, blackTerritories, whiteTerritories);
        }

        // DETERMINE TERRITORY OWNERSHIP (BFS approach)
        public static (int size, int owner) DeterminerTerritorieArea(
             int[,] plateau,
             int startX,
             int startY,
             bool[,] visited)
        {

            if (plateau[startX, startY] != Vide)
                return (0, Vide);
            HashSet<int> adjacentColors = new HashSet<int>();
            Queue<(int, int)> queue = new Queue<(int, int)>();
            int territorySize = 0;

            queue.Enqueue((startX, startY));
            visited[startX, startY] = true;

            // BFS to find all connected empty spaces
            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                territorySize++;

                // Check 4 directions
                int[] dx = { -1, 1, 0, 0 };
                int[] dy = { 0, 0, -1, 1 };

                for (int dir = 0; dir < 4; dir++)
                {
                    int nx = x + dx[dir];
                    int ny = y + dy[dir];

                    if (nx >= 0 && nx < Taille && ny >= 0 && ny < Taille)
                    {
                        if (plateau[nx, ny] == Vide && !visited[nx, ny])
                        {
                            visited[nx, ny] = true;
                            queue.Enqueue((nx, ny));
                        }
                        else if (plateau[nx, ny] != Vide)
                        {
                            adjacentColors.Add((char)plateau[nx, ny]);
                        }
                    }
                }
            }

            // Territory owner: only one color adjacent
            char owner = Vide;
            if (adjacentColors.Count == 1)
            {
                foreach (char color in adjacentColors)
                    owner = color;
            }
            // If 2+ colors or 0 colors → neutral (not counted)

            if (territorySize == plateau.Length -1)
            {
                territorySize = 0;
            }

            return (territorySize, owner);
        }

    }
}
