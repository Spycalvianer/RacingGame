using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBarScript : MonoBehaviour
{
    public WheelCollider wheelL;
    public WheelCollider wheelR;
    public float antiRoll = 5000;
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        WheelHit hit;
        float travelL;
        float travelR;
        bool groundedL = wheelL.GetGroundHit(out hit);
        if (groundedL) travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) / wheelL.suspensionDistance;
        else travelL = 1.0f;
        bool groundedR = wheelR.GetGroundHit(out hit);
        if (groundedR) travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) / wheelR.suspensionDistance;
        else travelR = 1.0f;
        float antiRollForce = (travelL - travelR) * antiRoll;
        if (groundedL) rb.AddForceAtPosition(wheelL.transform.up * -antiRollForce, wheelL.transform.position);
        if (groundedR) rb.AddForceAtPosition(wheelR.transform.up * antiRollForce, wheelR.transform.position);
    }
}
