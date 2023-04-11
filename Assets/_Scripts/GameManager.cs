using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Controls the enum-based state switching.
///     This class is responsible for managing the overall game state.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private ToggleGroup toggles;

    [SerializeField]
    private GameController gameController;

    public static event Action<GameState> OnGameStateChanged;

    public GameState State { get; private set; }

    private void Start() {
        UpdateGameState(GameState.Rules);
    }

    /// <summary>
    ///     Updates the game state to newstate.
    /// </summary>
    /// <param name="newstate">Next state.</param>
    public void UpdateGameState(GameState newstate) {
        State = newstate;
        UnityEngine.Debug.Log("State change: " + newstate);

        switch (newstate) {
            case GameState.Rules:
                // The 'Rules' state does not need extra functions as the UIManager is subscribed to this,
                // and the state is only UI.
                break;
            case GameState.SetUp:
                HandleSetUp();
                break;
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.TearDown:
                HandleTearDown();
                break;
            case GameState.Results:
                HandleResults();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newstate), newstate, null);
        }

        OnGameStateChanged?.Invoke(newstate);
    }

    /// <summary>
    ///     Handle the game setup.
    /// </summary>
    private void HandleSetUp() {
        gameController.SetUp(GetDifficulty());

        StartCoroutine(WaitJustOneMoment());

        IEnumerator WaitJustOneMoment() {
            yield return new WaitForFixedUpdate();

            UpdateGameState(GameState.Play);
        }
    }
    
    /// <summary>
    ///     Get the difficulty from the difficulty-toggles.
    /// </summary>
    /// <returns>The chosen difficulty.</returns>
    private Difficulty GetDifficulty() {
        Toggle activeToggle = toggles.GetFirstActiveToggle();
        string difficultyText = activeToggle.GetComponentInChildren<TextMeshProUGUI>().text;

        Difficulty difficulty;
        switch (difficultyText) {
            case "Easy":
                difficulty = Difficulty.EASY;
                break;
            case "Medium":
                difficulty = Difficulty.MEDIUM;
                break;
            case "Hard":
                difficulty = Difficulty.HARD;
                break;
            default:
                difficulty = Difficulty.EASY;
                break;
        }

        return difficulty;
    }

    /// <summary>
    ///     Handle the game start.
    /// </summary>
    private void HandlePlay() {
        gameController.StartGame();
    }

    /// <summary>
    ///     Handle tearing down the game scene.
    ///     Useful for setting a delay so the change isn't instant.
    /// </summary>
    private void HandleTearDown() {
        StartCoroutine(WaitForTearDown());

        IEnumerator WaitForTearDown() {
            yield return new WaitForFixedUpdate();

            UpdateGameState(GameState.Results);
        }
    }

    /// <summary>
    ///     Handle the results of the game.
    /// </summary>
    private void HandleResults() { }
}

public enum GameState
{
    Rules,
    SetUp,
    Play,
    TearDown,
    Results
}

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD
}