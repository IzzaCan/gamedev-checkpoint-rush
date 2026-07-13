using UnityEngine;
using UnityEngine.EventSystems;

public class VehicleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType
    {
        Gas,
        Brake,
        Left,
        Right
    }

    [Header("Button")]
    [SerializeField] private ButtonType buttonType;

    [Header("Visual Feedback")]
    [SerializeField] private Vector3 pressedScale = new(0.95f, 0.95f, 0.95f);

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = pressedScale;
        SetButtonState(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        SetButtonState(false);
    }

    private void SetButtonState(bool pressed)
    {
        if (MobileInputManager.Instance == null)
            return;

        switch (buttonType)
        {
            case ButtonType.Gas:
                MobileInputManager.Instance.GasPressed = pressed;
                break;

            case ButtonType.Brake:
                MobileInputManager.Instance.BrakePressed = pressed;
                break;

            case ButtonType.Left:
                MobileInputManager.Instance.LeftPressed = pressed;
                break;

            case ButtonType.Right:
                MobileInputManager.Instance.RightPressed = pressed;
                break;
        }
    }
}