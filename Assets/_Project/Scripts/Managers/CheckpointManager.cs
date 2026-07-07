using TMPro;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    [SerializeField] private int totalCheckpoints = 3;

    [Header("UI")]
    [SerializeField] private TMP_Text checkpointText;

    public int CurrentCheckpoint { get; private set; } = 0;
    public bool CanFinish { get; private set; } = false;

    private void Start()
    {
        UpdateCheckpointUI();
    }

    public void ReachCheckpoint()
    {
        CurrentCheckpoint++;

        if (CurrentCheckpoint >= totalCheckpoints)
        {
            CanFinish = true;
        }

        UpdateCheckpointUI();

        Debug.Log($"Checkpoint {CurrentCheckpoint}/{totalCheckpoints}");
    }

    private void UpdateCheckpointUI()
    {
        if (checkpointText == null)
            return;

        if (CanFinish)
        {
            checkpointText.text = "All Checkpoints Cleared!";
        }
        else
        {
            checkpointText.text = $"Checkpoint: {CurrentCheckpoint}/{totalCheckpoints}";
        }
    }
}