using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
///     A stopwatch times how long a game is taking. Also has a startup countdown.
/// </summary>
public class Stopwatch : MonoBehaviour
{
    /// <summary>
    ///     Reference to the Text field where the stopwatch is shown.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI currentTimeText;

    /// <summary>
    ///     Whether the stopwatch is active.
    /// </summary>
    private bool stopwatchActive = false;

    /// <summary>
    ///     Public property to see the time.
    /// </summary>
    public float CurrentTime { get; private set; }

    /// <summary>
    ///     SetUp this instance of the stopwatch class.
    /// </summary>
    /// <param name="timeRemaining">Time remaining as set in the game controller.</param>
    public void SetUp(float timeRemaining) {
        CurrentTime = timeRemaining;
    }

    // Update is called once per frame
    void Update() {
        if (stopwatchActive) {
            if (CurrentTime > 0) {
                CurrentTime -= Time.deltaTime;

                // Fade to red for the last 20 seconds to indicate time running out.
                if (CurrentTime > 15) {
                    currentTimeText.color = Color.black;
                }
                else if (CurrentTime > 10) {
                    float progress = 1f / (CurrentTime - 15);
                    currentTimeText.color = new Color(0f + progress, 0f, 0f, 1f);
                }
                else {
                    currentTimeText.color = Color.red;
                }
            }

            TimeSpan time = TimeSpan.FromSeconds(CurrentTime);
            currentTimeText.text = time.ToString(@"ss");
        }
    }

    /// <summary>
    ///     Resume the stopwatch.
    /// </summary>
    public void StartStopwatch() {
        stopwatchActive = true;
    }

    /// <summary>
    ///     Pause the stopwatch.
    /// </summary>
    public void StopStopwatch() {
        stopwatchActive = false;
    }
}
