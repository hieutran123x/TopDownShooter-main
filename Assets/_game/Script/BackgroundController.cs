using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    public float moveSpeed = 5f; // Speed at which the background moves
    public FloatingJoystick joystick; // Reference to the joystick controller

    void Update()
    {
        // Get input from the joystick
        float horizontalInput = joystick.GetHorizontalInput();

        // Move the background based on the joystick input
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}


