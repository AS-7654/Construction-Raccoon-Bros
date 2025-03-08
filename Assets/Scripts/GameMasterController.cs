using UnityEngine;

public class GameMasterController : MonoBehaviour
{
    public GameObject canvas;
    public bool gameStarted;
    public float startTime;
    [SerializeField]
    private GameObject[] stars;


    public void PlayerReady()
    {
        gameStarted = true;
        startTime = Time.time; // Time when the button is pressed
        canvas.SetActive(false);
        foreach (var star in stars) star.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameStarted == false) canvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) canvas.SetActive(false);
    }
}