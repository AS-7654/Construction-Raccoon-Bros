using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
     // Speed of the platform's movement
    public float speed = 2f;   
    [SerializeField]   private Transform target;
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
            // Toggle the target between point A and point B
            target = (target == pointA) ? pointB : pointA;
        }
    }

}
