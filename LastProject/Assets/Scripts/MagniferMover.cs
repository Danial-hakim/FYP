using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagniferMover : MonoBehaviour
{
    public float speed = 10.0f;
    public Transform playerCamera;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get the camera's local right, up, and forward vectors
        Vector3 right = playerCamera.right;
        Vector3 up = playerCamera.up;
        Vector3 forward = playerCamera.forward;

        // Project the forward vector onto the XZ plane
        forward.y = 0;

        // Normalize the vectors
        right.Normalize();
        up.Normalize();
        forward.Normalize();

        // Calculate the movement vector based on the input and camera orientation
        Vector3 movement = (horizontal * right + vertical * forward) * speed * Time.deltaTime;

        // Move the magnifier along with the camera
        transform.position += movement;
    }
}
