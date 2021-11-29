using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Image pizzaLauncherDisplay;
    public List<GameObject> introTextList, controlsTextList, pizzaLauncherTextList;
    public GameObject player;
    public bool introTextActive, controlsTextActive, pizzaLauncherTextActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UIControls();

        if (pizzaLauncherTextActive)
        {
            controlsTextActive = false;
            introTextActive = false;
        }
        if (controlsTextActive)
        {
            introTextActive = false;
            pizzaLauncherTextActive = false;
        }
        if (introTextActive)
        {
            controlsTextActive = false;
            pizzaLauncherTextActive = false;
        }

        //if (!pizzaLauncherDisplay.enabled)
        //{
        //    pizzaLauncherTextActive = false;
        //    foreach (GameObject tutorialText in pizzaLauncherTextList)
        //    {
        //        tutorialText.SetActive(false);
        //    }
        //}
        //else if (pizzaLauncherDisplay.enabled)
        //{
        //    pizzaLauncherTextActive = true;
        //    foreach (GameObject tutorialText in pizzaLauncherTextList)
        //    {
        //        tutorialText.SetActive(true);
        //    }
        //}
    }

    void UIControls()
    {
        if (Input.GetButtonDown("I"))
        {
            if (!introTextActive)
            {
                introTextActive = true;

                foreach (GameObject tutorialText in introTextList)
                {
                    tutorialText.SetActive(true);
                }
            }
            else
            {

                introTextActive = false;

                foreach (GameObject tutorialText in introTextList)
                {
                    tutorialText.SetActive(false);
                }
            }
        }

        if (Input.GetButtonDown("C"))
        {
            if (!controlsTextActive)
            {
                controlsTextActive = true;

                foreach (GameObject tutorialText in controlsTextList)
                {
                    tutorialText.SetActive(true);
                }
            }
            else
            {
                controlsTextActive = false;

                foreach (GameObject tutorialText in controlsTextList)
                {
                    tutorialText.SetActive(false);
                }
            }
        }

        if (Input.GetButtonDown("L"))
        {
            if (!pizzaLauncherTextActive)
            {
                pizzaLauncherTextActive = true;

                foreach (GameObject tutorialText in pizzaLauncherTextList)
                {
                    tutorialText.SetActive(true);
                }
            }
            else
            {

                pizzaLauncherTextActive = false;

                foreach (GameObject tutorialText in pizzaLauncherTextList)
                {
                    tutorialText.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Restaurant"))
        {
            foreach (GameObject tutorialText in introTextList)
            {
                tutorialText.SetActive(true);
            }
        }
    }
}
