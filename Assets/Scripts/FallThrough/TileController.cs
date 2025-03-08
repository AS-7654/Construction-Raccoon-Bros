using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Material startColour;
    public Material safeColour;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = startColour;
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Safe")
        {
            rend.sharedMaterial = safeColour;
        }
    }
}