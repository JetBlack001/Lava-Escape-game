using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int coinCount = 0;
    public HUDManager hudManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Load saved coins
        coinCount = SaveSystem.LoadCoins();

        // Always find the HUD in the current scene, if any
        hudManager = FindAnyObjectByType<HUDManager>();

        if (hudManager != null)
        {
            hudManager.UpdateCoinDisplay(coinCount);
        }
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        if (hudManager != null)
        {
            hudManager.UpdateCoinDisplay(coinCount);
        }
    }

    public void AddToTotalCoins(int amount)
    {
        coinCount += amount;
    }

    public void ResetCoins()
    {
        coinCount = 0;
        SaveSystem.SaveCoins(coinCount);

        // Try to find HUD again, in case it changed scenes
        hudManager = FindAnyObjectByType<HUDManager>();

        if (hudManager != null)
        {
            hudManager.UpdateCoinDisplay(coinCount);
        }
    }

    public void RestartLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("ReturnToMainMenu() called");
        SceneManager.LoadScene("Levels/MainMenu");
    }
}