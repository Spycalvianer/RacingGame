using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider tireFR;
    public WheelCollider tireFL;
    public WheelCollider tireRR;
    public WheelCollider tireRL;

    [Header("Wheel Particles")]
    public ParticleSystem partTireFR;
    public ParticleSystem partTireFL;
    public ParticleSystem partTireRR;
    public ParticleSystem partTireRL;

    [Header("Wheel Meshes")]
    public MeshRenderer tireMeshFR;
    public MeshRenderer tireMeshFL;
    public MeshRenderer tireMeshRR;
    public MeshRenderer tireMeshRL;

    [Header("Variables")]
    public GameObject smokeParticles;
    public AnimationCurve steeringCurve;
    public Rigidbody rb;
    public float motorPower;
    private float speed;
    public float brakePower;
    float slipAngle;
    public float gasInput;
    public float steeringInput;
    public float brakeInput;
    public float acceleration;
    public float steeringAngle;
    public float maxSpeed;
    public float gravity;
    public GameObject smokePrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        GetInput();
        UpdateWheelsMeshes();
        ApplyMotor();
        ApplySteering();
        ApplyBrake();
        //CheckParticles();
        //InstantiateSmoke();
        //InstantiateSmoke();
        //GravityForce();
        Debug.Log(tireRR.brakeTorque);
    }
    void GetInput()
    {
        gasInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
        slipAngle = Vector3.Angle(transform.forward, rb.velocity - transform.forward);
        if(slipAngle < 120f)
        {
            if (gasInput <= 0)
            {
                brakeInput = Mathf.Abs(gasInput);
                gasInput = 0;
            }
        }
        else
        {
            brakeInput = 0;
        }
        Debug.Log(gasInput);
    }
    void ApplyBrake()
    {
        tireFL.brakeTorque = brakeInput * brakePower;
        tireFR.brakeTorque = brakeInput * brakePower;
        tireRL.brakeTorque = brakeInput * brakePower;
        tireRR.brakeTorque = brakeInput * brakePower;
    }
    void ApplyMotor()
    {
        tireRR.motorTorque = motorPower * gasInput * acceleration;
        tireRL.motorTorque = motorPower * gasInput * acceleration;
        tireFL.motorTorque = motorPower * gasInput * acceleration;
        tireFR.motorTorque = motorPower * gasInput * acceleration;
        // Mathf.Clamp(tireRR.motorTorque, -15, 30);
        // Mathf.Clamp(tireRL.motorTorque, -15, 30);
        // Mathf.Clamp(tireFL.motorTorque, -15, 30);
        // Mathf.Clamp(tireFR.motorTorque, -15, 30);
        //rb.AddForce(transform.forward * accelSpeed * gasInput, ForceMode.Acceleration);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
    void ApplySteering()
    {
        steeringAngle = steeringInput  * steeringCurve.Evaluate(speed);
        //steeringAngle += Vector3.SignedAngle(transform.forward, rb.velocity + transform.forward, Vector3.up);//--->This line produces wobble drifting
        steeringAngle = Mathf.Clamp(steeringAngle, -80f, 80f);
        tireFR.steerAngle = steeringAngle;
        tireFL.steerAngle = steeringAngle;
    }
    void UpdateWheelsMeshes()
    {
        UpdateWheelIndividually(tireFR, tireMeshFR);
        UpdateWheelIndividually(tireFL, tireMeshFL);
        UpdateWheelIndividually(tireRR, tireMeshRR);
        UpdateWheelIndividually(tireRL, tireMeshRL);
    }
    void UpdateWheelIndividually(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }
    void CheckParticles()
    {
        WheelHit[] wheelHits = new WheelHit[4];
        tireFL.GetGroundHit(out wheelHits[0]);
        tireFR.GetGroundHit(out wheelHits[1]);
        tireRL.GetGroundHit(out wheelHits[2]);
        tireRR.GetGroundHit(out wheelHits[3]);
        float slipAllowance = 1f;
        if((Mathf.Abs(wheelHits[0].sidewaysSlip) > slipAllowance))
        {
            partTireFL.Play();
        }
        else
        {
            wheelHits[0].sidewaysSlip = 0;
            partTireFL.Stop();
        }
        if ((Mathf.Abs(wheelHits[1].sidewaysSlip) > slipAllowance))
        {
            partTireFR.Play();
        }
        else if (Mathf.Abs(wheelHits[1].sidewaysSlip) <= slipAllowance)
        {
            partTireFR.Stop();
        }
        if ((Mathf.Abs(wheelHits[2].sidewaysSlip) > slipAllowance))
        {
            partTireRL.Play();
        }
        else if(Mathf.Abs(wheelHits[2].sidewaysSlip) <= slipAllowance)
        {
            wheelHits[2].sidewaysSlip = 0;
            partTireRL.Stop();
        }
        if ((Mathf.Abs(wheelHits[3].sidewaysSlip) > slipAllowance))
        {
            partTireRR.Play();
        }
        else if (Mathf.Abs(wheelHits[3].sidewaysSlip) <= slipAllowance)
        {
            wheelHits[3].sidewaysSlip = 0;
            partTireRR.Stop();
        }
    }
    void InstantiateSmoke()
    {
        partTireFL = Instantiate(smokePrefab, tireFL.transform.position - Vector3.up * tireFL.radius, Quaternion.identity, tireFL.transform).GetComponent<ParticleSystem>();
        partTireFR = Instantiate(smokePrefab, tireFR.transform.position - Vector3.up * tireFR.radius, Quaternion.identity, tireFR.transform).GetComponent<ParticleSystem>();
        partTireRL = Instantiate(smokePrefab, tireRL.transform.position - Vector3.up * tireRL.radius, Quaternion.identity, tireRL.transform).GetComponent<ParticleSystem>();
        partTireRR = Instantiate(smokePrefab, tireRR.transform.position - Vector3.up * tireRR.radius, Quaternion.identity, tireRR.transform).GetComponent<ParticleSystem>();
    }
    void GravityForce()
    {
        if(tireFL.isGrounded == false && tireFR.isGrounded == false && tireRL.isGrounded == false && tireRR.isGrounded == false)
        {
            rb.AddForce(-Vector3.up * gravity, ForceMode.Acceleration);
        }
    }
}
