using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartButton: MonoBehaviour
{
    private void Awake()
    {
        Button startButton = GetComponentInChildren<Button>();
        if (startButton != null)
        {
            startButton.onClick.AddListener(GameStart);
        }
        else
        {
            Debug.LogError("Start button not found in children");
        }
    }

    private void GameStart()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance missing");
            return;
        }

        // Reset game state through GameManager's public interface
        GameManager.Instance.GameStart();
    }
}