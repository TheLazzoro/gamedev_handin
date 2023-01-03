using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public int playerId;
    private PlayerControls controls;

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;

    public WheelCollider FLWheel, FRWheel;
    public WheelCollider BLWheel, BRWheel;
    public Transform FLWheelT, FRWheelT;
    public Transform BLWheelT, BRWheelT;

    public float maxSteerAngle = 30;
    public float motorForce = 50;

    public Transform centerOfMass;
    private Rigidbody rigidbody;

    private float upsideDownTimer;
    private readonly float flipThreshold = 2f;

    public AudioSource audioSource;

    private float carSpeed = 0;

    public Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
    public float jumpForce = 2.0f;

    void Awake()
    {
        controls = new PlayerControls();
        if (playerId == 1)
        {
            // WASD keys
            controls.Player1.Move.performed += ctx =>
            {
                Vector2 v = ctx.ReadValue<Vector2>();
                horizontalInput = v.x;
                verticalInput = v.y;
            };
            controls.Player1.Move.canceled += ctx =>
            {
                horizontalInput = 0;
                verticalInput = 0;
            };
            controls.Player1.Jump.performed += Jump_performed;
        }
        else if (playerId == 2)
        {
            // Arrow keys
            controls.Player2.MoveArrowKeys.performed += ctx =>
            {
                Vector2 v = ctx.ReadValue<Vector2>();
                horizontalInput = v.x;
                verticalInput = v.y;
            };
            controls.Player2.MoveArrowKeys.canceled += ctx =>
            {
                horizontalInput = 0;
                verticalInput = 0;
            };

            // Joystick/Gamepad controls
            controls.Player2.Accelerate.performed += ctx => verticalInput = 1;
            controls.Player2.Accelerate.canceled += ctx => verticalInput = 0;
            controls.Player2.Reverse.canceled += ctx => verticalInput = 0;
            controls.Player2.Reverse.performed += ctx => verticalInput = -1;
            controls.Player2.TurnJoystick.performed += ctx =>
            {
                Vector2 v = ctx.ReadValue<Vector2>();
                horizontalInput = v.x;
            };
            controls.Player2.TurnJoystick.canceled += ctx => horizontalInput = 0;
            controls.Player2.Jump.performed += Jump_performed;
        }
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        WheelHit hit; // we don't use this, but 'GetGroundHit' requires it.
        bool[] isWheelGrounded = new bool[4];
        isWheelGrounded[0] = FLWheel.GetGroundHit(out hit);
        isWheelGrounded[1] = FRWheel.GetGroundHit(out hit);
        isWheelGrounded[2] = BLWheel.GetGroundHit(out hit);
        isWheelGrounded[3] = BRWheel.GetGroundHit(out hit);
        for (int i = 0; i < isWheelGrounded.Length; i++)
        {
            if (!isWheelGrounded[i])
                return;
        }

        rigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
    }


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass.localPosition;
        upsideDownTimer = flipThreshold;

        audioSource.volume = 0.15f;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        // if car is upside down.
        if (Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            RaycastHit hit;
            float distance = 1f;
            Vector3 targetLocation;
            // if ray hits something below the car (the ground), start timer to flip.
            if (Physics.Raycast(transform.position, Vector3.down, out hit, distance))
                upsideDownTimer -= Time.deltaTime;
        }
        else
            upsideDownTimer = flipThreshold;

        if (upsideDownTimer < 0) // flip car
        {
            upsideDownTimer = flipThreshold;
            var up = new Vector3(0, 1, 0);
            var rot = new Quaternion(0, 0, 0, 0);
            transform.Translate(up);
            transform.rotation = rot;
        }

        carSpeed = (rigidbody.velocity.magnitude > 0) ? rigidbody.velocity.magnitude : 0;

        // The car goes up to about 24 speed max, but in most cases stays around 20.
        // To simplify we're capping the calculated speed for pitch changes to 20,
        // and dividing by it to get a pitch change between 0 and 1.
        // Adding 1 due to it being the default pitch, meaning it goes between 1 and 2.
        var carCappedSpeed = carSpeed > 20 ? 20 : carSpeed;
        audioSource.pitch = 1 + (carCappedSpeed / 20);
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        FLWheel.steerAngle = steeringAngle;
        FRWheel.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        FLWheel.motorTorque = verticalInput * motorForce;
        FRWheel.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelPositions()
    {
        UpdateWheelPosition(FLWheel, FLWheelT);
        UpdateWheelPosition(FRWheel, FRWheelT);
        UpdateWheelPosition(BLWheel, BLWheelT);
        UpdateWheelPosition(BRWheel, BRWheelT);
    }

    private void UpdateWheelPosition(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateWheelPositions();
    }
}
