using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasFadeStart: MonoBehaviour
{
    private void Start()
    {
        CanvasFader canvasFade = GetComponent<CanvasFader>();
        if (canvasFade != null){
            GameStart(canvasFade);
        }
        else
        {
            Debug.LogError("Canvas not found in children");
        }
    }

    private void GameStart(CanvasFader canvasFader)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance missing");
            return;
        }

        canvasFader.FadeIn();

        // Reset game state through GameManager's public interface
        GameManager.Instance.canvasFader = canvasFader;
    }
}