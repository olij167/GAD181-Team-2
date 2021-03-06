using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public GameObject player;
    public Image pizzaLauncherDisplay;
    public List<GameObject> tutorialTextList, tutorialInputsList, tutorialBodyTextList, tutorialChecksList;
    bool driveForwardActive, reverseActive, brakeActive, destinationActive, launcherActive, returnActive, tutorialComplete;
    public bool driveForwardFinished, reverseFinished, brakeFinished, confirmationTimerActive;
    int clickNum;
    public float confirmationTimer;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject tutorialUi in tutorialTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialInputsList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialBodyTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialChecksList)
        {
            tutorialUi.SetActive(false);
        }

        
    }

    private void Update()
    {
        if (confirmationTimerActive)
        {
            ConfirmationTimer();
        }
        if (driveForwardActive)
        {
            DriveForward();
        }

        if (reverseActive)
        {
            Reverse();
        }

        if (brakeActive)
        {
            Brake();
        }

        if (destinationActive)
        {
            FindDestination();
        }

        if (launcherActive)
        {
            PizzaLauncher();
        }

        if (returnActive)
        {
            ReturnToSpawn();
        }

        if (tutorialComplete)
        {
            TutorialComplete();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !tutorialComplete)
        {
            tutorialTextList[0].SetActive(true);
            tutorialTextList[1].SetActive(true);
            tutorialBodyTextList[0].SetActive(true);
            tutorialBodyTextList[1].SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !tutorialComplete)
        {
            tutorialTextList[0].SetActive(false);
            tutorialTextList[1].SetActive(false);
            tutorialBodyTextList[0].SetActive(false);
            tutorialBodyTextList[1].SetActive(false);

            DriveForwardVariables();
        }
    }

    void ConfirmationTimer()
    {
        confirmationTimer -= Time.deltaTime;

        if (confirmationTimer <= 0)
        {
            if (driveForwardFinished)
            {
                ReverseVariables();
            }

            if (reverseFinished)
            {
                BrakeVariables();
            }

            if (brakeFinished)
            {
                FindDestinationVariables();
            }
            confirmationTimerActive = false;
        }
    }
    void DriveForwardVariables()
    {

        tutorialTextList[2].SetActive(true);
        tutorialInputsList[0].SetActive(true);
        tutorialChecksList[3].SetActive(true);
        tutorialChecksList[4].SetActive(true);
        tutorialChecksList[5].SetActive(true);
        confirmationTimerActive = false;

        clickNum = 0;
        confirmationTimer = 1;

        driveForwardActive = true;
    }

    void DriveForward()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
            clickNum++;
        Debug.Log(clickNum);

        switch (clickNum)
        {
            case 1:
                {
                    tutorialChecksList[3].SetActive(false);
                    tutorialChecksList[0].SetActive(true);
                    break;
                }
            case 2:
                {
                    tutorialChecksList[4].SetActive(false);
                    tutorialChecksList[1].SetActive(true);
                    break;
                }
            case 3:
                {
                    tutorialChecksList[5].SetActive(false);
                    tutorialChecksList[2].SetActive(true);
                    driveForwardFinished = true;

                    confirmationTimerActive = true;
                    ConfirmationTimer();
                    break;
                }
            default:
                {
                    return;
                }
        }
    }
    void ReverseVariables()
    {

        foreach (GameObject tutorialUi in tutorialTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialInputsList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialBodyTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialChecksList)
        {
            tutorialUi.SetActive(false);
        }

        tutorialChecksList[3].SetActive(true);
        tutorialChecksList[4].SetActive(true);
        tutorialChecksList[5].SetActive(true);
        driveForwardActive = false;

        tutorialTextList[3].SetActive(true);
        tutorialInputsList[1].SetActive(true);
        confirmationTimerActive = false;
        clickNum = 0;
        confirmationTimer = 1;

        reverseActive = true;
    }

    void Reverse()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            clickNum++;
        Debug.Log(clickNum);

        switch (clickNum)
        {
            case 1:
                {
                    tutorialChecksList[3].SetActive(false);
                    tutorialChecksList[0].SetActive(true);
                    break;
                }
            case 2:
                {
                    tutorialChecksList[4].SetActive(false);
                    tutorialChecksList[1].SetActive(true);
                    break;
                }
            case 3:
                {
                    tutorialChecksList[5].SetActive(false);
                    tutorialChecksList[2].SetActive(true);

                    reverseFinished = true;
                    ConfirmationTimer();
                    break;
                }
            default:
                {
                    return;
                }
        }
    }

    void BrakeVariables()
    {
        foreach (GameObject tutorialUi in tutorialTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialInputsList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialBodyTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialChecksList)
        {
            tutorialUi.SetActive(false);
        }

        tutorialChecksList[3].SetActive(true);
        tutorialChecksList[4].SetActive(true);
        tutorialChecksList[5].SetActive(true);
        driveForwardActive = false;
        reverseActive = false;

        tutorialTextList[4].SetActive(true);
        tutorialInputsList[2].SetActive(true);
        confirmationTimerActive = false;

        clickNum = 0;
        confirmationTimer = 1;

        brakeActive = true;
    }
    void Brake()
    {

        if (Input.GetKeyUp(KeyCode.Mouse2) || Input.GetKeyUp(KeyCode.LeftShift))
            clickNum++;
        Debug.Log(clickNum);

        switch (clickNum)
        {
            case 1:
                {
                    tutorialChecksList[3].SetActive(false);
                    tutorialChecksList[0].SetActive(true);
                    break;
                }
            case 2:
                {
                    tutorialChecksList[4].SetActive(false);
                    tutorialChecksList[1].SetActive(true);
                    return;
                }
            case 3:
                {
                    tutorialChecksList[5].SetActive(false);
                    tutorialChecksList[2].SetActive(true);
                    brakeFinished = true;

                    confirmationTimerActive = true;
                    ConfirmationTimer();
                    return;
                }
            default:
                {
                    return;
                }
        }
    }

    void FindDestinationVariables()
    {
        foreach (GameObject tutorialUi in tutorialTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialInputsList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialBodyTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialChecksList)
        {
            tutorialUi.SetActive(false);
        }

        player.gameObject.GetComponent<SetRandomDestination>().temp = player.gameObject.GetComponent<SetRandomDestination>().resetTemp;
        player.gameObject.GetComponent<Condition>().condition = player.gameObject.GetComponent<Condition>().maxCondition;

        driveForwardActive = false;
        reverseActive = false;
        brakeActive = false;

        
        tutorialTextList[5].SetActive(true);
        tutorialTextList[6].SetActive(true);
        tutorialBodyTextList[2].SetActive(true);

        tutorialTextList[10].SetActive(true);
        tutorialTextList[11].SetActive(true);
        tutorialTextList[12].SetActive(true);
        tutorialTextList[13].SetActive(true);
        tutorialBodyTextList[6].SetActive(true);
        tutorialBodyTextList[7].SetActive(true);
        tutorialBodyTextList[8].SetActive(true);
        tutorialBodyTextList[9].SetActive(true);

        destinationActive = true;



    }
    void FindDestination()
    {

        if (pizzaLauncherDisplay.enabled)
        {
            
            PizzaLauncherVariables();
        }
    }

    void PizzaLauncherVariables()
    {
        foreach (GameObject tutorialUi in tutorialTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialInputsList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialBodyTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialChecksList)
        {
            tutorialUi.SetActive(false);
        }

        driveForwardActive = false;
        reverseActive = false;
        brakeActive = false;
        destinationActive = false;

        tutorialTextList[7].SetActive(true);
        tutorialInputsList[3].SetActive(true);
        tutorialBodyTextList[3].SetActive(true);
        tutorialBodyTextList[5].SetActive(true);

        tutorialTextList[10].SetActive(true);
        tutorialTextList[11].SetActive(true);
        tutorialTextList[12].SetActive(true);
        tutorialTextList[13].SetActive(true);
        tutorialBodyTextList[6].SetActive(true);
        tutorialBodyTextList[7].SetActive(true);
        tutorialBodyTextList[8].SetActive(true);
        tutorialBodyTextList[9].SetActive(true);

        launcherActive = true;
    }
    void PizzaLauncher()
    {

        if (GameObject.FindGameObjectWithTag("Pizza"))
        {
            ReturnToSpawnVariables();
        }
    }

    void ReturnToSpawnVariables()
    {
        foreach (GameObject tutorialUi in tutorialTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialInputsList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialBodyTextList)
        {
            tutorialUi.SetActive(false);
        }

        foreach (GameObject tutorialUi in tutorialChecksList)
        {
            tutorialUi.SetActive(false);
        }

        driveForwardActive = false;
        reverseActive = false;
        brakeActive = false;
        destinationActive = false;
        launcherActive = false;

        tutorialTextList[8].SetActive(true);
        tutorialInputsList[4].SetActive(true);
        tutorialBodyTextList[4].SetActive(true);

        tutorialTextList[10].SetActive(true);
        tutorialTextList[14].SetActive(true);
        tutorialBodyTextList[10].SetActive(true);
        tutorialBodyTextList[11].SetActive(true);
        tutorialBodyTextList[12].SetActive(true);
        tutorialBodyTextList[13].SetActive(true);
        tutorialBodyTextList[14].SetActive(true);
        tutorialBodyTextList[15].SetActive(true);

        returnActive = true;
    }

    void ReturnToSpawn()
    {
        driveForwardActive = false;
        reverseActive = false;
        brakeActive = false;
        destinationActive = false;
        launcherActive = false;

        if (Input.GetButton("R"))
        {
            tutorialTextList[8].SetActive(false);
            tutorialInputsList[4].SetActive(false);
            tutorialBodyTextList[4].SetActive(false);
            

            returnActive = false;
            tutorialComplete = true;
        }
    }

    void TutorialComplete()
    {
        Debug.Log("Tutorial Complete");

        tutorialTextList[9].SetActive(true);
        tutorialInputsList[5].SetActive(true);
        tutorialInputsList[6].SetActive(true);


        // pause menu but without resume option
    }

}
