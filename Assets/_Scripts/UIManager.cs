using UnityEngine;

/// <summary>
///     Controls UI-related functionality that affects or is affected by game-states.
/// </summary>
public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject rulesPanel, gamePanel, resultsPanel;

    protected override void Awake() {
        base.Awake();

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        rulesPanel.SetActive(state == GameState.Rules);
        gamePanel.SetActive(state == GameState.Play);
        resultsPanel.SetActive(state == GameState.Results);
    }

    public void BeginPressed() {
        GameManager.Instance.UpdateGameState(GameState.SetUp);
    }
}