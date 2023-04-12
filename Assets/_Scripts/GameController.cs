using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     This class is responsible for setting up the gameplay mechanics and keeping track of score and mole spawning.
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    ///     Total number of currently visible moles.
    /// </summary>
    public int VisibleMoles;

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

    private GameBoard gameBoard;

    public List<Mole> Moles => gameBoard.Moles;

    public float TimeRemaining { get; private set; }
    /// <summary>
    ///     Whether the game is currently running. Used for managing remaining time.
    /// </summary>
    public bool IsGameRunning { get; private set; }


    public int Score { get; private set; }

    /// <summary>
    ///     Time between spawning a new mole, in seconds.
    /// </summary>
    private float TimeBetweenSpawn { get; set; }

    /// <summary>
    ///     Initialization of game variables.
    /// </summary>
    /// <param name="difficulty">Chosen difficulty</param>
    public void SetUp(Difficulty difficulty) {
        lastSpawn = gameDuration;
        IsGameRunning = false;
        TimeRemaining = gameDuration;
        Score = 0;

        resultsPanel.SaveDifficulty(difficulty);
        scoreboard.UpdateScore(Score);

        InterpretDifficulty(difficulty);
    }
    
    /// <summary>
    ///     Convert difficulty enum to actual rules.
    /// </summary>
    /// <param name="difficulty">Chosen difficulty.</param>
    private void InterpretDifficulty(Difficulty difficulty) {
        // Default number of molehills.
        int moleHills = 1;

        // Default time a mole can be shown.
        float showTime = 1f;

        // Default time between spawning of moles.
        int timeBetweenSpawn = 2;

        switch (difficulty) {
            case Difficulty.EASY:
                moleHills = 6;
                showTime = 2f;
                timeBetweenSpawn = 3;
                break;
            case Difficulty.MEDIUM:
                moleHills = 9;
                showTime = 1.75f;
                timeBetweenSpawn = 2;
                break;
            case Difficulty.HARD:
                moleHills = 12;
                showTime = 1.5f;
                timeBetweenSpawn = 1;
                break;
            default:
                break;
        }

        TimeBetweenSpawn = timeBetweenSpawn;

        // Create a gameboard that instantiates and maintains the mole hills on the board.
        gameBoard = new GameBoard(this, hillPrefab, moleHills, showTime);
    }

    /// <summary>
    ///     Called when the player presses the Start-button.
    /// </summary>
    public void StartGame() {
        IsGameRunning = true;
    }

    /// <summary>
    ///     Called when the time remaining is below 0.
    /// </summary>
    public void EndGame() {
        IsGameRunning = false;

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
