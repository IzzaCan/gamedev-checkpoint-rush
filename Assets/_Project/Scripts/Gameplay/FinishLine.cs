using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public TimerManager timerManager;
    public CheckpointManager checkpointManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.root.CompareTag("Player"))
            return;

        // Safety check (hindari NullReference)
        if (checkpointManager == null)
        {
            Debug.LogError("CheckpointManager NOT ASSIGNED!");
            return;
        }

        if (timerManager == null)
        {
            Debug.LogError("TimerManager NOT ASSIGNED!");
            return;
        }

        // Debug info
        Debug.Log("canFinish = " + checkpointManager.canFinish);
        Debug.Log("currentCheckpoint = " + checkpointManager.currentCheckpoint);
        Debug.Log("totalCheckpoints = " + checkpointManager.totalCheckpoints);

        // Validasi checkpoint
        if (!checkpointManager.canFinish)
        {
            Debug.Log("You must complete all checkpoints!");
            return;
        }

        // Finish success
        Debug.Log("LEVEL COMPLETE!");

        timerManager.StopTimer();
    }
}