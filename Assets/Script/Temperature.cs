using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Temperature : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public int secondsLeft = 30;
    public int resetSecondsLeft = 30;
    public bool takingAway = false;
    public GameObject player;
    public bool deliveryComplete;

    void Start()
    {
        textDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + secondsLeft;
    }

    void Update()
    {
        

        if (takingAway == false && secondsLeft >= 0)
        {
            StartCoroutine(TimerTake());
        }

        if (secondsLeft <= 0)
        {
            //game over
        }
    }
    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            textDisplay.GetComponent<TextMeshProUGUI>().text = "00:0" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + secondsLeft;
        }

        deliveryComplete = player.GetComponent<SetRandomDestination>().deliveryComplete;

        if (deliveryComplete)
        {
            ResetTimer();
        }

        takingAway = false;
        GameOver();

        void GameOver()
        {

        }
        
        void ResetTimer()
        {
            secondsLeft = resetSecondsLeft;
        }
    }

    
     
}
