using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    public GameObject turbo; // Turbo shield on NPC
    public GameObject playerTurbo; // Turbo shield on Player
    public PlayerController player;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Find the GameObject with the tag "Player" when the game starts
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        turbo.SetActive(true);
        playerTurbo.SetActive(false);
        canvas.SetActive(false);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            turbo.SetActive(false);
            playerTurbo.SetActive(true);
            player.moveSpeed *= 2;
            player.rotateSpeed *= 2;
            canvas.SetActive(true);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
