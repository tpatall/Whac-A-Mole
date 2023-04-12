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
    ///     Prefab of a hill.
    /// </summary>
    [SerializeField]
    [Tooltip("The hill prefab which will be instantiated.")]
    private GameObject hillPrefab;

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
    ///     Time between spawning a new mole, in seconds.
    /// </summary>
    private float timeBetweenSpawn;
    
    /// <summary>
    ///     Reference to the gameboard.
    /// </summary>
    private GameBoard gameBoard;

    /// <summary>
    ///     Get the Moles-list from the gameboard.
    /// </summary>
    public List<Mole> Moles => gameBoard.Moles;

    /// <summary>
    ///     Total time remaining in this game.
    /// </summary>
    public float TimeRemaining { get; private set; }
    
    /// <summary>
    ///     Whether the game is currently running. Used for managing remaining time.
    /// </summary>
    public bool IsGameRunning { get; private set; }

    /// <summary>
    ///     Current score.
    /// </summary>
    public int Score { get; private set; }

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

        this.timeBetweenSpawn = timeBetweenSpawn;

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
    ///     Update the score.
    /// </summary>
    /// <param name="updatedScore">The updated score.</param>
    public void UpdateScore(int updatedScore) {
        Score = updatedScore;
    }

    // Update is called once per frame.
    private void Update() {
        if (IsGameRunning) {
            TimeRemaining -= Time.deltaTime;

            // If the time remaining is less than the time of the last mole spawn minus the time allowed between spawns.
            if (TimeRemaining < (lastSpawn - timeBetweenSpawn)) {
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
