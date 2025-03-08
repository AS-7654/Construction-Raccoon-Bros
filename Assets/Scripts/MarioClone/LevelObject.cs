using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelObject : ScriptableObject
{
    public int requiredStars;
    public AudioClip backgroundMusic;
    public string sceneName;
}