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
    public GameObject racer;
    public List<GameObject> racerList;

    // checkpoint and lap tracking variables
    public int checkpointsReached = 0, currentLap;
    public bool raceOverCheck, lapOverCheck;

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // start race & get variables from race marker
        if (other.gameObject.CompareTag("RaceTrigger"))
        {
            CheckpointTrigger raceTrigger = other.GetComponent<CheckpointTrigger>();
            raceCheckpointList = raceTrigger.raceCheckpoints;
            startCheckpoint = raceCheckpointList[0];
            nextCheckpoint = startCheckpoint;
            currentLap = 1;
            numOfLaps = raceTrigger.numOfLaps;

            // spawn racers
            GameObject[] racers = new GameObject[raceTrigger.racerNum];

            for (int i = 0; i < raceTrigger.racerNum; i++)
            {
                racers[i] = Instantiate(racer, gameObject.transform.position, Quaternion.identity);
                racers[i].GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
            racerList.AddRange(racers);


            GameObject.FindWithTag("RaceTrigger").SetActive(false); // deactivate start triggers
        }

        // track player's next checkpoint
        if (other.gameObject.Equals(nextCheckpoint))
        {
            if (currentLap <= numOfLaps) // ensure under max laps
            {
                if (checkpointsReached < raceCheckpointList.Count - 1) // check if there are more checkpoints left in lap before adding
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
            else
            {
                // race over
                for (int i = 0; i < racerList.Count; i++)
                {
                    Destroy(racerList[i]);
                }
                racerList.Clear();
            }

            foreach (GameObject checkpoint in raceCheckpointList)
            {
                checkpoint.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    void RaceOver()
    {
        // leaderboard & rewards
        //return to hub world
    }


}
