using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip CollectSFX;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add to session coin count only (not total)
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddCoin(1);
            }

            // Play sound
            if (CollectSFX != null && audioSource != null)
            {
                audioSource.PlayOneShot(CollectSFX, 1.0f);
            }

            // Disable visuals and collision
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;

            // Destroy with delay to let sound finish
            Destroy(gameObject, 0.5f);
        }
    }
}