using UnityEngine;

/// <summary>
///     Controls UI-related functionality that affects or is affected by game-states.
/// </summary>
public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject rulesPanel, resultsPanel;

    protected override void Awake() {
        base.Awake();

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        rulesPanel.SetActive(state == GameState.Rules);
        resultsPanel.SetActive(state == GameState.Results);
    }

    public void BeginPressed() {
        GameManager.Instance.UpdateGameState(GameState.SetUp);
    }

    public void ShowResults() {
        
    }

    public void RestartPressed() {
        
    }

    public void QuitPressed() {
        
    }
}