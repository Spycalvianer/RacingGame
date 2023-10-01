using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float speed = 10f;  // speed of the car
    public float rotationSpeed = 100f;  // rotation speed of the car
    private float horizontalInput;  // input for turning left/right
    private float verticalInput;  // input for moving forward/backward
    void Update()
    {
        // Get the user inputs for turning and moving
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move the car forward or backward based on user input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);

        // Rotate the car left or right based on user input
        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
    }
}
