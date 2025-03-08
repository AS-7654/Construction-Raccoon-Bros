using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoMovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    public Transform pointE;
    // Speed of the platform's movement
    public float speed = 2f;  
    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial target to point A
        target = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform towards the current target
        MovePlatform();

        // Check if the platform has reached the target
        CheckTargetReached();
    }

    // Function to move the platform towards the target
    void MovePlatform()
    {
        // Calculate the step based on speed and time
        float step = speed * Time.deltaTime;

        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    // Function to check if the platform has reached the target
    void CheckTargetReached()
    {
        // If the platform is at the target position
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // Toggle the target between points in a sequence
            if (target == pointA)
                target = pointB;
            else if (target == pointB)
                target = pointC;
            else if (target == pointC)
                target = pointD;
            else if (target == pointD)
                target = pointE;
            else if (target == pointE)
                target = pointA;
        }
    }
}