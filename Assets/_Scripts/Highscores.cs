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
    public void UpdateHighScores(string playerName, int score) {
        List<PlayerScore> highScores = LoadHighScores();

        PlayerScore playerScore = new PlayerScore { Name = playerName, Score = score };
        highScores.Add(playerScore);
        highScores = highScores.OrderByDescending(x => x.Score).ToList();
        SaveHighScores(highScores);
    }

    /// <summary>
    ///     Load highscores from the highscore-file.
    /// </summary>
    /// <returns>A list of scores.</returns>
    public List<PlayerScore> LoadHighScores() {
        List<PlayerScore> highScores = new List<PlayerScore>();

        string filePath = GetFilePath();

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
    /// <param name="highScores"></param>
    public void SaveHighScores(List<PlayerScore> highScores) {
        string filePath = GetFilePath();

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
    private string GetFilePath() {
        string filename = "highscores.txt";
        string filePath = Application.dataPath + filename;
        return filePath;
    }

}

public class PlayerScore
{
    public string Name { get; set; }
    public int Score { get; set; }
}
