using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomDestination : MonoBehaviour
{
    public GameObject[] destinationArray; // all potential destinations
    public GameObject destination; // selected destination

    public float deliveryRange; // look radius
    public bool destinationSet, currentDeliveryComplete, destinationInRange;
    public LayerMask destinationLayer;

    public int numOfDeliveries, deliveryCounter;

    int destinationNum;
    void Start()
    {
        destinationNum = GameObject.FindGameObjectsWithTag("Building").Length;

        destinationArray = new GameObject[destinationNum];
        for (int i = 0; i < destinationNum; i++)
        {
            destinationArray[i] = GameObject.FindGameObjectsWithTag("Building")[i];
        }

        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {

        destinationInRange = Physics.CheckSphere(transform.position, deliveryRange, destinationLayer); // check if destination is within delivery range

        if (destinationInRange) DeliveryComplete(); //EngagePizzaLauncher();


    }

    void SetDestination() // choose random destination
    {
        int randDestination = Random.Range(0, destinationArray.Length);

        for (int i = 0; i < destinationArray.Length; i++)
            destinationArray[i].layer = LayerMask.NameToLayer("BuildingLayer");

        destination = destinationArray[randDestination];
        destination.layer = LayerMask.NameToLayer("Destination");
    }

    void EngagePizzaLauncher()
    {

    }

    void DeliveryComplete()
    {

        deliveryCounter++;
        SetDestination();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, deliveryRange);
    }
}
