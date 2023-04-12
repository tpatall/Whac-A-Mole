using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoresPanel : MonoBehaviour
{
    [SerializeField]
    private Button easyButton, mediumButton, hardButton;

    [SerializeField]
    private HighscoresTable highscoresTable;

    public void Back() {
        Loader.Load(Scene.Menu);
    }

    public void GetEasyHighscores() {
        highscoresTable.ShowTable(Difficulty.EASY);
    }

    public void GetMediumHighscores() {
        highscoresTable.ShowTable(Difficulty.MEDIUM);
    }

    public void GetHardHighscores() {
        highscoresTable.ShowTable(Difficulty.HARD);
    }
}