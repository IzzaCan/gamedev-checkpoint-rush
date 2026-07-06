using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleCarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;

    [Header("Wheel Meshes")]
    public Transform frontLeftMesh;
    public Transform frontRightMesh;
    public Transform rearLeftMesh;
    public Transform rearRightMesh;

    [Header("Car Settings")]
    public float motorForce = 1000f;
    public float steeringAngle = 20f;
    public float brakeForce = 3000f;

    [Header("Nitro Settings")]
    public float nitroMultiplier = 2f;
    public float nitroDuration = 2f;
    public float nitroCooldown = 3f;

    [Header("Mobile Joystick")]
    [SerializeField] private FixedJoystick joystick;

    private float moveInput;
    private float steerInput;
    private bool isBraking;

    private bool isNitroActive;
    private float nitroTimer;
    private float cooldownTimer;

    void Update()
    {
        // ===== KEYBOARD =====
        float keyboardMove = 0f;
        float keyboardSteer = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
                keyboardMove = 1f;
            else if (Keyboard.current.sKey.isPressed)
                keyboardMove = -0.5f;

            if (Keyboard.current.aKey.isPressed)
                keyboardSteer = -1f;
            else if (Keyboard.current.dKey.isPressed)
                keyboardSteer = 1f;

            isBraking = Keyboard.current.spaceKey.isPressed;
            isNitroActive = Keyboard.current.leftShiftKey.isPressed;
        }

        // ===== JOYSTICK =====
        float joystickMove = 0f;
        float joystickSteer = 0f;

        if (joystick != null)
        {
            joystickMove = joystick.Vertical;
            joystickSteer = joystick.Horizontal;
        }

        // ===== INPUT PRIORITY =====
        moveInput = Mathf.Abs(joystickMove) > 0.05f ? joystickMove : keyboardMove;
        steerInput = Mathf.Abs(joystickSteer) > 0.05f ? joystickSteer : keyboardSteer;
    }

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        HandleBraking();
        UpdateAllWheels();
        HandleNitro();
    }

    void HandleMotor()
    {
        float finalMotorForce = motorForce;

        if (isNitroActive && cooldownTimer <= 0)
        {
            finalMotorForce *= nitroMultiplier;
        }

        rearLeftCollider.motorTorque = moveInput * finalMotorForce;
        rearRightCollider.motorTorque = moveInput * finalMotorForce;
    }

    void HandleSteering()
    {
        float currentSteerAngle = steerInput * steeringAngle;

        frontLeftCollider.steerAngle = currentSteerAngle;
        frontRightCollider.steerAngle = currentSteerAngle;
    }

    void HandleBraking()
    {
        float currentBrakeForce = isBraking ? brakeForce : 0f;

        frontLeftCollider.brakeTorque = currentBrakeForce;
        frontRightCollider.brakeTorque = currentBrakeForce;
        rearLeftCollider.brakeTorque = currentBrakeForce;
        rearRightCollider.brakeTorque = currentBrakeForce;
    }

    void HandleNitro()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.fixedDeltaTime;
        }

        if (isNitroActive && cooldownTimer <= 0)
        {
            nitroTimer += Time.fixedDeltaTime;

            if (nitroTimer >= nitroDuration)
            {
                nitroTimer = 0f;
                cooldownTimer = nitroCooldown;
            }
        }
        else
        {
            nitroTimer = 0f;
        }
    }

    void UpdateAllWheels()
    {
        UpdateWheel(frontLeftCollider, frontLeftMesh);
        UpdateWheel(frontRightCollider, frontRightMesh);
        UpdateWheel(rearLeftCollider, rearLeftMesh);
        UpdateWheel(rearRightCollider, rearRightMesh);
    }

    void UpdateWheel(WheelCollider collider, Transform wheelMesh)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        wheelMesh.position = position;
        wheelMesh.rotation = rotation;
    }
}