using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelCoinCount = 0;

    public void AddCoin()
    {
        levelCoinCount++;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddToTotalCoins(1); // Add to global total
        }
    }

    public void RestartLevel()
    {
        Debug.Log("RestartLevel button pressed");

        // Reset coins for the level
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetCoins();
        }

        // Reload current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    // Call this when the player completes the level
    public void CompleteLevel()
    {
        int currentCoins = GameManager.Instance.coinCount;
        int previousCoins = SaveSystem.LoadCoins();
        int newTotalCoins = previousCoins + currentCoins;

        // Save new total coin count
        SaveSystem.SaveCoins(newTotalCoins);

        // Save the next level as unlocked
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SaveSystem.SaveLevel(currentSceneIndex + 1);

        Debug.Log("Level completed. Total coins saved: " + newTotalCoins);

        // Load next level
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels.");
        }
    }
}
