using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FadingPlatform : MonoBehaviour
{
    [SerializeField]
    private Collider platformCollider;
    // Time in seconds for the platform to fully fade out
    public float fadeDuration = 2f;

    // Flag to track if the player is on the platform
    [SerializeField]
    private bool isPlayerOnPlatform = false;

    // Reference to the platform's Renderer
    [SerializeField]
    private Renderer platformRenderer;

    // Store the original color of the platform
    [SerializeField]
    private Color originalColor;

    // Time elapsed since the fade started
    [SerializeField]
    private float fadeElapsed = 0f;
    // Start is called before the first frame update
    // Add this method inside your FadingPlatform class
[Button("Simulate Player On Platform")]
void SimulatePlayerOnPlatform()
{
    isPlayerOnPlatform = true;
    fadeElapsed = 0f; // Reset the fade timer
}
    void Start()
    {
        // Get the Renderer component of the platform
        platformRenderer = GetComponent<Renderer>();
        // Store the original color of the platform
        if (platformRenderer != null)
        {
            originalColor = platformRenderer.material.color;
        }
        else
        {
            Debug.LogError("Platform Renderer not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is on the platform, start fading
        if (isPlayerOnPlatform)
        {
            FadePlatform();
        }
        // Function to start the fade-out process
    }
    void FadePlatform()
    {
        // Increment the elapsed fade time
        fadeElapsed += Time.deltaTime;

        // Calculate the current alpha based on the elapsed time
        float alpha = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration);

        // Apply the new alpha to the platform's material color
        if (platformRenderer != null)
        {
            Color newColor = originalColor;
            newColor.a = alpha;
            platformRenderer.material.color = newColor;
        }

        // If the platform is fully faded, stop updating
        if (fadeElapsed >= fadeDuration)
        {
            isPlayerOnPlatform = false;
            fadeElapsed = fadeDuration; // Ensure alpha stays at 0
        }
    }
    // Trigger event when the player steps on the platform
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            fadeElapsed = 0f; // Reset the fade timer
            StartCoroutine(DisableColliderTemporarily());
        }
    }
    private IEnumerator DisableColliderTemporarily()
    {
        // Turn off the collider i
        if (platformCollider != null)
         { platformCollider.enabled = false; }
        // Wait for the same duration as the fade tim
        yield return new WaitForSeconds(fadeDuration);
        // Turn the collider back on
        if (platformCollider != null)
        { 
            platformCollider.enabled = true;
        } 
    }

    // Trigger event when the player leaves the platform
    private void OnTriggerExit(Collider other)
    {
        // Check if the player leaves the platform
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            ResetPlatform();
        }
    }

    // Function to reset the platform color
    void ResetPlatform()
    {
        if (platformRenderer != null)
        {
            platformRenderer.material.color = originalColor;
            fadeElapsed = 0f;
        }
    }
    }