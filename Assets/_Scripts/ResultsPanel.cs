using TMPro;
using UnityEngine;

public class ResultsPanel : MonoBehaviour
{
    /// <summary>
    ///     Reference to the Text field where the final score is shown.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI finalScoreText;

    /// <summary>
    ///     Reference to the input field for the player name.
    /// </summary>
    [SerializeField]
    private TMP_InputField inputField;

    /// <summary>
    ///     Reference to the Text field where an error could be shown if the input is incorrect.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI inputFieldError;

    /// <summary>
    ///     The final score.
    /// </summary>
    private int finalScore;

    /// <summary>
    ///     Update the achieved final score.
    /// </summary>
    /// <param name="score">The final score.</param>
    public void UpdateScore(int score) {
        finalScore = score;
        finalScoreText.text = finalScore.ToString();
    }

    /// <summary>
    ///     Submit the score and player-name when the latter is filled in.
    /// </summary>
    public void Submit() {
        if (inputField.text == "") {
            inputFieldError.text = "To submit a score you need to give a name.";
        }
        else {
            inputFieldError.text = "";
            SaveScore();
        }
    }

    /// <summary>
    ///     Call the Highscores-class to update the file.
    /// </summary>
    private void SaveScore() {
        Highscores highscores = new Highscores();
        highscores.UpdateHighScores(inputField.text, finalScore);
    }
}
