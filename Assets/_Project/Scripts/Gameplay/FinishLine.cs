using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private TimerManager timerManager;
    private CheckpointManager checkpointManager;
    private UIManager uiManager;

    private void Awake()
    {
        timerManager = FindFirstObjectByType<TimerManager>();
        checkpointManager = FindFirstObjectByType<CheckpointManager>();
        uiManager = FindFirstObjectByType<UIManager>();

        if (timerManager == null)
            Debug.LogError("TimerManager not found in scene!");

        if (checkpointManager == null)
            Debug.LogError("CheckpointManager not found in scene!");

        if (uiManager == null)
            Debug.LogError("UIManager not found in scene!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.root.CompareTag("Player"))
            return;

        if (timerManager == null || checkpointManager == null || uiManager == null)
            return;

        if (!checkpointManager.CanFinish)
        {
            Debug.Log("You must complete all checkpoints!");
            return;
        }

        timerManager.StopTimer();

        Debug.Log("LEVEL COMPLETE!");

        uiManager.ShowLevelComplete();
    }
}