using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.hudManager = this;
            UpdateCoinDisplay(GameManager.Instance.coinCount);
        }
    }

    public void UpdateCoinDisplay(int coinCount)
    {
        if (coinText != null)
        {
            coinText.text = coinCount.ToString();
        }
    }

    public void UpdateCoinDisplayFromSavedData()
    {
        if (GameManager.Instance != null)
        {
            UpdateCoinDisplay(GameManager.Instance.coinCount);
        }
    }

    // 🔁 Called by the Restart button in the UI
    public void OnRestartButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartLevel();
        }
    }

    // 🏠 Called by the Main Menu button in the UI
    public void OnMainMenuButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReturnToMainMenu();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.hudManager = this;
        }
    }
}