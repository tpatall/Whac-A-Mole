using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Controls the enum-based state switching.
///     This class is responsible for setting up the gameplay mechanics and keeping track of score and mole spawning.
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    ///     Total number of currently visible moles.
    /// </summary>
    public int VisibleMoles;

    /// <summary>
    ///     Size of horizontal spacing between hills..
    /// </summary>
    [SerializeField]
    [Tooltip("Size of horizontal spacing between hills.")]
    [Range(1f, 20f)]
    private float xOffset;

    /// <summary>
    ///     Size of vertical spacing between hills..
    /// </summary>
    [SerializeField]
    [Tooltip("Size of vertical spacing between hills.")]
    [Range(1f, 20f)]
    private float yOffset;

    /// <summary>
    ///     Total game duration in seconds.
    /// </summary>
    [SerializeField]
    [Tooltip("Total game duration in seconds. Choose a number from 60 to 300.")]
    [Range(20, 300)]
    private int gameDuration;

    /// <summary>
    ///     Total points one whacked mole is worth.
    /// </summary>
    [SerializeField]
    [Tooltip("Total points that are rewarded upon succesfully whacking a mole.")]
    [Range(1, 100)]
    private int scoreIncrement;

    /// <summary>
    ///     Prefab of a hill.
    /// </summary>
    [SerializeField]
    [Tooltip("The hill prefab which will be instantiated.")]
    private GameObject hillPrefab;

    /// <summary>
    ///     Reference to the timer.
    /// </summary>
    [SerializeField]
    private Stopwatch stopWatch;

    /// <summary>
    ///     Reference to the scoreboard.
    /// </summary>
    [SerializeField]
    private Scoreboard scoreboard;

    /// <summary>
    ///     Reference to the results panel.
    /// </summary>
    [SerializeField]
    private ResultsPanel resultsPanel;

    /// <summary>
    ///     Total time remaining when the previous mole spawned. 
    /// </summary>
    private float lastSpawn;

    /// <summary>
    ///     Whether the game is currently running. Used for managing remaining time.
    /// </summary>
    public bool IsGameRunning { get; private set; }

    public List<Mole> Moles { get; private set; }

    public float TimeRemaining { get; private set; }

    public int Score { get; private set; }

    /// <summary>
    ///     Molehills of the current game.
    /// </summary>
    private int MoleHills { get; set; }

    /// <summary>
    ///     Total time (in seconds) that a mole can be visible before dissapearing.
    /// </summary>
    private float ShowTime { get; set; }

    /// <summary>
    ///     Time between spawning a new mole, in seconds.
    /// </summary>
    private float TimeBetweenSpawn { get; set; }

    /// <summary>
    ///     Initialization of game variables.
    /// </summary>
    /// <param name="difficulty">Chosen difficulty</param>
    public void SetUp(Difficulty difficulty) {
        lastSpawn = gameDuration - 3;
        IsGameRunning = false;
        TimeRemaining = gameDuration;
        Score = 0;

        resultsPanel.SaveDifficulty(difficulty);
        stopWatch.SetUp(TimeRemaining);
        scoreboard.UpdateScore(Score);

        InterpretDifficulty(difficulty);
        GenerateMoleHills();
    }
    
    /// <summary>
    ///     Convert difficulty enum to actual rules.
    /// </summary>
    /// <param name="difficulty">Chosen difficulty.</param>
    private void InterpretDifficulty(Difficulty difficulty) {
        // Default number of molehills
        int molehills = 1;

        // Default time a mole can be shown
        float showTime = 1f;

        // Default time between spawning of moles
        int timeBetweenSpawn = 2;

        switch (difficulty) {
            case Difficulty.EASY:
                molehills = 6;
                showTime = 2f;
                timeBetweenSpawn = 3;
                break;
            case Difficulty.MEDIUM:
                molehills = 9;
                showTime = 1.75f;
                timeBetweenSpawn = 2;
                break;
            case Difficulty.HARD:
                molehills = 12;
                showTime = 1.5f;
                timeBetweenSpawn = 1;
                break;
            default:
                break;
        }

        MoleHills = molehills;
        ShowTime = showTime;
        TimeBetweenSpawn = timeBetweenSpawn;
    }

    /// <summary>
    ///     Called when the player presses the Start-button.
    /// </summary>
    public void StartGame() {
        IsGameRunning = true;
        stopWatch.StartStopwatch();
    }

    /// <summary>
    ///     Called when the time remaining is below 0.
    /// </summary>
    public void EndGame() {
        IsGameRunning = false;
        stopWatch.StopStopwatch();

        resultsPanel.InitializeResults(Score);

        GameManager.Instance.UpdateGameState(GameState.TearDown);
    }

    /// <summary>
    ///     When a mole is correctly clicked when it is visible.
    /// </summary>
    /// <param name="mole"></param>
    public void WhackMole(Mole mole) {
        mole.Whacked();

        Score += scoreIncrement;
        scoreboard.UpdateScore(Score);
    }

    void Update() {
        if (IsGameRunning) {
            TimeRemaining -= Time.deltaTime;

            // If the time remaining is less than the time of the last mole spawn minus the time allowed between spawns.
            if (TimeRemaining < (lastSpawn - TimeBetweenSpawn)) {
                // Dont spawn a mole when there already are 3 moles visible.
                if (VisibleMoles < 3) {
                    SpawnMole();
                    lastSpawn = TimeRemaining;
                }
            }

            if (TimeRemaining <= 0) {
                EndGame();
            }
        }
    }

    /// <summary>
    ///     Instantiate a prefabricated mole hill gameobject.
    /// </summary>
    private void GenerateMoleHills() {
        Moles = new List<Mole>();

        List<int> moleHills = BoardUtils.GenerateOptimalLayout(MoleHills);

        // Get the additional offset of the y-coordinate of the first row amount of rows, so that the field is still centered.
        float extraYOffset;
        if (moleHills.Count % 2 == 0) {
            extraYOffset = -yOffset * (moleHills.Count / 2) / 2;
        }
        else {
            extraYOffset = -yOffset * (moleHills.Count / 2);
        }

        // Change the y-coordinate of the startposition based off the amount of rows, so that the overall field is centered
        float startPosY = extraYOffset + yOffset * (moleHills.Count - 1);

        // Loop over number of rows
        for (int i = 0; i < moleHills.Count; i++) {
            int hillsInRow = moleHills[i];

            // Get the additional offset of the x-coordinate of the first hill in the row
            // based off the amount of hills in the row, so that the row is still centered.
            float extraXOffset;
            if (hillsInRow % 2 == 0) {
                extraXOffset = (xOffset + -xOffset * hillsInRow) / 2;
            } else {
                extraXOffset = -xOffset * (hillsInRow / 2);
            }

            // Loop over hills in a row
            for (int j = 0; j < hillsInRow; j++) {

                Vector2 worldPosition = new Vector2(extraXOffset + (j * xOffset), startPosY - (i * yOffset));

                var hillObject = Instantiate(hillPrefab);
                hillObject.transform.SetParent(gameObject.transform);
                hillObject.transform.position = worldPosition;

                Mole mole = hillObject.GetComponentInChildren<Mole>();
                mole.SetUp(this, ShowTime);
                Moles.Add(mole);
            }
        }
    }

    /// <summary>
    ///     Enable a random mole. The mole will disable itself after its time is up.
    /// </summary>
    private void SpawnMole() {
        Mole mole = null;

        while (mole == null) {
            mole = Moles[Random.Range(0, Moles.Count)];

            if (mole.IsVisible) {
                mole = null;
            }
        }

        mole.Show();
    }
}
