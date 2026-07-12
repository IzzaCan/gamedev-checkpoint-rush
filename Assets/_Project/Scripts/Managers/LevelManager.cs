using UnityEngine;

public static class LevelManager
{
    private const string UnlockedLevelKey = "UnlockedLevel";

    public static int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt(UnlockedLevelKey, 1);
    }

    public static void CompleteLevel(int currentLevel)
    {
        int unlockedLevel = GetUnlockedLevel();

        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt(UnlockedLevelKey, currentLevel + 1);
            PlayerPrefs.Save();
        }
    }

    public static bool IsLevelUnlocked(int level)
    {
        return level <= GetUnlockedLevel();
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(UnlockedLevelKey);
        PlayerPrefs.Save();
    }
}