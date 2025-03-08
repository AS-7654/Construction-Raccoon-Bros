using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject ball;
    private Vector3 prevBallPos;

    void Start()
    {
        prevBallPos = ball.transform.position;
    }

    void LateUpdate()
    {
        transform.Translate(ball.transform.position - prevBallPos, Space.World);
        prevBallPos = ball.transform.position;
    }
}