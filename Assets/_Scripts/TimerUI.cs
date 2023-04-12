using System;
using UnityEngine;
using TMPro;

/// <summary>
///     Handles the timer that is displayed in the UI.
/// </summary>
public class TimerUI : MonoBehaviour
{
    /// <summary>
    ///     Fade timer color to red over 5 seconds to indicate time running out.
    /// </summary>
    [SerializeField]
    [Tooltip("Fade timer color to red over 5 seconds to indicate time running out. Choose a starting point of seconds left.")]
    [Range(0, 30)]
    private int fadeTimerColor;

    /// <summary>
    ///     Reference to the Text field where the stopwatch is shown.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI currentTimeText;

    /// <summary>
    ///     Reference to the game controller that manages the passage of time.
    /// </summary>
    [SerializeField]
    private GameController gameController;

    /// <summary>
    ///     Keep track of the time without calling gameController a lot.
    /// </summary>
    private float timeRemaining = 0f;

    // Update is called once per frame
    void Update() {
        if (gameController.IsGameRunning) {
            timeRemaining = gameController.TimeRemaining;
            if (timeRemaining > 0) {
                // Fade to red for the last 20 seconds to indicate time running out.
                if (timeRemaining < fadeTimerColor) {
                    if (timeRemaining > (fadeTimerColor - 5)) {
                        float progress = 1f / (timeRemaining - 10);
                        Color fadingRed = new Color(0f + progress, 0f, 0f, 1f);

                        currentTimeText.faceColor = fadingRed;
                        currentTimeText.color = fadingRed;
                    }
                    else {
                        currentTimeText.faceColor = Color.red;
                        currentTimeText.color = Color.red;
                    }
                }
            }

            TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
            currentTimeText.text = time.ToString(@"ss");
        }
    }
}
