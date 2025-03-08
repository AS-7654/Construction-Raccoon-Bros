using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 🚀 Class to handle the movement of a platform between multiple points dynamically.
/// The platform moves to each point in the specified list in sequence and loops back to the start.
/// </summary>
public class ListMovingPlatform : MonoBehaviour
{
    // List of points for the platform to move between
    [SerializeField] private List<Transform> points = new List<Transform>();

    /// <summary>
    /// 🕒 Speed of the platform's movement
    /// </summary>
    public float speed = 2f;

    // Index of the current target in the points list
    private int currentTargetIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure there are points in the list
        if (points.Count == 0)
        {
            Debug.LogError("🚨 No points specified for the platform to move between!");
            return;
        }

        // Log initial state
        Debug.Log($"✅ Initial target set to: {points[currentTargetIndex].name}");
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Count == 0)
        {
            return; // Exit if no points are defined
        }

        // Log the current target and move the platform
        Debug.Log($"🔄 Moving towards: {points[currentTargetIndex].name}");
        MovePlatform();

        // Check if the platform has reached the target and update the target if necessary
        CheckTargetReached();
    }

    /// <summary>
    /// 📍 Moves the platform towards the current target.
    /// </summary>
    void MovePlatform()
    {
        // Calculate the step based on speed and deltaTime
        float step = speed * Time.deltaTime;

        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, points[currentTargetIndex].position, step);

        Debug.Log($"🚶‍♂️ Platform moving. Current position: {transform.position}");
    }

    /// <summary>
    /// 🔍 Checks if the platform has reached the current target and updates the target index.
    /// </summary>
    void CheckTargetReached()
    {
        // Check if the platform is close enough to the current target
        if (Vector3.Distance(transform.position, points[currentTargetIndex].position) < 0.1f)
        {
            Debug.Log($"✅ Reached target: {points[currentTargetIndex].name}");

            // Update the target index to the next point, loop back if at the end
            currentTargetIndex = (currentTargetIndex + 1) % points.Count;

            Debug.Log($"🎯 New target set to: {points[currentTargetIndex].name}");
        }
    }
}
