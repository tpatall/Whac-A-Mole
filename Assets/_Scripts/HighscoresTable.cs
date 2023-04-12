using System.Collections.Generic;
using UnityEngine;

public class HighscoresTable : MonoBehaviour
{
    [SerializeField]
    private GameObject highscoresTableHolder;

    [SerializeField]
    private GameObject highscoresTableEntry;

    private List<GameObject> easyEntries, mediumEntries, hardEntries;

    private Highscores highscores;

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

    private void Start() {
        highscores = new Highscores();

        easyEntries = SpawnEntries(Difficulty.EASY);
        mediumEntries = SpawnEntries(Difficulty.MEDIUM);
        hardEntries = SpawnEntries(Difficulty.HARD);

        ShowTable(Difficulty.EASY);
    }

    private void SetState(List<GameObject> objectList, bool active) {
        foreach (GameObject gameObject in objectList) {
            gameObject.SetActive(active);
        }
    }

    private List<GameObject> SpawnEntries(Difficulty difficulty) {
        List<PlayerScore> scoreList = new List<PlayerScore>();
        switch (difficulty) {
            case Difficulty.EASY:
                scoreList = highscores.LoadHighScores(Difficulty.EASY);
                break;
            case Difficulty.MEDIUM:
                scoreList = highscores.LoadHighScores(Difficulty.MEDIUM);
                break;
            case Difficulty.HARD:
                scoreList = highscores.LoadHighScores(Difficulty.HARD);
                break;
        }

        List<GameObject> entries = new List<GameObject>();
        for (int rank = 0; rank < scoreList.Count; rank++) {
            var entryObject = Instantiate(highscoresTableEntry);
            entryObject.transform.SetParent(highscoresTableHolder.transform);

            HighscoresEntryUI highscoresEntryUI = entryObject.GetComponent<HighscoresEntryUI>();
            highscoresEntryUI.Initialise(rank + 1, scoreList[rank]);

            entries.Add(entryObject);
        }

        return entries;
    }
}
