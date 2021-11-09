using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temperature : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 30;
    public int resetSecondsLeft = 30;
    public bool takingAway = false;
    public GameObject player;
    public bool deliveryComplete;

    void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    void Update()
    {
        //player.GetComponent<SetRandomDestination>().del




        if (secondsLeft <= 0)
        {
            secondsLeft = resetSecondsLeft;
        }
        if (takingAway == false && secondsLeft >= 0)
        {
            StartCoroutine(TimerTake());
        }
    }
    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }
        takingAway = false;
        GameOver();

        void GameOver()
        {

        }
    }
     
}
