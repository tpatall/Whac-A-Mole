using UnityEngine;

/// <summary>
///     Handles the UI-panel that contains the UI-elements that need this functionality.
/// </summary>
public class HighScoresPanel : MonoBehaviour
{
    /// <summary>
    ///     Reference to the highScoreTable-element.
    /// </summary>
    [SerializeField]
    private HighScoresTable highscoresTable;

    /// <summary>
    ///     Loads the Menu-scene.
    /// </summary>
    public void Back() {
        Loader.Load(Scene.Menu);
    }

    /// <summary>
    ///     Loads the highScores of the lowest difficulty.
    /// </summary>
    public void GetEasyHighscores() {
        highscoresTable.ShowTable(Difficulty.EASY);
    }

    /// <summary>
    ///     Loads the highScores of the middle difficulty.
    /// </summary>
    public void GetMediumHighscores() {
        highscoresTable.ShowTable(Difficulty.MEDIUM);
    }

    /// <summary>
    ///     Loads the highScores of the highest difficulty.
    /// </summary>
    public void GetHardHighscores() {
        highscoresTable.ShowTable(Difficulty.HARD);
    }
}