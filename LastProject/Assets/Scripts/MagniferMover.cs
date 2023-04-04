using System.Collections;
using System.Collections.Generic;
using Tobii.Research;
using Tobii.Research.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class MagniferMover : MonoBehaviour
{
    //public float speed = 10.0f;
    //public Transform playerCamera;

    //void Update()
    //{
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");

    //    // Get the camera's local right, up, and forward vectors
    //    Vector3 right = playerCamera.right;
    //    Vector3 up = playerCamera.up;
    //    Vector3 forward = playerCamera.forward;

    //    // Project the forward vector onto the XZ plane
    //    forward.y = 0;

    //    // Normalize the vectors
    //    right.Normalize();
    //    up.Normalize();
    //    forward.Normalize();

    //    // Calculate the movement vector based on the input and camera orientation
    //    Vector3 movement = (horizontal * right + vertical * forward) * speed * Time.deltaTime;

    //    // Move the magnifier along with the camera
    //    transform.position += movement;
    //}
    [SerializeField] GameObject player;
    private EyeTracker eyeTracker;
    private Calibration calibrationObject;

    private Camera cam;
    private float distance = 2f;
    public float lerpSpeed = 5f;
    private void Start()
    {
        eyeTracker = EyeTracker.Instance;
        calibrationObject = Calibration.Instance;
        cam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLook>().cam;
        //tempBall = transform.GetChild(0).gameObject;
    }

    private void LateUpdate()
    {
        
    }
    private void FixedUpdate()
    {
        shootRay();
    }
    private void shootRay()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        // returns whether the combined gaze is valid. also sets ray to this data
        var valid = GetRay(out ray);
        if (valid)
        {
            Vector3 targetPosition = ray.GetPoint(distance);
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Get ray is false");
        }
    }

    bool GetRay(out Ray ray)
    {
        var data = eyeTracker.LatestGazeData;
        ray = data.CombinedGazeRayScreen;
        return data.CombinedGazeRayScreenValid;
    }

}
