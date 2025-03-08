using UnityEngine;
using System.Collections;

public class GameEnd : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        // Get the CanvasGroup component attached to this GameObject
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component is missing on GameEnd GameObject.");
        }

        // Ensure the canvas is invisible on awake
        SetCanvasAlpha(0f);
    }

    private void OnEnable()
    {
        // Subscribe to the GameManager's OnGameEnd event
        if (GameManager.Instance != null)
        {
            GameManager.OnGameEnd += HandleGameEnd;
        }
        else
        {
            Debug.LogWarning("GameManager instance is null. Unable to subscribe to OnGameEnd event.");
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the GameManager's OnGameEnd event
        if (GameManager.Instance != null)
        {
            GameManager.OnGameEnd -= HandleGameEnd;
        }
    }

    private void HandleGameEnd()
    {
        Debug.Log("Handling Game End: Fading in the GameEnd canvas.");

        if (canvasGroup != null)
        {
            // Fade in the canvas over 1 second
            StartCoroutine(FadeCanvas(0f, 1f, 1f));
        }
        else
        {
            Debug.LogError("CanvasGroup is null. Cannot fade in GameEnd canvas.");
        }
    }

    private IEnumerator FadeCanvas(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        // Gradually interpolate the alpha value
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            SetCanvasAlpha(Mathf.Lerp(startAlpha, endAlpha, t));
            yield return null;
        }

        // Ensure the final alpha value is set
        SetCanvasAlpha(endAlpha);
    }

    private void SetCanvasAlpha(float alpha)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
            canvasGroup.interactable = alpha > 0.5f; // Enable interaction only when mostly visible
            canvasGroup.blocksRaycasts = alpha > 0.5f; // Enable raycasts only when mostly visible
        }
    }
}