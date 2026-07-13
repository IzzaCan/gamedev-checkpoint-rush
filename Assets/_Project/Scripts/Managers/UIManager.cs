using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("HUD")]
    [SerializeField] private GameObject pauseButton;

    private TimerManager timerManager;

    private bool isPaused;
    private bool tutorialFinished;

    private void Awake()
    {
        timerManager = FindFirstObjectByType<TimerManager>();

        if (timerManager == null)
            Debug.LogError("TimerManager not found in scene!");
    }

    private void Start()
    {
        HideAllPanels();

        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        // Engine tidak menyala selama tutorial
        EngineAudio.Instance?.StopEngine();
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

        // Mulai suara mesin
        EngineAudio.Instance?.StartEngine();
    }

    //==================================================
    // Pause
    //==================================================

    public void PauseGame()
    {
        if (isPaused)
            return;

        isPaused = true;

        pausePanel?.SetActive(true);
        pauseButton?.SetActive(false);

        Time.timeScale = 0f;

        EngineAudio.Instance?.PauseEngine();
    }

    public void ResumeGame()
    {
        if (!isPaused)
            return;

        isPaused = false;

        pausePanel?.SetActive(false);
        pauseButton?.SetActive(true);

        Time.timeScale = 1f;

        EngineAudio.Instance?.ResumeEngine();
    }

    //==================================================
    // Game Over
    //==================================================

    public void ShowGameOver()
    {
        timerManager?.StopTimer();

        Time.timeScale = 0f;

        EngineAudio.Instance?.StopEngine();

        gameOverPanel?.SetActive(true);
    }

    //==================================================
    // Level Complete
    //==================================================

    public void ShowLevelComplete()
    {
        timerManager?.StopTimer();

        Time.timeScale = 0f;

        EngineAudio.Instance?.StopEngine();

        levelCompletePanel?.SetActive(true);
    }

    //==================================================
    // Scene Navigation
    //==================================================

    public void RestartLevel()
    {
        EngineAudio.Instance?.StopEngine();

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        EngineAudio.Instance?.StopEngine();

        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        EngineAudio.Instance?.StopEngine();

        Time.timeScale = 1f;

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextScene);
        else
            SceneManager.LoadScene("MainMenu");
    }

    //==================================================
    // Settings
    //==================================================

    public void OpenSettings()
    {
        pausePanel?.SetActive(false);
        settingsPanel?.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel?.SetActive(false);
        pausePanel?.SetActive(true);
    }

    //==================================================
    // Utilities
    //==================================================

    private void HideAllPanels()
    {
        tutorialPanel?.SetActive(false);
        pausePanel?.SetActive(false);
        gameOverPanel?.SetActive(false);
        levelCompletePanel?.SetActive(false);
        settingsPanel?.SetActive(false);
    }
}