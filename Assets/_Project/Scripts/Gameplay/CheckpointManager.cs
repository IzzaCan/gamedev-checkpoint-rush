using UnityEngine;
using TMPro;

public class CheckpointManager : MonoBehaviour
{
    public int totalCheckpoints = 3;
    public int currentCheckpoint = 0;

    public TMP_Text checkpointText;

    public bool canFinish = false;

    public void ReachCheckpoint()
    {
        currentCheckpoint++;

        checkpointText.text = "Checkpoint: " + currentCheckpoint + "/" + totalCheckpoints;

        if (currentCheckpoint >= totalCheckpoints)
        {
            canFinish = true;
            checkpointText.text = "All Checkpoints Cleared!";
        }
    }
}