using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     Scenemanager that automatically puts a loading scene inbetween.
/// </summary>
public class Loader : MonoBehaviour
{
    private static Action onLoaderCallback;

    public static void Load(Scene scene) {
        // Set the loader callback action to load the target scene.
        onLoaderCallback = () => {
            SceneManager.LoadScene(scene.ToString());
        };

        // Load the loading scene.
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback() {
        // Triggered after the first Update, which lets the screen refresh.
        // Execute the loader callback action which will load the target scene.
        if (onLoaderCallback != null) {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}

public enum Scene
{
    Menu,
    Highscores,
    Game,
    Loading
}