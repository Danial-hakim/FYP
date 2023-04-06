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
    private void Start()
    {
        eyeTracker = EyeTracker.Instance;
        calibrationObject = Calibration.Instance;
        cam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLook>().cam;
        magnifierCam = transform.GetChild(0).GetComponent<Camera>();
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
            //updateCameraRotation();
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
        Quaternion curRotation = magnifierCam.gameObject.transform.rotation;
        Vector3 targetPosition = transform.position;
        Vector3 forwardDirection = (targetPosition - player.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(forwardDirection, Vector3.up);

        getRotation(transform.position.x);

        magnifierCam.gameObject.transform.rotation = Quaternion.Lerp(curRotation, yRotation, 1f);

        Debug.Log(magnifierCam.transform.rotation.y.ToString());
    }
    void getRotation(float axisValue)
    {
        yRotation = Quaternion.Euler(0f, axisValue * 22.5f, 0f);
    }

}
