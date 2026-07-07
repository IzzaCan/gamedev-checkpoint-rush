using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager manager;
    private bool triggered = false;

    private void Awake()
    {
        manager = FindFirstObjectByType<CheckpointManager>();

        if (manager == null)
        {
            Debug.LogError("CheckpointManager not found in scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (!other.transform.root.CompareTag("Player"))
            return;

        triggered = true;

        manager.ReachCheckpoint();

        Debug.Log($"Checkpoint Passed: {gameObject.name}");
    }
}