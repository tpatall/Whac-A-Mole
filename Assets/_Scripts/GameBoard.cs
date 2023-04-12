using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Handles the setup of the gameboard and keeps the list of mole(hills).
/// </summary>
public class GameBoard
{
    /// <summary>
    ///     Reference to the GameController.
    /// </summary>
    private readonly GameController gameController;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GameBoard"/> class.
    /// </summary>
    public GameBoard(GameController gameController, int totalMoleHills) {
        this.gameController = gameController;

        GenerateMoleHills(totalMoleHills);
    }

    /// <summary>
    ///     Instantiate a prefabricated mole hill gameobject.
    /// </summary>
    private void GenerateMoleHills(int totalMoleHills) {
        // Horizontal spacing between hills.
        float xOffset = 1.4f;
        // Vertical spacing between hills.
        float yOffset = 2f;

        List<int> moleHillsLayout = GenerateOptimalLayout(totalMoleHills);

        // Get the additional offset of the y-coordinate of the first row based on amount of rows,
        // so that the overall *field* is still centered.
        float extraYOffset;
        if (moleHillsLayout.Count % 2 == 0) {
            extraYOffset = -yOffset * (moleHillsLayout.Count / 2) / 2;
        }
        else {
            extraYOffset = -yOffset * (moleHillsLayout.Count / 2);
        }

        // Set the starting y-coordinate based on offset and amount of rows.
        float startPosY = extraYOffset + yOffset * (moleHillsLayout.Count - 1);

        // Loop over number of rows.
        for (int i = 0; i < moleHillsLayout.Count; i++) {
            int hillsInRow = moleHillsLayout[i];

            // Get the additional offset of the y-coordinate of the first row based on amount of rows,
            // so that the overall *row* is still centered.
            float extraXOffset;
            if (hillsInRow % 2 == 0) {
                extraXOffset = (xOffset + -xOffset * hillsInRow) / 2;
            }
            else {
                extraXOffset = -xOffset * (hillsInRow / 2);
            }

            // Loop over hills in a row.
            for (int j = 0; j < hillsInRow; j++) {
                Vector2 worldPosition = new Vector2(extraXOffset + (j * xOffset), startPosY - (i * yOffset));
                gameController.InstantiateMoles(worldPosition);
            }
        }
    }

    /// <summary>
    ///     Generate the optimal layout a field of mole hills can take based on number of hills.
    /// </summary>
    /// <param name="hills">Number of hills on this gameboard.</param>
    /// <returns>Grid layout as list of mole hills per row.</returns>
    private static List<int> GenerateOptimalLayout(int hills) {
        // Total mole hills per row.
        List<int> layout = new List<int>();

        // Determine amount of rows based on hills.
        int rows;
        if (hills <= 6) {
            rows = 2;
        }
        else if (hills <= 9) {
            rows = 3;
        }
        else {
            rows = 4;
        }

        // Fill list of rows with 0 to indicate a still-empty row.
        for (int i = 0; i < rows; i++) {
            layout.Add(0);
        }

        // Loop down the number of hills to fill the rows gradually.
        int currentRowIndex = 0;
        while (hills > 0) {
            hills--;

            layout[currentRowIndex++]++;

            // If beyond the size of the list, reset back to the start.
            if (currentRowIndex >= layout.Count) {
                currentRowIndex = 0;
            }
        }

        return layout;
    }
}
