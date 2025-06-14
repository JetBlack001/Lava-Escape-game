using UnityEngine;

public static class SaveSystem
{
    private static string CurrentUser => PlayerPrefs.GetString("LastUser", "DefaultUser");

    public static void SaveLevel(int levelNumber)
    {
        PlayerPrefs.SetInt(CurrentUser + "_LevelProgress", levelNumber);
        PlayerPrefs.Save();
    }

    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt(CurrentUser + "_LevelProgress", 1);
    }

    public static void SaveCoins(int totalCoins)
    {
        PlayerPrefs.SetInt(CurrentUser + "_TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }

    public static int LoadCoins()
    {
        return PlayerPrefs.GetInt(CurrentUser + "_TotalCoins", 0);
    }

    public static void ResetProgress()
    {
        string user = CurrentUser;
        PlayerPrefs.DeleteKey(user + "_LevelProgress");
        PlayerPrefs.DeleteKey(user + "_TotalCoins");
        PlayerPrefs.Save();
    }
}