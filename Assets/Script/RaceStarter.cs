using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStarter : MonoBehaviour
{
    // race set-up variables
    public int numOfLaps;

    // checkpoint identification variables
    public List<GameObject> raceCheckpointList;
    public GameObject nextCheckpoint, startCheckpoint;

    // racer variables
    //public GameObject racer;
    //public List<GameObject> racerList;

    // checkpoint and lap tracking variables
    public int checkpointsReached = 0, currentLap;
    public bool raceOverCheck, lapOverCheck;

    public GameObject arrow;

    public float deliveryRange, feedbackTimer, feedbackTimerReset, deliveryCounter, numOfDeliveries; // look radius
    public bool destinationSet, destinationInRange, playerFeedback; //check if destination is set, check if destination is in range, check if delivery complete, check if player requires feedback
    public LayerMask destinationLayer;
    public Vector3 distanceToDestination;

    void Update()
    {
        destinationInRange = Physics.CheckSphere(transform.position, deliveryRange, destinationLayer); // check if destination is within delivery range
        distanceToDestination = transform.position - nextCheckpoint.transform.position;

        if (destinationInRange)
        {
           
            arrow.GetComponent<MeshRenderer>().material.color = gameObject.GetComponent<SetRandomDestination>().highlightedDestination.color;

        }

        if (!destinationInRange)
        {
           
            arrow.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.red, gameObject.GetComponent<SetRandomDestination>().cold, distanceToDestination.magnitude / transform.position.magnitude);
        }

        for (int i = 0; i < raceCheckpointList.Count; i++)
        {
            if (raceCheckpointList[i] == nextCheckpoint)
            {
                raceCheckpointList[i].GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                raceCheckpointList[i].GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // start race & get variables from race marker
        if (other.gameObject.CompareTag("Restaurant"))
        {
            CheckpointTrigger raceTrigger = other.GetComponent<CheckpointTrigger>();
            raceCheckpointList = raceTrigger.raceCheckpoints;
            startCheckpoint = raceCheckpointList[0];
            nextCheckpoint = startCheckpoint;
            currentLap = 1;
            numOfLaps = raceTrigger.numOfLaps;

            // spawn racers
            //GameObject[] racers = new GameObject[raceTrigger.racerNum];

            //for (int i = 0; i < raceTrigger.racerNum; i++)
            //{
            //    racers[i] = Instantiate(racer, gameObject.transform.position, Quaternion.identity);
            //    racers[i].GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            //}
            //racerList.AddRange(racers);


            GameObject.FindWithTag("Restaurant").SetActive(false); // deactivate start triggers
        }

        // track player's next checkpoint
        if (other.gameObject.Equals(nextCheckpoint))
        {
            if (currentLap <= numOfLaps) // ensure under max laps
            {
                if (checkpointsReached < raceCheckpointList.Count) // check if there are more checkpoints left in lap before adding
                {
                    checkpointsReached++;
                    nextCheckpoint = raceCheckpointList[raceCheckpointList.IndexOf(nextCheckpoint) + 1];
                }
                else // if they aren't, start next lap
                {
                    lapOverCheck = true;
                    checkpointsReached = 0;
                    nextCheckpoint = raceCheckpointList[0];
                    currentLap++;
                }

            }
            //else
            //{
            //    // race over
            //    for (int i = 0; i < racerList.Count; i++)
            //    {
            //        Destroy(racerList[i]);
            //    }
            //    racerList.Clear();
            //}

            foreach (GameObject checkpoint in raceCheckpointList)
            {
                checkpoint.GetComponent<BoxCollider>().enabled = true;
            }

            if (currentLap >= numOfLaps)
            {
                raceOverCheck = true;
                gameObject.GetComponent<SetRandomDestination>().enabled = true;
                gameObject.GetComponent<RaceStarter>().enabled = false;
            }
        }
    }

    void RaceOver()
    {
        // leaderboard & rewards
        //return to hub world
    }


}
