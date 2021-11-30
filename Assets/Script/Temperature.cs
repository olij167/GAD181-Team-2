using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Temperature : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 10;
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
        if (secondsLeft <= 0)
        {
            secondsLeft = resetSecondsLeft;
        }
        if (takingAway == false && secondsLeft >= 0)
        {
            StartCoroutine(TimerTake());
        }
        if (secondsLeft <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
    //i dont know if below code is still valid at this point?
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

    }

}
