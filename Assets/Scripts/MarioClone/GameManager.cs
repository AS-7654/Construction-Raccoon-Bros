using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using NaughtyAttributes;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int levelIndex = 0;
    public static event Action OnGameEnd;
    public CanvasFader canvasFader;
    public static GameManager Instance { get; private set; }

    public LevelObject[] levels;
    public TMP_Text levelText;
    public TMP_Text starsText;
    public TMP_Text messageText;
    public AudioSource audioSource;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int collectedStars;

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager instance created.");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate GameManager destroyed.");
        }
    }

    void Start()
    {
        if (Instance == this)
        {
            if (levels == null || levels.Length == 0)
            {
                Debug.LogError("Levels array is not set or empty.");
                return;
            }
        }
    }

    [Button("Game Start")]

    public void GameStart()
    {
            // Initializing UI elements
            InitializeUIElements();

            currentLevel = 0;
            collectedStars = 0;
            UpdateUI();
            PlayBackgroundMusic();
            Debug.Log("GameManager initialized.");
            canvasFader.FadeOut();
    }

    void OnEnable()
    {
        // Subscribe to sceneLoaded event to handle UI element reassignments
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("Subscribed to sceneLoaded event.");
    }

    void OnDisable()
    {
        // Unsubscribe from sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("Unsubscribed from sceneLoaded event.");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        // Reassign UI elements after scene load
        InitializeUIElements();
        UpdateUI();
        PlayBackgroundMusic();
    }

    private void InitializeUIElements()
    {
        // Find UI elements in the scene
        levelText = GameObject.Find("LevelText")?.GetComponent<TMP_Text>();
        starsText = GameObject.Find("StarsText")?.GetComponent<TMP_Text>();
        messageText = GameObject.Find("MessageText")?.GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();

        if (levelText == null || starsText == null || messageText == null || audioSource == null)
        {
            Debug.LogError("UI Text elements or AudioSource are not set. Make sure they are named correctly in the scene.");
        }
        else
        {
            Debug.Log("UI elements and AudioSource successfully initialized.");
        }
    }

    public void CollectStar()
    {
        collectedStars++;
        Debug.Log("Star collected! Total stars: " + collectedStars);
        UpdateUI();

        if (collectedStars >= levels[currentLevel].requiredStars)
        {
            Debug.Log("Collected enough stars to complete the level.");
            CompleteLevel();
        }
        else
        {
            Debug.Log("Need more stars to complete the level.");
        }
    }

    public void ShowStarCollectedMessage()
    {
        messageText.text = "I got a star!";
        StartCoroutine(ClearMessageAfterDelay(2f));
        Debug.Log("Star collected message displayed.");
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
        Debug.Log("Message text cleared after delay.");
    }

    public void CompleteLevel()
    {
        if (collectedStars >= levels[currentLevel].requiredStars)
        {
            Debug.Log("Level " + (currentLevel + 1) + " completed.");
            messageText.text = "Level Completed!";
            currentLevel++;
            levelIndex++;
            collectedStars = 0;
            if (currentLevel >= levels.Length)
            {
                Debug.Log("All levels completed.");
                messageText.text = "All Levels Completed!";
                // Trigger the game end event
                EndGame();
            }
            else
            {
                StartCoroutine(LoadNextLevelAfterDelay(2f)); // Wait for 2 seconds before loading next level
                Debug.Log("Preparing to load next level.");
            }
        }
        else
        {
            Debug.LogWarning("Not enough stars to complete the level.");
            messageText.text = "Not enough stars to complete the level!";
        }
    }

    private IEnumerator LoadNextLevelAfterDelay(float delay)
{
    Debug.Log("Waiting for " + delay + " seconds before loading next level.");
    yield return new WaitForSeconds(delay);
    LoadNextLevel(levelIndex);  // Pass levelIndex to LoadNextLevel
}

private void LoadNextLevel(int levelIndex)
{
    if (levelIndex < levels.Length)
    {
        string nextSceneName = levels[levelIndex].sceneName;  // Use levelIndex to load the next level's scene

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log("Loading scene: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set for level " + (levelIndex + 1));
        }
    }
    else
    {
        Debug.LogError("No more levels to load.");
    }
}

    void UpdateUI()
    {
        Debug.Log("UpdateUI method called.");

        if (levelText == null || starsText == null || messageText == null)
        {
            Debug.LogError("UI Text elements are not set. Cannot update UI.");
            return;
        }

        if (currentLevel < levels.Length)
        {
            levelText.text = "Level: " + (currentLevel + 1);
            starsText.text = "Stars: " + collectedStars + "/" + levels[currentLevel].requiredStars;
            messageText.text = "";

            Debug.Log("Level text updated to: " + levelText.text);
            Debug.Log("Stars text updated to: " + starsText.text);
            Debug.Log("Message text cleared.");

            Debug.Log("UI updated for level " + (currentLevel + 1));
        }
        else
        {
            Debug.LogWarning("Current level index (" + currentLevel + ") is out of bounds. Total levels available: " + levels.Length);
        }
    }

    void PlayBackgroundMusic()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not set. Cannot play background music.");
            return;
        }

        if (currentLevel < levels.Length && levels[currentLevel].backgroundMusic != null)
        {
            audioSource.clip = levels[currentLevel].backgroundMusic;
            audioSource.Play();
            Debug.Log("Playing background music for level " + (currentLevel + 1));
        }
        else
        {
            Debug.LogWarning("No background music set for level " + (currentLevel + 1));
        }
    }

[Button("Test CollectStar")]

    // Methods for testing via button
    public void TestCollectStar()
    {
        Debug.Log("TestCollectStar method called.");
        CollectStar();
    }
    
[Button("Test LoadLevel")]
    public void TestLoadLevel()
    {
        Debug.Log("TestLoadLevel called with levelIndex: " + levelIndex);
        if (levelIndex >= 0 && levelIndex < levels.Length)
        {
            collectedStars = 0;
            LoadNextLevel(levelIndex);
        }
        else
        {
            Debug.LogError("Invalid level index: " + levelIndex);
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Ended"); OnGameEnd?.Invoke();
        // Trigger the event for listeners
    }

    public void ResetGameState()
    {
    currentLevel = 0;
    levelIndex = 0;
    collectedStars = 0;
    UpdateUI();
   
    // Add any additional reset logic needed for your game
    Debug.Log("Game state reset to initial values");
    }
}