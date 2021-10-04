using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HubWorldAI : MonoBehaviour
{
    //Call pathfinding agent
    public NavMeshAgent agent;

    public List<GameObject> checkpoints;

    public float speed = 1000f, stopDistance, walkPointTimer, walkPointTimerReset; 
    //stop distance = distance from checkpoint before a new point is chosen
    //walkpoint timer = time ai has to reach walkpoint before finding a new one
    //walkpoint timer reset = time for timer to reset to

    public Vector3 walkPoint, lastPosition;
    public bool walkPointSet;


    void Start()
    {
        //fill list with all checkpoints in scene
        checkpoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
    }

    void Update()
    {

        if (walkPointTimer > 0)
            walkPointTimer -= Time.deltaTime;

        Patroling();
    }

    private void Patroling()
    {
        // if no walkpoint, search for a new walkpoint
        if (!walkPointSet) SearchWalkPoint();

        // if there is a walkpoint, go there
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        lastPosition = transform.position;  // remember the last position so we can check if we are staying still
        
        // Speed controls - maybe where to look to change handling?
        Vector3 distanceToWalkPoint = (transform.position - walkPoint) * speed * Time.deltaTime;


        // get a new target point if you reach the target or you stopped moving
        if (distanceToWalkPoint.magnitude < stopDistance || walkPointTimer <= 0)
        {
            walkPointSet = false;
            walkPointTimer = walkPointTimerReset;
        }
    }
    private void SearchWalkPoint()
    {
        // choose random checkpoint
        float newCheckpoint = Random.Range(0, GameObject.FindGameObjectsWithTag("Checkpoint").Length + 1);

        //assign walkpoint to chosen checkpoint
        for (int i = 0; i < newCheckpoint; i++)
        {
            walkPoint = checkpoints[i].transform.position;
        }

        walkPointSet = true;
    }
}
