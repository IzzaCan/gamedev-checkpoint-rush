using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;

    private TimerManager timerManager;

    private bool isPaused;
    private bool tutorialFinished;

    private void Awake()
    {
        timerManager = FindFirstObjectByType<TimerManager>();

        if (timerManager == null)
        {
            Debug.LogError("TimerManager not found in scene!");
        }
    }

    private void Start()
    {
        HideAllPanels();

        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    //==================================================
    // Tutorial
    //==================================================

    public void StartGame()
    {
        if (tutorialFinished)
            return;

        tutorialFinished = true;

        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

        Time.timeScale = 1f;

        timerManager?.StartTimer();
    }

    //==================================================
    // Pause
    //==================================================

    public void PauseGame()
    {
        if (isPaused)
            return;

        isPaused = true;

        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (!isPaused)
            return;

        isPaused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    //==================================================
    // Game Over
    //==================================================

    public void ShowGameOver()
    {
        timerManager?.StopTimer();

        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    //==================================================
    // Level Complete
    //==================================================

    public void ShowLevelComplete()
    {
        timerManager?.StopTimer();

        Time.timeScale = 0f;

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);
    }

    //==================================================
    // Scene Navigation
    //==================================================

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextScene);
        else
            SceneManager.LoadScene("MainMenu");
    }

    //==================================================
    // Utilities
    //==================================================

    private void HideAllPanels()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }
}