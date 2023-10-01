using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterScript : MonoBehaviour
{
    public Vector3 respawnVector;
    CarController carController;

    private void Awake()
    {
        carController = GetComponent<CarController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Shatter")
        {
            gameObject.transform.position -= respawnVector;
            carController.rb.velocity = new Vector3 (0, 0, 0);
        }
    }
}
