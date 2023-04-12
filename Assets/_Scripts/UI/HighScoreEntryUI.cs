using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
///     Handles the UI-elements of the highscore-table entries.
/// </summary>
public class HighScoreEntryUI : MonoBehaviour
{
    /// <summary>
    ///     Reference to the text-fields where the respective info will be shown.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI entryRank, entryPlayerName, entryScore;

    /// <summary>
    ///     Initialize the text-fields based on given parameters.
    /// </summary>
    /// <param name="rank">The rank in the list of highscores.</param>
    /// <param name="highScoreEntry">The highscore-entry information.</param>
    public void Initialize(int rank, HighScoreEntry highScoreEntry) {
        entryRank.text = rank.ToString();
        entryScore.text = "Score: " + highScoreEntry.Score.ToString();

        string name = highScoreEntry.Name;
        // Put a max char limit in case of long names.
        // A FULL-CAPS name will overflow the UI-element at 7 chars, but thats way too small a limit,
        // so for this test-demo put an average estimation instead.
        if (name.Count() > 10) {
            name = name[..10] + "...";
        }
        entryPlayerName.text = name;
    }
}
