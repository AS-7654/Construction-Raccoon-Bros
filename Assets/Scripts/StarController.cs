using System.Diagnostics;
using UnityEngine;

public class StarController : MonoBehaviour
{
    float spinSpeed = 2f;

    public AudioClip collectSound;

void Start()
    {
        
    }

void Update()

        {
            transform.Rotate(Vector3.up * spinSpeed); // Roatate around y (up) axis

        }
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StarPlayer player = other.gameObject.GetComponent<StarPlayer>();
                player.stars += 1; // increase by 1
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
                gameObject.SetActive(false);

            }
        }
}