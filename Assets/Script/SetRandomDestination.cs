using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetRandomDestination : MonoBehaviour
{
    public GameObject[] destinationArray; // all potential destinations
    public GameObject destination; // selected destination

    public float deliveryRange; // look radius
    public bool destinationSet, destinationInRange; //check if destination is set, check if destination is in range
    public LayerMask destinationLayer; 

    public int numOfDeliveries, deliveryCounter;

    // projectile variables
    public Transform shootPos;
    public GameObject pizza;
    public List<GameObject> pizzaList; // control pizzas
    public float shotPower, stopDistance; // pizza speed, pizza delivered proximity

    int destinationNum;

    // delivery complete placeholder UI
    public TextMeshProUGUI deliveriesCompleteUI;


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

        //if (destinationInRange) 
            EngagePizzaLauncher(); // press space to shoot pizza

        deliveriesCompleteUI.text = deliveryCounter.ToString();


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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(pizza, shootPos.position, shootPos.rotation);

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Pizza").Length; i++)
            {
                GameObject newPizza = GameObject.FindGameObjectsWithTag("Pizza")[i];

                if (!pizzaList.Contains(newPizza))
                {
                    pizzaList.Add(newPizza); // add new pizza to list
                }
            }
        }

        foreach (GameObject pizza in pizzaList)
        {
            pizza.transform.position = Vector3.MoveTowards(pizza.transform.position, destination.transform.position, shotPower * Time.deltaTime); // send pizza to the destination

            Vector3 distanceToWalkPoint = pizza.transform.position - destination.transform.position;

            if (distanceToWalkPoint.magnitude < stopDistance) // check if pizza is at destination
            {
                DeliveryComplete();
            }
        }
    }

    void DeliveryComplete()
    {
        deliveryCounter++;

        foreach (GameObject pizza in pizzaList)
            Destroy(pizza);

        pizzaList.Clear();

        SetDestination();

        if (deliveryCounter >= numOfDeliveries)
        {
            //game over scene transition
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, deliveryRange);
    }
}
