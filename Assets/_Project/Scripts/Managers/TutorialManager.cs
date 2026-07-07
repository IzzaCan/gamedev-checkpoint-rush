using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject tutorialPanel;

    private TimerManager timerManager;
    private bool tutorialFinished = false;

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
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);

        Time.timeScale = 0f;
    }

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
}