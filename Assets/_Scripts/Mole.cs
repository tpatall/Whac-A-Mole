using System.Collections;
using UnityEngine;

/// <summary>
///     Handles a mole object and its actions.
/// </summary>
public class Mole : MonoBehaviour
{
    /// <summary>
    ///     Normal sprite for a mole.
    /// </summary>
    [SerializeField]
    private Sprite moleSprite;

    /// <summary>
    ///     Sprite to show when the mole gets whacked.
    /// </summary>
    [SerializeField]
    private Sprite deadSprite;

    /// <summary>
    ///     Show-time in seconds.
    /// </summary>
    private float showTime;

    /// <summary>
    ///     Reference to the gameController.
    /// </summary>
    private GameController gameController;

    /// <summary>
    ///     Reference to its attached spriteRenderer.
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    ///     Whether the mole is currently visible.
    /// </summary>
    public bool IsVisible { get; private set; }

    /// <summary>
    ///     If the mole has been whacked, to swap sprites.
    /// </summary>
    public bool Whacked { get; private set; }

    /// <summary>
    ///     Public property to see the time.
    /// </summary>
    public float CurrentTime { get; private set; }

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
        if (IsVisible && !Whacked) {
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
        Whacked = false;

        IsVisible = true;
        gameController.VisibleMoles++;
        gameObject.SetActive(true);
    }

    /// <summary>
    ///     Change the sprite and delay its hiding when they got whacked.
    /// </summary>
    public void GotWhacked() {
        Whacked = true;

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
