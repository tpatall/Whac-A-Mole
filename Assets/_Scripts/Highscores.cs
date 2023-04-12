using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
///     Manages the highscore file(s).
///     This class is responsible for loading the highscores, adding new scores, and saving it to the file.
/// </summary>
public static class HighScores
{
    /// <summary>
    ///     Update and sort the highscore list when a new score is submitted.
    /// </summary>
    /// <param name="playerName">Name of the player.</param>
    /// <param name="score">Score the player achieved.</param>
    /// <param name="difficulty">The chosen game difficulty.</param>
    public static void UpdateHighScores(string playerName, int score, Difficulty difficulty) {
        List<HighScoreEntry> highScores = LoadHighScores(difficulty);

        HighScoreEntry playerScore = new HighScoreEntry { Name = playerName, Score = score };
        highScores.Add(playerScore);
        highScores = highScores.OrderByDescending(x => x.Score).ToList();
        SaveHighScores(highScores, difficulty);
    }

    /// <summary>
    ///     Load highscores from the highscore-file.
    /// </summary>
    /// <param name="difficulty">The chosen game difficulty.</param>
    /// <returns>A list of scores.</returns>
    public static List<HighScoreEntry> LoadHighScores(Difficulty difficulty) {
        List<HighScoreEntry> highScores = new List<HighScoreEntry>();

        string filePath = GetFilePath(difficulty);

        if (File.Exists(filePath)) {
            string[] scoreLines = File.ReadAllLines(filePath);
            foreach (string line in scoreLines) {
                string[] parts = line.Split(',');
                if (parts.Length == 2) {
                    string name = parts[0];
                    int score = int.Parse(parts[1]);
                    highScores.Add(new HighScoreEntry { Name = name, Score = score });
                }
            }
        }

        return highScores;
    }

    /// <summary>
    ///     Save the list of highscores to the file.
    /// </summary>
    /// <param name="highScores">The list of highscores.</param>
    /// <param name="difficulty">The chosen game difficulty.</param>
    public static void SaveHighScores(List<HighScoreEntry> highScores, Difficulty difficulty) {
        string filePath = GetFilePath(difficulty);

        using (StreamWriter writer = new StreamWriter(filePath)) {
            foreach (HighScoreEntry score in highScores) {
                writer.WriteLine("{0},{1}", score.Name, score.Score);
            }
        }
    }

    /// <summary>
    ///     Get the (hardcoded) file from the filepath.
    /// </summary>
    /// <returns>The filepath.</returns>
    private static string GetFilePath(Difficulty difficulty) {
        string prefix = "";
        switch (difficulty) {
            case Difficulty.EASY:
                prefix = "easy";
                break;
            case Difficulty.MEDIUM:
                prefix = "medium";
                break;
            case Difficulty.HARD:
                prefix = "hard";
                break;
        }

        string filename = "\\" + prefix + "highscores.txt";
        string filePath = Application.dataPath + filename;
        return filePath;
    }
}
