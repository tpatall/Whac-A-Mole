using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Handles the filling of the highscores-table.
///     This class is responsible for loading in highscores and swapping between different lists for the UI.
/// </summary>
public class HighscoresTable : MonoBehaviour
{
    /// <summary>
    ///     Holder for the highscore-table entries.
    /// </summary>
    [SerializeField]
    private GameObject highscoresTableHolder;

    /// <summary>
    ///     Prefab of a highscore-table entry.
    /// </summary>
    [SerializeField]
    private GameObject highscoresTableEntry;

    /// <summary>
    ///     List of entry-prefabs for its respective difficulty.
    /// </summary>
    private List<GameObject> easyEntries, mediumEntries, hardEntries;

    /// <summary>
    ///     Enables the relevant entries and disables the rest.
    /// </summary>
    /// <param name="difficulty">The chosen difficulty-list.</param>
    public void ShowTable(Difficulty difficulty) {
        switch (difficulty) {
            case Difficulty.EASY:
                SetState(mediumEntries, false);
                SetState(hardEntries, false);

                SetState(easyEntries, true);
                break;
            case Difficulty.MEDIUM:
                SetState(easyEntries, false);
                SetState(hardEntries, false);

                SetState(mediumEntries, true);
                break;
            case Difficulty.HARD:
                SetState(easyEntries, false);
                SetState(mediumEntries, false);

                SetState(hardEntries, true);
                break;
        }
    }

    /// <summary>
    ///     Initialize variables and settings.
    /// </summary>
    private void Start() {
        easyEntries = SpawnEntries(Difficulty.EASY);
        mediumEntries = SpawnEntries(Difficulty.MEDIUM);
        hardEntries = SpawnEntries(Difficulty.HARD);

        // Always open the easy list on initialisation.
        ShowTable(Difficulty.EASY);
    }

    /// <summary>
    ///     Set all gameObjects in a list to active or inactive.
    /// </summary>
    /// <param name="objectList">List of gameObjects.</param>
    /// <param name="active">Whether to set it to active or inactive.</param>
    private void SetState(List<GameObject> objectList, bool active) {
        foreach (GameObject gameObject in objectList) {
            gameObject.SetActive(active);
        }
    }

    /// <summary>
    ///     Initialize highscore-table entries based on a difficulty setting.
    /// </summary>
    /// <param name="difficulty">The chosen difficulty-setting.</param>
    /// <returns>A list of highscore-entry gameObjects based on the given difficulty.</returns>
    private List<GameObject> SpawnEntries(Difficulty difficulty) {
        List<HighScoreEntry> scoreList = new List<HighScoreEntry>();
        switch (difficulty) {
            case Difficulty.EASY:
                scoreList = HighScores.LoadHighScores(Difficulty.EASY);
                break;
            case Difficulty.MEDIUM:
                scoreList = HighScores.LoadHighScores(Difficulty.MEDIUM);
                break;
            case Difficulty.HARD:
                scoreList = HighScores.LoadHighScores(Difficulty.HARD);
                break;
        }

        List<GameObject> entries = new List<GameObject>();
        // Easier to add rank here than by keeping track of it in the struct.
        for (int rank = 0; rank < scoreList.Count; rank++) {
            var entryObject = Instantiate(highscoresTableEntry);
            entryObject.transform.SetParent(highscoresTableHolder.transform);

            HighScoreEntryUI highscoresEntryUI = entryObject.GetComponent<HighScoreEntryUI>();
            highscoresEntryUI.Initialize(rank + 1, scoreList[rank]);

            entries.Add(entryObject);
        }

        return entries;
    }
}
