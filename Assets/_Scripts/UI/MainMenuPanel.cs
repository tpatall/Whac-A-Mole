using UnityEngine;

/// <summary>
///     Handles the MainMenu-panel that contains the UI-elements that need this functionality.
/// </summary>
public class MainMenuPanel : MonoBehaviour
{
    /// <summary>
    ///     Loads the Game-scene.
    /// </summary>
    public void StartGame() {
        Loader.Load(Scene.Game);
    }

    /// <summary>
    ///     Loads the Highscores-scene.
    /// </summary>
    public void ViewHighscores() {
        Loader.Load(Scene.Highscores);
    }

    /// <summary>
    ///     Quits the application.
    /// </summary>
    public void Quit() {
        Application.Quit();
    }
}
