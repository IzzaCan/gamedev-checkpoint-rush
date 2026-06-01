using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointManager manager;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.transform.root.CompareTag("Player"))
        {
            triggered = true;
            manager.ReachCheckpoint();

            Debug.Log("Checkpoint: " + gameObject.name + 
              " | Total = " + manager.currentCheckpoint);
        }
    }
}