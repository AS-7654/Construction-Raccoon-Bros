using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarPlayer : MonoBehaviour
{
    public int stars = 0; // An integer whole number
    public TMP_Text starText;
    public TMP_Text timeText;
    // Start is called before the first frame update
    public GameMasterController gamemaster;
    void Start()

{

   if(gamemaster == null) {

    gamemaster = FindObjectOfType<GameMasterController>();

}}

    // Update is called once per frame
    void Update()
    {
        starText.SetText("Stars: " + stars + "/5");
           if (stars < 100 && gamemaster.gameStarted == true)
        {
            timeText.SetText("Time: " + Mathf.Round(Time.time - gamemaster.startTime));
        }
    }
}