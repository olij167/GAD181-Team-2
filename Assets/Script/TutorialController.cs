using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Image introText, controlsText, pizzaLauncherText, pizzaLauncherDisplay;
    public List<TextMeshProUGUI> introTextList, controlsTextList, pizzaLauncherTextList;
    public GameObject player, introTrigger;
    public bool pizzaLauncherActive;

    // Start is called before the first frame update
    void Start()
    {
        introText.enabled = false;
        controlsText.enabled = true;
        pizzaLauncherText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (introText.enabled)
        {
            foreach (TextMeshProUGUI tutorialText in introTextList)
            {
                tutorialText.enabled = true;
            }
        }
        else
        {
            foreach (TextMeshProUGUI tutorialText in introTextList)
            {
                tutorialText.enabled = false;
                introText.enabled = false;
            }

        }

        if (pizzaLauncherDisplay.enabled)
        {
            pizzaLauncherText.enabled = true;

            foreach (TextMeshProUGUI tutorialText in pizzaLauncherTextList)
            {
                tutorialText.enabled = true;
            }
        }
        else
        {

            pizzaLauncherText.enabled = false;

            foreach (TextMeshProUGUI tutorialText in pizzaLauncherTextList)
            {
                tutorialText.enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (controlsText.enabled)
            {
                controlsText.enabled = false;

                foreach (TextMeshProUGUI tutorialText in controlsTextList)
                {
                    tutorialText.enabled = false;
                }
            }
            else
            {
                controlsText.enabled = true;

                foreach (TextMeshProUGUI tutorialText in controlsTextList)
                {
                    tutorialText.enabled = true;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == introTrigger)
        {
            introText.enabled = true;
            foreach (TextMeshProUGUI tutorialText in introTextList)
            {
                tutorialText.enabled = true;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != introTrigger)
        {
            introText.enabled = false;
            foreach (TextMeshProUGUI tutorialText in introTextList)
            {
                tutorialText.enabled = false;
            }

        }
    }
}
