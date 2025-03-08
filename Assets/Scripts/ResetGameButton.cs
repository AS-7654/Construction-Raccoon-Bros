using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetGameButton : MonoBehaviour
{
    private void Awake()
    {
        Button resetButton = GetComponentInChildren<Button>();
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetGame);
        }
        else
        {
            Debug.LogError("Reset button not found in children");
        }
    }

    private void ResetGame()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance missing");
            return;
        }

        // Reset game state through GameManager's public interface
        GameManager.Instance.ResetGameState();
       
        // Load first level scene
        if (GameManager.Instance.levels.Length > 0)
        {
            SceneManager.LoadScene(GameManager.Instance.levels[0].sceneName);
        }
        else
        {
            Debug.LogError("No levels configured in GameManager");
        }
    }
}