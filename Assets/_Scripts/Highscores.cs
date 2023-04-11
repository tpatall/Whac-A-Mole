using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Highscores
{
    /// <summary>
    ///     Update and sort the highscore list when a new score is submitted.
    /// </summary>
    /// <param name="playerName">Name of the player.</param>
    /// <param name="score">Score the player achieved.</param>
    /// <param name="difficulty">The chosen game difficulty.</param>
    public void UpdateHighScores(string playerName, int score, Difficulty difficulty) {
        List<PlayerScore> highScores = LoadHighScores(difficulty);

        PlayerScore playerScore = new PlayerScore { Name = playerName, Score = score };
        highScores.Add(playerScore);
        highScores = highScores.OrderByDescending(x => x.Score).ToList();
        SaveHighScores(highScores, difficulty);
    }

    /// <summary>
    ///     Load highscores from the highscore-file.
    /// </summary>
    /// <param name="difficulty">The chosen game difficulty.</param>
    /// <returns>A list of scores.</returns>
    public List<PlayerScore> LoadHighScores(Difficulty difficulty) {
        List<PlayerScore> highScores = new List<PlayerScore>();

        string filePath = GetFilePath(difficulty);

        if (File.Exists(filePath)) {
            string[] scoreLines = File.ReadAllLines(filePath);
            foreach (string line in scoreLines) {
                string[] parts = line.Split(',');
                if (parts.Length == 2) {
                    string name = parts[0];
                    int score = int.Parse(parts[1]);
                    highScores.Add(new PlayerScore { Name = name, Score = score });
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
    public void SaveHighScores(List<PlayerScore> highScores, Difficulty difficulty) {
        string filePath = GetFilePath(difficulty);

        using (StreamWriter writer = new StreamWriter(filePath)) {
            foreach (PlayerScore score in highScores) {
                writer.WriteLine("{0},{1}", score.Name, score.Score);
            }
        }
    }

    /// <summary>
    ///     Get the (hardcoded) file from the filepath.
    /// </summary>
    /// <returns>The filepath.</returns>
    private string GetFilePath(Difficulty difficulty) {
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

public class PlayerScore
{
    public string Name { get; set; }
    public int Score { get; set; }
}
