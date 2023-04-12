using UnityEngine;

/// <summary>
///     Controls UI-related functionality that affects or is affected by game-states.
/// </summary>
public class UIManager : Singleton<UIManager>
{
    /// <summary>
    ///     Reference to the UI-panels that control their respective elements.
    /// </summary>
    [SerializeField]
    private GameObject rulesPanel, gamePanel, resultsPanel;

    protected override void Awake() {
        base.Awake();

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    /// <summary>
    ///     Enable and disable canvases based on current gamestate.
    /// </summary>
    /// <param name="state">Current GameState.</param>
    private void GameManagerOnGameStateChanged(GameState state) {
        rulesPanel.SetActive(state == GameState.Rules);
        gamePanel.SetActive(state == GameState.Play);
        resultsPanel.SetActive(state == GameState.Results);
    }

    /// <summary>
    ///     When the startbutton is pressed.
    /// </summary>
    public void StartPressed() {
        GameManager.Instance.UpdateGameState(GameState.SetUp);
    }
}