using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Animation Settings")]
    public Sprite[] rotationSprites;    // assign all rotation frames in the Inspector
    public float frameDuration = 0.1f;  // time per frame

    [Header("Score Settings")]
    public int coinValue = 1;           // how much to add to score

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(RotateAnimation());
    }

    private System.Collections.IEnumerator RotateAnimation()
    {
        while (true)
        {
            spriteRenderer.sprite = rotationSprites[currentFrame];
            currentFrame = (currentFrame + 1) % rotationSprites.Length;
            yield return new WaitForSeconds(frameDuration);
        }
    }

    // Detect when player touches the coin
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(1);
            Destroy(gameObject); // coin disappears
        }
    }
}
