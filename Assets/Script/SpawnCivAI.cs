using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnCivAI : MonoBehaviour
{
    //Car spawn variables
    public GameObject pedestrian;
    public GameObject[] pedestrianSpawn;
    public List<GameObject> pedestrianList;

    //number of cars to spawn - set in the inspector
    public int pedestrianNum;

    //don't spawn on road
    public LayerMask roadMask;

    void Start()
    {
        pedestrianSpawn = new GameObject[pedestrianNum];

        // spawn desired number of cars upon start
        for (int i = 0; i < pedestrianNum; i++)
        {
            Vector3 randomBoardLocation = GetRandomSpawn();
            pedestrianSpawn[i] = Instantiate(pedestrian, randomBoardLocation, Quaternion.identity);
            pedestrianSpawn[i].GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

        pedestrianList.AddRange(pedestrianSpawn);
    }


    //not my script, found online ~~ chooses random points on nav mesh to spawn cars
    private Vector3 GetRandomSpawn()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int maxIndices = navMeshData.indices.Length - 3;

        // pick the first indice of a random triangle in the nav mesh
        int firstVertexSelected = UnityEngine.Random.Range(0, maxIndices);
        int secondVertexSelected = UnityEngine.Random.Range(0, maxIndices);

        // spawn on verticies
        Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];

        Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
        Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];

        // eliminate points that share a similar X or Z position to stop spawining in square grid line formations
        if ((int)firstVertexPosition.x == (int)secondVertexPosition.x || (int)firstVertexPosition.z == (int)secondVertexPosition.z || roadMask.Equals("roadMask"))
        {
            point = GetRandomSpawn(); // re-roll a position - I'm not happy with this recursion it could be better
        }
        else
        {
            // select a random point on it
            point = Vector3.Lerp(firstVertexPosition, secondVertexPosition, UnityEngine.Random.Range(0.05f, 0.95f));
        }

        return point;
    }
}
