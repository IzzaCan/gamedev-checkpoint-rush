using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text timerText;

    [Header("Timer Settings")]
    [SerializeField] private float startTime = 60f;

    public float CurrentTime => currentTime;
    public bool IsRunning => isRunning;

    private float currentTime;
    private bool isRunning = false;

    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindFirstObjectByType<UIManager>();

        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in scene!");
        }
    }

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (!isRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            UpdateTimerUI();
            GameOver();
            return;
        }

        UpdateTimerUI();
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        isRunning = false;
        UpdateTimerUI();
    }

    private void GameOver()
    {
        StopTimer();

        Debug.Log("GAME OVER");

        uiManager?.ShowGameOver();
    }

    private void UpdateTimerUI()
    {
        if (timerText == null)
            return;

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }
}