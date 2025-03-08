using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrack : MonoBehaviour
{
    public GameObject ball;
    public GameObject level;
    private Vector3 startPosition;
    private Quaternion levelStartRotation;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = ball.transform.position;
        levelStartRotation = level.transform.rotation;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            level.transform.rotation = levelStartRotation;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.transform.position = startPosition;
        }
    }
}