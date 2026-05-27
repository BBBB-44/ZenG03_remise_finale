using Xunit;
using System;
using ZenGo3.ViewModels;
using ZenGo3;
using System.Windows;


namespace TestProject1
{
    public class GoScoringTests
    {
        private const int Noir = 1;
        private const int Blanc = 2;
        private const int Vide = 0;
        private const int Taille = 9;


        [Fact]
        public void CalculerScore_TerritoryScoring_Komi_Classic()
        {
            int[,] grid = new int[Taille, Taille];
            // Exemple simple : Noir contrôle le coin supérieur gauche
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (i == 0 || j == 0) grid[i, j] = Noir; // bord noir


            var result = GameViewVM.CalculerScore(grid, 6.5, GameViewVM.scoringLogic.Territory, 2, 0);


            Assert.True(result.scoreNoir > 0);
            Assert.True(result.scoreBlanc >= 6.5);
            Assert.Contains("Noir", result.gagnant); // ou Blanc selon territoire
        }


        [Fact]
        public void CalculerScore_AreaScoring_VS_TerritoryScoring()
        {
            int[,] grid = new int[9, 9];
            // Quelques pierres
            grid[1, 1] = Noir;
            grid[1, 2] = Noir;
            grid[3, 3] = Blanc;


            var territory = GameViewVM.CalculerScore(grid, 6.5, GameViewVM.scoringLogic.Territory, 0, 0);
            var area = GameViewVM.CalculerScore(grid, 6.5, GameViewVM.scoringLogic.Area, 0, 0);


            Assert.NotEqual(territory.scoreNoir, area.scoreNoir);
            // En Area, les pierres sur le plateau sont comptées
            Assert.True(area.scoreNoir > territory.scoreNoir);
        }

        [Fact]
        public void DeterminerTerritorieArea_NeutralZone_ReturnsOwnerZero()
        {
            int[,] grid = new int[9, 9];

            // Création d'une petite zone neutre (1 case vide touchant Noir ET Blanc)
            grid[1, 2] = Noir;
            grid[2, 1] = Blanc;
            grid[2, 3] = Noir;
            grid[3, 2] = Blanc;

            // La case (2,2) est vide et entourée des deux couleurs
            bool[,] visited = new bool[9, 9];

            var (size, owner) = GameViewVM.DeterminerTerritorieArea(grid, 2, 2, visited);

            Assert.Equal(1, size);
            Assert.Equal(Vide, owner);
        }


        [Fact]
        public void CalculerScore_PlateauVide_BlancGagneAvecKomi()
        {
            int[,] grid = new int[Taille, Taille]; // tout vide


            var result = GameViewVM.CalculerScore(grid, 6.5, GameViewVM.scoringLogic.Territory, 0, 0);


            Assert.Equal(6.5, result.scoreBlanc);
            Assert.Equal(0, result.scoreNoir);
            Assert.Equal("Blanc", result.gagnant);
        }
    }


    public class GoMoveValidationTests
    {
        [StaFact]
        public void IsValidMove_SuicideSimple_ReturnsFalse()
        {
            var canvas = new GridCanvas();


            int[,] grid = new int[9, 9];
            // Entourer une case
            grid[1, 2] = 2; grid[2, 1] = 2; grid[2, 3] = 2; grid[3, 2] = 2;


            bool valid = canvas.IsValidMove(grid, 2, 2, 1); // Noir essaie de jouer au centre


            Assert.False(valid);
        }


        [StaFact]
        public void IsValidMove_CaptureOpponents_ReturnsTrue()
        {
            var canvas = new GridCanvas();
            int[,] grid = new int[9, 9];
            // Groupe blanc avec 1 liberté
            grid[2, 2] = 2;
            grid[1, 2] = 1; grid[2, 1] = 1; grid[2, 3] = 1;


            bool valid = canvas.IsValidMove(grid, 3, 2, 1); // Noir capture


            Assert.True(valid);
        }


        [StaFact]
        public void IsValidMove_NormalMoveWithLiberty_ReturnsTrue()
        {
            var canvas = new GridCanvas();
            int[,] grid = new int[9, 9];
            bool valid = canvas.IsValidMove(grid, 4, 4, 1);

            Assert.True(valid);
        }


        [StaFact]
        public void IsValidMove_OccupiedCell_ReturnsFalse()
        {
            var canvas = new GridCanvas();
            int[,] grid = new int[9, 9];
            grid[4, 4] = 1;


            bool valid = canvas.IsValidMove(grid, 4, 4, 2);

            Assert.Equal(grid[4, 4], 0); // la case doit rester occupée
        }
    }
}