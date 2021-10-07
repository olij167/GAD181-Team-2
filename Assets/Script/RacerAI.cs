using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RacerAI : MonoBehaviour
{
    NavMeshAgent agent;

    public List<GameObject> raceCheckpointList; // add checkpoints to raceCheckpoints in order
    public GameObject nextCheckpoint; // for tracking positions
    public Vector3 racerLocation;
    public int numOfLaps, currentLap, checkpointsReached = 0;


    // end game stats
    public bool raceOverCheck, lapOverCheck;
    public float lapTimer, raceTimer, lapTimeAverage;
    float sum;
    public List<float> lapTimesList;
    public int positionInRace;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        RaceStarter raceInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<RaceStarter>();
        raceCheckpointList = raceInfo.raceCheckpointList;
        numOfLaps = raceInfo.numOfLaps;
        nextCheckpoint = raceCheckpointList[0];
        currentLap = 1;
    }

    void Update()
    {
        racerLocation = gameObject.transform.position;

        agent.SetDestination(nextCheckpoint.transform.position);
    }

    private void OnTriggerEnter(Collider other) // update checkpoint
    {
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
            //else 
            //{
            //    raceOverCheck = true;
            //    nextCheckpoint = raceCheckpointList[0];
            //}
        }
    }

    // for figuring out end positions later

    //void Timers() // track lap and total time
    //{
    //    if (currentLap >= numOfLaps)
    //    {
    //        raceOverCheck = true;
    //    }

    //    if (!raceOverCheck)
    //    {
    //        raceTimer += Time.deltaTime;

    //        if (!lapOverCheck)
    //        {
    //            lapTimer += Time.deltaTime;
    //        }
    //        else
    //        {
    //            lapTimesList.Add(lapTimer);
    //            lapTimer = 0f;
    //            lapOverCheck = false;
    //        }
    //    }
    //    else
    //    {
    //        raceTimer = Time.deltaTime;

    //        for (int i = 0; i < numOfLaps; i++)
    //        {
    //            sum += lapTimesList[i];
    //        }
    //        lapTimeAverage = sum / lapTimesList.Count;
    //    }
    //}
}
