using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardUtils
{
    /// <summary>
    ///     Generate the optimal layout a field of mole hills can take based on number of hills.
    /// </summary>
    /// <param name="hills">Number of hills on this gameboard.</param>
    /// <returns>Grid layout as list of mole hills per row.</returns>
    public static List<int> GenerateOptimalLayout(int hills) {
        // Total mole hills per row.
        List<int> layout = new List<int>();

        // Determine amount of rows based on hills.
        int rows;
        if (hills <= 6) {
            rows = 2;
        } else if (hills <= 9) {
            rows = 3;
        } else {
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

    /// <summary>
    ///     Get the world position from the starting position and position in field.
    /// </summary>
    /// <param name="startPos">Position of the middle of the field.</param>
    /// <param name="row">Row number.</param>
    /// <param name="col">Column number.</param>
    /// <returns>World position from hill.</returns>
    public static Vector2 GetWorldPosition(Vector2 startPos, int row, int col, float xOffset, float yOffset) {
        // Calculate the world position of the hill
        Vector2 worldPosition = startPos + new Vector2(row * xOffset, col * yOffset);

        return worldPosition;
    }
}
