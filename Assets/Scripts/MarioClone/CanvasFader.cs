using UnityEngine;

public class CanvasFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    public void FadeIn()
    {
        StartCoroutine(FadeCanvas(0, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvas(1, 0));
    }

    private System.Collections.IEnumerator FadeCanvas(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}