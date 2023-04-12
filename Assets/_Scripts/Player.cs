using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
///     Handles the player interaction with the game board.
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    ///     Total points that the player receives upon successfully whacking a mole.
    /// </summary>
    [SerializeField]
    [Tooltip("Total points that are rewarded upon successfully whacking a mole.")]
    [Range(1, 100)]
    private int scoreIncrement;

    /// <summary>
    ///     Reference to the game controller for requesting mole position.
    /// </summary>
    [SerializeField]
    private GameController gameController;

    /// <summary>
    ///     Check for player clicks.
    /// </summary>
    private void Update() {
        if (gameController.IsGameRunning && Input.GetMouseButtonDown(0)) {

            // If there are no moles currently visible, there is no need to check for hits.
            if (gameController.VisibleMoles <= 0) {
                return;
            }

            List<Mole> moles = gameController.Moles;
            
            // Check for every visible mole if the clicked position matches their hitbox.
            for (int i = 0; i < moles.Count; i++) {
                if (!moles[i].IsVisible) continue;

                Vector2 molePosition = moles[i].gameObject.transform.position;
                float moleLowX = molePosition.x - 0.5f;
                float moleLowY = molePosition.y - 0.5f;

                float moleHighX = molePosition.x + 0.5f;
                float moleHighY = molePosition.y + 0.5f;

                Vector3 playerClickPoint = GetMouseWorldPosition();

                if (playerClickPoint.x > moleLowX && playerClickPoint.x < moleHighX &&
                    playerClickPoint.y > moleLowY && playerClickPoint.y < moleHighY) {
                    WhackMole(moles[i]);
                }
            }
        }
    }

    /// <summary>
    ///     Get world position from mouse position.
    /// </summary>
    /// <returns>World position from mouse position.</returns>
    private Vector3 GetMouseWorldPosition() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }

    private void WhackMole(Mole mole) {
        mole.Whacked();
        int updatedScore = gameController.Score + scoreIncrement;
        gameController.UpdateScore(updatedScore);
    }
}
