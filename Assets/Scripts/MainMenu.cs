using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpMenu;
    public GameObject aboutMenu;
    public GameObject LoadSave;
    public GameObject quitGame;
    public GameObject hudObject;

    public TMP_Text saveSlotText;
    public TMP_Text savedLevelText;
    public TMP_Text savedCoinsText;

    public void PlayGame()
    {
        if (hudObject != null)
        {
            hudObject.SetActive(true); // show the HUD before switching scenes
        }

        SceneManager.LoadScene("Levels/Level1");
    }

    // ✅ NEW GAME: Resets all progress
    public void NewGame()
    {
        SaveSystem.ResetProgress(); // Clear all saved PlayerPrefs

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetCoins(); // Clear in-memory coin count
        }

        if (hudObject != null)
        {
            hudObject.SetActive(true);
        }

        SceneManager.LoadScene("Levels/Level1");
    }

    public void GoToLoadSave()
    {
        if (saveSlotText != null)
        {
            saveSlotText.text = "Player's Save";
        }

        int savedLevel = SaveSystem.LoadLevel();
        int savedCoins = SaveSystem.LoadCoins();

        if (savedLevelText != null)
        {
            savedLevelText.text = "Level: " + savedLevel;
        }

        if (savedCoinsText != null)
        {
            savedCoinsText.text = "Total Coins: " + savedCoins;
        }

        LoadSave.SetActive(true); // Show Load Save menu UI panel
    }


    // ✅ LOAD GAME: Only load if saved level exists
    public void LoadGame()
    {
        int savedLevel = SaveSystem.LoadLevel();

        if (savedLevel <= 1)
        {
            Debug.Log("No saved progress found. Load disabled.");
            return; // Skip loading if there's no real progress
        }

        int totalCoins = SaveSystem.LoadCoins();

        if (GameManager.Instance.hudManager != null)
        {
            GameManager.Instance.hudManager.UpdateCoinDisplay(totalCoins);
        }

        if (hudObject != null)
        {
            hudObject.SetActive(true); // Show HUD before loading
        }

        SceneManager.LoadScene("Levels/Level" + savedLevel);
    }

    public void GoToHelp()
    {
        helpMenu.SetActive(true); // Show the Help UI panel
    }

    public void GoToStory()
    {
        aboutMenu.SetActive(true); // Show the About UI panel
    }

    public void GoBack() // returns to main menu - hides both about and help menu when either is active.
    {
        helpMenu.SetActive(false);
        aboutMenu.SetActive(false);
        LoadSave.SetActive(false);
    }

    public void QuitGame()
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