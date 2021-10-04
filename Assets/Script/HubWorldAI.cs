using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HubWorldAI : MonoBehaviour
{
    //Call pathfinding agent
    public NavMeshAgent agent;

    public List<GameObject> checkpoints;

    public float speed = 1000f;

    public Vector3 walkPoint;
    public bool walkPointSet;


    void Start()
    {
        //fill list with all checkpoints in scene
        checkpoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
    }

    void Update()
    {
        Patroling();
    }

    private void Patroling()
    {
        // if no walkpoint, search for a new walkpoint
        if (!walkPointSet) SearchWalkPoint();

        // if there is a walkpoint, go there
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        // Speed controls - maybe where to look to change handling?
        Vector3 distanceToWalkPoint = (transform.position - walkPoint) * speed * Time.deltaTime;

        //walk point reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
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
