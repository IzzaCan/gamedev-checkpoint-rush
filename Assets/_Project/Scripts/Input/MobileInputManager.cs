using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    public static MobileInputManager Instance { get; private set; }

    public bool GasPressed { get; set; }
    public bool BrakePressed { get; set; }
    public bool LeftPressed { get; set; }
    public bool RightPressed { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}