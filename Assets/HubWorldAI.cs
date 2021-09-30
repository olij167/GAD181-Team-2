using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HubWorldAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public List<GameObject> checkpoints;
    //public Transform checkpoint;

    public float speed = 1000f;


    public Vector3 walkPoint;
    public bool walkPointSet;


    void Start()
    {
        checkpoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
    }

    // Update is called once per frame
    void Update()
    {
        Patroling();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();


        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = (transform.position - walkPoint) * speed * Time.deltaTime;

        //walk point reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        // calculate random point in range
        float newCheckpoint = Random.Range(0, GameObject.FindGameObjectsWithTag("Checkpoint").Length + 1);

        for (int i = 0; i < newCheckpoint; i++)
        {
            walkPoint = checkpoints[i].transform.position;
        }

        

        walkPointSet = true;
    }
}
