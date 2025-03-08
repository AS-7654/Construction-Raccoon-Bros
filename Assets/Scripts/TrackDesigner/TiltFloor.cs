using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TiltFloor : MonoBehaviour
{
    public float maxTilt;
    public float turnSpeed;

    public string forwardKey;
    public string leftKey;
    public string backwardKey;
    public string rightKey;

    // Update is called once per frame
    void Update()
    {
        float targetXRotation = 0;
        float targetZRotation = 0;

        if (Input.GetKey(forwardKey)){
            targetXRotation += maxTilt;
        }

        if (Input.GetKey(backwardKey)){
            targetXRotation += 360 - maxTilt;
        }

        if (Input.GetKey(rightKey)){
            targetZRotation += 360 - maxTilt;
        }

        if (Input.GetKey(leftKey)){
            targetZRotation += maxTilt;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetXRotation, 0, targetZRotation), turnSpeed * Time.deltaTime);
    }
}