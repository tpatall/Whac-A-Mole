using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
    public void StartGame() {
        Loader.Load(Scene.Game);
    }

    public void ViewHighscores() {
        Loader.Load(Scene.Highscores);
    }

    public void Quit() {
        Application.Quit();
    }
}
