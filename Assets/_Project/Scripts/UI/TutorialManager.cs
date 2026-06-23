using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TimerManager timerManager;

    private void Start()
    {
        tutorialPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        tutorialPanel.SetActive(false);

        Time.timeScale = 1f;

        timerManager.StartTimer();
    }
}