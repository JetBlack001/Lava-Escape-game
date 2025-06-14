using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public GameObject loginPanel;
    public GameObject welcomePanel;
    public TMP_Text playerNameText;

    private string currentUser;

    void Start()
    {
        if (PlayerPrefs.HasKey("LastUser"))
        {
            currentUser = PlayerPrefs.GetString("LastUser");
            playerNameText.text = currentUser;
            loginPanel.SetActive(false);
            welcomePanel.SetActive(true);
        }
        else
        {
            loginPanel.SetActive(true);
            welcomePanel.SetActive(false);
        }
    }

    public void OnRegisterButton()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (PlayerPrefs.HasKey(username + "_password"))
        {
            Debug.Log("User already exists.");
        }
        else
        {
            PlayerPrefs.SetString(username + "_password", password);
            PlayerPrefs.SetString("LastUser", username);
            PlayerPrefs.SetString("PlayerName", username); // 🔧 NEW LINE

            PlayerPrefs.SetInt(username + "_LevelProgress", 1);
            PlayerPrefs.SetInt(username + "_TotalCoins", 0);

            LoadMainMenu();
        }
    }

    public void OnLoginButton()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (PlayerPrefs.HasKey(username + "_password"))
        {
            string storedPassword = PlayerPrefs.GetString(username + "_password");
            if (storedPassword == password)
            {
                PlayerPrefs.SetString("LastUser", username);
                PlayerPrefs.SetString("PlayerName", username); // 🔧 NEW LINE
                LoadMainMenu();
            }
            else
            {
                Debug.Log("Incorrect password.");
            }
        }
        else
        {
            Debug.Log("User does not exist.");
        }
    }

    public void OnLoadGameButton()
    {
        LoadMainMenu();
    }

    public void OnChangeUserButton()
    {
        PlayerPrefs.DeleteKey("LastUser");
        PlayerPrefs.DeleteKey("PlayerName"); // 🔧 Optional: clear player name
        currentUser = "";
        welcomePanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}