using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    /// <summary>
    ///     Reference to the Text field where the score is shown.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI currentScoreText;

    void Start() {
        currentScoreText.color = Color.black;
    }

    /// <summary>
    ///     SetUp this instance of the scoreboard class.
    /// </summary>
    /// <param name="score">Score as set in the game controller.</param>
    public void UpdateScore(int score) {
        currentScoreText.text = score.ToString();
    }
}