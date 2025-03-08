
using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public Transform cameraTransform;
    public string forwardKey = "w";
    public string leftKey = "a";
    public string backwardKey = "s";
    public string rightKey = "d";
    public float moveSpeed = 5f;  // Unified speed factor to simplify tuning

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not set. Please assign it in the inspector.");
            enabled = false; // Disable the script if cameraTransform is not set
            return;
        }
        
    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        if (cameraTransform != null)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 forward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
        Vector3 right = Quaternion.AngleAxis(90, Vector3.up) * forward;

        Vector3 movementDirection = Vector3.zero;

        if (Input.GetKey(forwardKey))
        {
            movementDirection += forward;
        }
        if (Input.GetKey(backwardKey))
        {
            movementDirection -= forward;
        }
        if (Input.GetKey(rightKey))
        {
            movementDirection += right;
        }
        if (Input.GetKey(leftKey))
        {
            movementDirection -= right;
        }

        rb.AddForce(movementDirection * moveSpeed);
    }
}
