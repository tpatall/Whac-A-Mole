using System.Linq;
using TMPro;
using UnityEngine;

public class HighscoresEntryUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI entryRank, entryPlayerName, entryScore;

    public void Initialise(int rank, PlayerScore playerScore) {
        entryRank.text = rank.ToString();
        entryScore.text = "Score: " + playerScore.Score.ToString();

        string name = playerScore.Name;
        // Put a max char limit in case of long names.
        // A FULL-CAPS name will overflow the UI-element at 7 chars, but thats way too small, so for this test-demo put an average estimation of 10.
        if (name.Count() > 10) {
            name = name[..10] + "...";
        }
        entryPlayerName.text = name;
    }
}
