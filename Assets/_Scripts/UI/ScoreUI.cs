using UnityEngine;
using TMPro;

/// <summary>
///     Handles the score that is displayed in the UI.
/// </summary>
public class ScoreUI : MonoBehaviour
{
    /// <summary>
    ///     Reference to the Text field where the score is shown.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI currentScoreText;

    /// <summary>
    ///     Reference to the game controller that manages the passage of time.
    /// </summary>
    [SerializeField]
    private GameController gameController;

    /// <summary>
    ///     Keep track of the score without calling gameController a lot.
    /// </summary>
    private int score = 0;

    // Update is called once per frame.
    private void Update() {
        if (gameController.Score != score) {
            UpdateScore(gameController.Score);
        }
    }

    /// <summary>
    ///     Update the currently displayed score to the newly achieved score.
    /// </summary>
    /// <param name="score">Score as set in the game controller.</param>
    private void UpdateScore(int score) {
        currentScoreText.text = score.ToString();
        this.score = score;
    }
}