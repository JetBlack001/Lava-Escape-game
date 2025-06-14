using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonsYESNO : MonoBehaviour
{
    public TMP_Text totalCoinsText; // Drag the "TotalCoins" TMP UI element into this in the Inspector
    public TMP_Text playerNameText;

    public GameObject quitGame;

    private void Start()
    {
        // Show total coins collected out of total possible
        int coinsPerLevel = 12;
        int totalLevels = 2;
        int totalPossibleCoins = coinsPerLevel * totalLevels;

        int collectedCoins = GameManager.Instance.coinCount;

        if (totalCoinsText != null)
        {
            totalCoinsText.text = $"Total Coins: {collectedCoins}/{totalPossibleCoins}";
        }
        else
        {
            Debug.LogWarning("TotalCoins TMP_Text is not assigned in the inspector.");
        }

        // Set player name text
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        if (playerNameText != null)
        {
            playerNameText.text = $"Congradulations!";
        }
        else
        {
            Debug.LogWarning("PlayerName TMP_Text is not assigned in the inspector.");
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Levels/MainMenu");
    }

    public void GoToMainMenu()
    {
#if UNITY_WEBGL
        quitGame.SetActive(true); // Show message instead of quitting
#else
        Debug.Log("Quit game called.");
        quitGame.SetActive(true);
        // Application.Quit(); // Use this for standalone builds
#endif
    }
}