using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public GameObject magnifier;
    public float lerpSpeed = 5f;
    float yRotation;
    // Update is called once per frame
    private void FixedUpdate()
    {
        if ( magnifier != null ) 
        { 
            Quaternion curRotation = transform.rotation;
            getRotation(magnifier.transform.position.x);
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, 
                new Quaternion(0, yRotation, 0, curRotation.w), 
                lerpSpeed * Time.deltaTime); 
        }
    }

    void getRotation(float axisValue)
    {
        yRotation = axisValue * 22.5f;
    }
}
