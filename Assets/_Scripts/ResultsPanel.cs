using TMPro;
using UnityEngine;

/// <summary>
///     Handles the Results-panel that contains the UI-elements that need this functionality.
/// </summary>
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
    private TextMeshProUGUI errorText;

    /// <summary>
    ///     The final score.
    /// </summary>
    private int finalScore;

    /// <summary>
    ///     The chosen difficulty.
    /// </summary>
    private Difficulty difficulty;

    /// <summary>
    ///     Save the difficulty for choosing the right highscore list.
    /// </summary>
    /// <param name="difficulty">The chosen difficulty setting.</param>
    public void SaveDifficulty(Difficulty difficulty) {
        this.difficulty = difficulty;
    }

    /// <summary>
    ///     Update the achieved final score.
    /// </summary>
    /// <param name="score">The final score.</param>
    public void InitializeResults(int score) {
        finalScore = score;
        finalScoreText.text = finalScore.ToString();
    }

    /// <summary>
    ///     Reloads the Game-scene after checking if the score can be submitted.
    /// </summary>
    public void PlayAgain() {
        CheckAndSubmit(Scene.Game);
    }

    /// <summary>
    ///     Loads the Menu-scene after checking if the score can be submitted.
    /// </summary>
    public void Menu() {
        CheckAndSubmit(Scene.Menu);
    }

    private void Start() {
        errorText.text = "To submit a score you need to give a name. If you do not want to save your score, leave the name empty.";
    }

    /// <summary>
    ///     Submit the score and player-name when the latter is filled in. Load the next scene afterwards.
    /// </summary>
    /// <param name="scene">The scene to load.</param>
    private void CheckAndSubmit(Scene scene) {
        if (inputField.text != "") {
            SaveScore();
        }
        Loader.Load(scene);
    }

    /// <summary>
    ///     Call the Highscores-class to update the file.
    /// </summary>
    private void SaveScore() {
        HighScores.UpdateHighScores(inputField.text, finalScore, difficulty);
    }
}
