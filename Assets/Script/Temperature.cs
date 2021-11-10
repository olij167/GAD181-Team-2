using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Temperature : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public float secondsLeft = 30;
    public float resetSecondsLeft = 30;
    //public bool takingAway = false;
    public GameObject player;
    public bool deliveryComplete;
    public int deliveryCounter = 0;

    void Start()
    {
        textDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + secondsLeft;
    }

    void Update()
    {
        deliveryComplete = player.GetComponent<SetRandomDestination>().deliveryComplete;


        if (!deliveryComplete) secondsLeft -= Time.deltaTime;
        else secondsLeft = resetSecondsLeft;
                if (secondsLeft <= 0) GameOver();

        textDisplay.text = secondsLeft.ToString("F0");
    }

    void GameOver()
    {
        secondsLeft = 0;
        // game over stuff
    }
    

    
     
}
