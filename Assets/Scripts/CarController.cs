using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    
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


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass.localPosition;
        upsideDownTimer = flipThreshold;
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
    }


    public void GetInput()  {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()    {
        steeringAngle = maxSteerAngle * horizontalInput;
        FLWheel.steerAngle = steeringAngle;
        FRWheel.steerAngle = steeringAngle;
    }

    private void Accelerate()   {
        FLWheel.motorTorque = verticalInput * motorForce;
        FRWheel.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelPositions()   {
        UpdateWheelPosition(FLWheel, FLWheelT);
        UpdateWheelPosition(FRWheel, FRWheelT);
        UpdateWheelPosition(BLWheel, BLWheelT);
        UpdateWheelPosition(BRWheel, BRWheelT);
    }

    private void UpdateWheelPosition(WheelCollider _collider, Transform _transform) {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()  {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPositions();
    }



}
