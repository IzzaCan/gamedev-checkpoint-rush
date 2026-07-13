using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider;
    [SerializeField] private WheelCollider rearRightCollider;

    [Header("Wheel Meshes")]
    [SerializeField] private Transform frontLeftMesh;
    [SerializeField] private Transform frontRightMesh;
    [SerializeField] private Transform rearLeftMesh;
    [SerializeField] private Transform rearRightMesh;

    [Header("Car Settings")]
    [SerializeField] private float motorForce = 1000f;
    [SerializeField] private float steeringAngle = 20f;
    [SerializeField] private float brakeForce = 3000f;

    [Header("Reverse")]
    [Tooltip("Reverse speed multiplier relative to motor force.")]
    [SerializeField, Range(-1f, 0f)]
    private float reverseMultiplier = -0.5f;

    [Tooltip("Speed threshold before switching from brake to reverse.")]
    [SerializeField]
    private float reverseThreshold = 0.5f;

    private float moveInput;
    private float steerInput;
    private bool isBrakePressed;

    private Rigidbody carRb;

    private void Awake()
    {
        carRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ReadInput();
        UpdateWheelVisuals();
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        HandleBraking();
    }

    #region Input

    private void ReadInput()
    {
        moveInput = 0f;
        steerInput = 0f;
        isBrakePressed = false;

        // Unity 6
        float forwardSpeed = Vector3.Dot(transform.forward, carRb.linearVelocity);

        //-----------------------
        // Keyboard
        //-----------------------
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
            {
                moveInput = 1f;
            }
            else if (Keyboard.current.sKey.isPressed)
            {
                if (forwardSpeed > reverseThreshold)
                {
                    isBrakePressed = true;
                }
                else
                {
                    moveInput = reverseMultiplier;
                }
            }

            if (Keyboard.current.aKey.isPressed)
                steerInput = -1f;
            else if (Keyboard.current.dKey.isPressed)
                steerInput = 1f;

            // Space = Hand Brake
            if (Keyboard.current.spaceKey.isPressed)
            {
                isBrakePressed = true;
            }
        }

        //-----------------------
        // Mobile
        //-----------------------
        if (MobileInputManager.Instance != null)
        {
            if (MobileInputManager.Instance.GasPressed)
            {
                moveInput = 1f;
            }
            else if (MobileInputManager.Instance.BrakePressed)
            {
                if (forwardSpeed > reverseThreshold)
                {
                    isBrakePressed = true;
                }
                else
                {
                    moveInput = reverseMultiplier;
                }
            }

            if (MobileInputManager.Instance.LeftPressed)
                steerInput = -1f;
            else if (MobileInputManager.Instance.RightPressed)
                steerInput = 1f;
        }
    }

    #endregion

    #region Car Movement

    private void HandleMotor()
    {
        float motorTorque = moveInput * motorForce;

        rearLeftCollider.motorTorque = motorTorque;
        rearRightCollider.motorTorque = motorTorque;
    }

    private void HandleSteering()
    {
        float steer = steerInput * steeringAngle;

        frontLeftCollider.steerAngle = steer;
        frontRightCollider.steerAngle = steer;
    }

    private void HandleBraking()
    {
        float brakeTorque = isBrakePressed ? brakeForce : 0f;

        frontLeftCollider.brakeTorque = brakeTorque;
        frontRightCollider.brakeTorque = brakeTorque;
        rearLeftCollider.brakeTorque = brakeTorque;
        rearRightCollider.brakeTorque = brakeTorque;
    }

    #endregion

    #region Wheel Visual

    private void UpdateWheelVisuals()
    {
        UpdateWheel(frontLeftCollider, frontLeftMesh);
        UpdateWheel(frontRightCollider, frontRightMesh);
        UpdateWheel(rearLeftCollider, rearLeftMesh);
        UpdateWheel(rearRightCollider, rearRightMesh);
    }

    private void UpdateWheel(WheelCollider wheelCollider, Transform wheelMesh)
    {
        if (wheelMesh == null)
            return;

        wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

        wheelMesh.position = position;
        wheelMesh.rotation = rotation;
    }

    #endregion
}