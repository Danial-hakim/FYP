using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(-vertical * rotationSpeed * Time.deltaTime, horizontal * rotationSpeed * Time.deltaTime, 0));
    }
}
