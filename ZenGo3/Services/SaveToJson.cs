using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;

namespace ZenGo3.Services
{
    public class SaveToJson
    {
        /// <summary>
        /// Convert a 2D array to a jagged array for JSON serialization.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="length"></param>
        // Change ArrayToJaggedArray to return the array, NOT a string
        public int[][] ArrayToJaggedArray(int[,] grid, int length)
        {
            int[][] jaggedGrid = new int[length][];
            for (int i = 0; i < length; i++)
            {
                jaggedGrid[i] = new int[length];
                for (int j = 0; j < length; j++)
                {
                    jaggedGrid[i][j] = grid[i, j];
                }
            }
            return jaggedGrid;  // Return the array itself, not a string
        }

        public void Save(string json)
        {
            // Nettoyage de l'indentation de la grille (vos Regex inchangées)
            json = System.Text.RegularExpressions.Regex.Replace(json, @"(\[\s*)\n\s+(\d+)", "[ $2");
            json = System.Text.RegularExpressions.Regex.Replace(json, @",\s*\n\s+(\d+)", ", $1");
            json = System.Text.RegularExpressions.Regex.Replace(json, @"(\d+)\s*\n\s+(\])", "$1 ]");

            string filePath = "grid.json";

            if (File.Exists(filePath))
            {
                // 1. Lire tout le fichier existant
                string currentContent = File.ReadAllText(filePath).Trim();

                // 2. Retirer le crochet fermant ']' à la fin du fichier s'il existe
                if (currentContent.EndsWith("]"))
                {
                    currentContent = currentContent.Substring(0, currentContent.Length - 1).TrimEnd();
                }

                // 3. Recomposer le fichier : contenu existant + virgule + nouveau JSON + crochet fermant
                string updatedContent = currentContent + ",\n" + json + "\n]";
                File.WriteAllText(filePath, updatedContent);
            }
            else
            {
                // 1. Premier enregistrement : on entoure le JSON avec des crochets [ ]
                string initialContent = "[\n" + json + "\n]";
                File.WriteAllText(filePath, initialContent);
            }
        }


    }
}
