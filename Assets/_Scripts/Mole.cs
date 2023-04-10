using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mole : MonoBehaviour
{
    /// <summary>
    ///     Show-time in seconds.
    /// </summary>
    private float showTime;

    /// <summary>
    ///     Public property to see the time.
    /// </summary>
    public float CurrentTime { get; private set; }

    /// <summary>
    ///     Whether the mole is currently visible.
    /// </summary>
    public bool IsVisible; //{ get; set; }

    private GameController gameController;

    /// <summary>
    ///     SetUp this instance of the mole class.
    /// </summary>
    public void SetUp(GameController gameController, float showTime) {
        this.gameController = gameController;
        this.showTime = showTime;
        CurrentTime = showTime;

        Hide(true);
    }
    
    void Update() {
        if (IsVisible) {
            CurrentTime -= Time.deltaTime;

            if (CurrentTime <= 0) {
                Hide(false);
            }
        }
    }

    /// <summary>
    ///     Make the mole visible to the player.
    /// </summary>
    public void Show() {
        IsVisible = true;
        gameController.VisibleMoles++;
        gameObject.SetActive(true);
    }

    /// <summary>
    ///     Hide the mole from the player.
    /// </summary>
    public void Hide(bool duringSetup) {
        IsVisible = false;
        if (!duringSetup) gameController.VisibleMoles--;
        gameObject.SetActive(false);

        CurrentTime = showTime;
    }
}
