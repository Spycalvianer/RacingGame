using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void ShatterBuilding()
    {
        List<Rigidbody> rigidBodies = new List<Rigidbody>();
        rigidBodies.AddRange(GetComponentsInChildren<Rigidbody>());
        for (int i = 0; i < rigidBodies.Count; i++)
        {
            rigidBodies[i].isKinematic = false;
            rigidBodies[i].AddForce(new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)),ForceMode.VelocityChange);
        }
    }
}
