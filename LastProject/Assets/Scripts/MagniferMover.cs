using System.Collections;
using System.Collections.Generic;
using Tobii.Research;
using Tobii.Research.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class MagniferMover : MonoBehaviour
{
    [SerializeField] GameObject player;
    private EyeTracker eyeTracker;
    private Calibration calibrationObject;

    private Camera cam;
    private float distance = 2f;
    public float lerpSpeed = 5f;

    private Camera magnifierCam;

    Quaternion yRotation;

    float startingVal;
    private void Start()
    {
        eyeTracker = EyeTracker.Instance;
        calibrationObject = Calibration.Instance;
        cam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLook>().cam;
        magnifierCam = transform.GetChild(0).GetComponent<Camera>();

        startingVal = eyeTracker.LatestGazeData.Left.PupilDiameter;
    }
    private void Update()
    {
        UpdateZoomLevel();
    }
    private void FixedUpdate()
    {
        //shootRay();
        updateCameraRotation();

        //if(Input.GetKeyDown(KeyCode.J)) 
        //{
        //    Vector3 newPosition = transform.position + new Vector3(1f, 0f, 0f); // Calculate new position
        //    transform.position = newPosition;
        //}
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
    }

    bool GetRay(out Ray ray)
    {
        var data = eyeTracker.LatestGazeData;
        ray = data.CombinedGazeRayScreen;
        return data.CombinedGazeRayScreenValid;
    }

    void updateCameraRotation()
    {
        Quaternion curRotation = magnifierCam.transform.localRotation;
        getRotation(new Vector2(transform.position.x, transform.position.y));

        magnifierCam.transform.localRotation = Quaternion.Lerp(curRotation, yRotation, 1f);
    }
    void getRotation(Vector2 axisValue)
    {
        yRotation = Quaternion.Euler(0f, -axisValue.x * 22.5f, 0f);
    }

    void UpdateZoomLevel()
    {
        bool leftEyeOpen = eyeTracker.LatestGazeData.Left.PupilDiameterValid;
        bool rightEyeOpen = eyeTracker.LatestGazeData.Right.PupilDiameterValid;
        if (!leftEyeOpen)
        {
            magnifierCam.fieldOfView = 4;
        }
        else if (!rightEyeOpen)
        {
            magnifierCam.fieldOfView = 12;
        }
        else
        {
            magnifierCam.fieldOfView = 8;
        }
    }
}
