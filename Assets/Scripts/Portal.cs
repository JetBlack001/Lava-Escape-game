using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string nextLevel; // Assign in Inspector
    [SerializeField] private bool isFinalLevel = false; // Toggle this in Inspector for final level
    [SerializeField] private GameObject youWinPanel; // Drag the "You Win" UI prefab here
    [SerializeField] private GameObject player; // Drag your Player GameObject here

    private GameManager gameManager; // Reference to your GameManager (where coins are tracked)

    private void Start()
    {
        // Find your GameManager in the scene
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager not found in scene. Make sure one exists.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isFinalLevel)
            {
                ShowYouWinPanel();
                DisablePlayerControl();
            }
            else
            {
                SaveProgress();
                SceneManager.LoadScene(nextLevel);
            }
        }
    }

    private void SaveProgress()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("Cannot save progress, GameManager is missing.");
            return;
        }

        // Extract level number from nextLevel string (assuming format "Levels/LevelX")
        int levelNumber = ExtractLevelNumber(nextLevel);
        if (levelNumber == -1)
        {
            Debug.LogWarning("Could not extract level number from nextLevel string.");
            return;
        }

        int totalCoins = gameManager.coinCount; // or gameManager.GetCoinCount() if you made a method

        SaveSystem.SaveLevel(levelNumber);      // ✅ Save level
        SaveSystem.SaveCoins(totalCoins);       // ✅ Save coins

        Debug.Log($"Progress saved: Level {levelNumber}, Coins {totalCoins}");
    }

    private int ExtractLevelNumber(string levelName)
    {
        // Example input: "Levels/Level2"
        // Split by '/' and take last part: "Level2"
        string[] parts = levelName.Split('/');
        if (parts.Length == 0)
            return -1;

        string lastPart = parts[parts.Length - 1]; // "Level2"
        // Remove "Level" prefix and parse number
        if (lastPart.StartsWith("Level"))
        {
            string numberStr = lastPart.Substring(5); // after "Level"
            if (int.TryParse(numberStr, out int levelNum))
            {
                return levelNum;
            }
        }
        return -1;
    }

    private void ShowYouWinPanel()
    {
        if (youWinPanel != null)
        {
            youWinPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("YouWinPanel not assigned in the Inspector.");
        }
    }
    private void DisablePlayerControl()
    {
        if (player != null)
        {
            // Disable the PlayerMovement script
            Assets.Scripts.PlayerMovement movementScript = player.GetComponent<Assets.Scripts.PlayerMovement>();
            if (movementScript != null)
            {
                movementScript.enabled = false;
            }

            // Freeze the Rigidbody2D
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
        else
        {
            Debug.LogWarning("Player not assigned in Portal script.");
        }
    }
}