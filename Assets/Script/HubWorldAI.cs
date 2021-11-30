using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HubWorldAI : MonoBehaviour
{
    // pathfinding variables
    public NavMeshAgent agent;

    public string checkpointType; // write checkpoint type's tag in here

    public List<GameObject> checkpoints;

    public Vector3 walkPoint, lastPosition;
    public bool walkPointSet, playerFeedback;

    public float speed = 1000f, stopDistance, walkPointTimer, walkPointTimerReset;
                //stop distance is the distance from checkpoint before a new point is chosen

                //walkpoint timer is the time ai has to reach walkpoint before finding a new one

                //walkpoint timer reset is the time for timer to reset to

    public LayerMask roadMask;

    void Start()
    {
        //fill list with all checkpoints in scene
        checkpoints.AddRange(GameObject.FindGameObjectsWithTag(checkpointType));
    }

    void Update()
    {
        if (walkPointTimer > 0)
            walkPointTimer -= Time.deltaTime;
        else SearchWalkPoint();

        Patroling();

        
    }

    private void Patroling()
    {
        if (agent.isOnNavMesh) // make sure ai is on a nav mesh
        {
            // if no walkpoint, search for a new walkpoint
            if (!walkPointSet) SearchWalkPoint();

            // if there is a walkpoint, go there
            else agent.SetDestination(walkPoint);
        }
        //else transform.position = GetRandomPoint();

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
        float newCheckpoint = Random.Range(0, GameObject.FindGameObjectsWithTag(checkpointType).Length + 1);

        //assign walkpoint to chosen checkpoint
        for (int i = 0; i < newCheckpoint; i++)
        {
            walkPoint = checkpoints[i].transform.position;
        }

        walkPointSet = true;
    }

    //private Vector3 GetRandomPoint()
    //{
    //    NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

    //    int maxIndices = navMeshData.indices.Length - 3;

    //    // pick the first indice of a random triangle in the nav mesh
    //    int firstVertexSelected = UnityEngine.Random.Range(0, maxIndices);
    //    int secondVertexSelected = UnityEngine.Random.Range(0, maxIndices);

    //    // spawn on verticies
    //    Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];

    //    Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
    //    Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];

    //    // eliminate points that share a similar X or Z position to stop spawining in square grid line formations
    //    if ((int)firstVertexPosition.x == (int)secondVertexPosition.x || (int)firstVertexPosition.z == (int)secondVertexPosition.z || roadMask.Equals("roadMask"))
    //    {
    //        point = GetRandomPoint(); // re-roll a position - I'm not happy with this recursion it could be better
    //    }
    //    else
    //    {
    //        // select a random point on it
    //        point = Vector3.Lerp(firstVertexPosition, secondVertexPosition, UnityEngine.Random.Range(0.05f, 0.95f));
    //    }

    //    return point;
    //}
}
