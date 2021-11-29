using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial1 : MonoBehaviour
{
    public bool tutorialMissionActive = true, controlsTutorialComplete, deliveryTutorialComplete;
    public GameObject arrow;
    void Start()
    {
        gameObject.GetComponent<SetRandomDestination>().enabled = false;
        arrow.GetComponent<DestinationArrow>().enabled = false;
        arrow.GetComponent<TutorialDestinationArrow>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<RaceStarter>().raceOverCheck)
        {
            controlsTutorialComplete = true;
            gameObject.GetComponent<RaceStarter>().enabled = false;
            gameObject.GetComponent<SetRandomDestination>().enabled = true;

            arrow.GetComponent<TutorialDestinationArrow>().enabled = false;
            arrow.GetComponent<DestinationArrow>().enabled = true;
            
            Debug.Log("controls tutorial complete");
        }

        if (gameObject.GetComponent<SetRandomDestination>().deliveryCounter > 0)
        {
            deliveryTutorialComplete = true;
            Debug.Log("delivery tutorial complete");
        }

        if (controlsTutorialComplete && deliveryTutorialComplete)
        {
            // scene navigation options
        }
    }
}
