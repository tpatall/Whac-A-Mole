using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [SerializeField]
    private Sprite moleSprite;

    [SerializeField]
    private Sprite deadSprite;

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

    private SpriteRenderer spriteRenderer;

    private bool whacked;

    /// <summary>
    ///     SetUp this instance of the mole class.
    /// </summary>
    public void SetUp(GameController gameController, float showTime) {
        spriteRenderer = GetComponent<SpriteRenderer>();

        this.gameController = gameController;
        this.showTime = showTime;
        CurrentTime = showTime;

        IsVisible = false;
        gameObject.SetActive(false);
    }
    
    void Update() {
        if (IsVisible && !whacked) {
            CurrentTime -= Time.deltaTime;

            if (CurrentTime <= 0) {
                Hide();
            }
        }
    }

    /// <summary>
    ///     Make the mole visible to the player.
    /// </summary>
    public void Show() {
        spriteRenderer.sprite = moleSprite;
        whacked = false;

        IsVisible = true;
        gameController.VisibleMoles++;
        gameObject.SetActive(true);
    }

    public void Whacked() {
        whacked = true;

        spriteRenderer.sprite = deadSprite;

        StartCoroutine(DelayHide());

        IEnumerator DelayHide() {
            yield return new WaitForSeconds(0.5f);

            Hide();
        }
    }

    /// <summary>
    ///     Hide the mole from the player.
    /// </summary>
    public void Hide() {
        IsVisible = false;
        gameController.VisibleMoles--;
        gameObject.SetActive(false);

        CurrentTime = showTime;
    }
}
