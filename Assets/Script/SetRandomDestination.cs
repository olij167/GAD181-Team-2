using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetRandomDestination : MonoBehaviour
{
    public GameObject[] destinationArray; // all potential destinations
    public GameObject destination; // selected destination
    public Material highlightedDestination;
    Material  originalDestinationMaterial;

    public float deliveryRange; // look radius
    public bool destinationSet, destinationInRange, deliveryComplete; //check if destination is set, check if destination is in range, check if delivery complete
    public LayerMask destinationLayer; 

    public int numOfDeliveries, deliveryCounter;

    // projectile variables
    public Transform shootPos;
    public GameObject pizza;
    public List<GameObject> pizzaList; // control pizzas
    public float shotPower, stopDistance; // pizza speed, pizza delivered proximity

    int destinationNum;

    // delivery complete placeholder UI
    public TextMeshProUGUI deliveriesCompleteUI, pizzaLauncherEngagedText;

    //temp variables
    public float temp, resetTemp;
    public TextMeshProUGUI tempUIValue;
    public Image tempBar;


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

        if (destinationInRange)
        {
            EngagePizzaLauncher(); // press space to shoot pizza
            pizzaLauncherEngagedText.text = "Pizza Launcher Engaged!";
        }
        else pizzaLauncherEngagedText.text = "Pizza Launcher Inactive.";

        deliveriesCompleteUI.text = deliveryCounter.ToString();

        temp -= Time.deltaTime;

        if (temp <= 0)
        {
            // game over
        }

        tempUIValue.text = temp.ToString("F0");
        tempBar.fillAmount = Mathf.Clamp(temp / resetTemp, 0, resetTemp);
        tempBar.color = Color.Lerp(Color.blue, Color.red, temp / resetTemp);
    }

    void SetDestination() // choose random destination
    {
        deliveryComplete = false;
        int randDestination = Random.Range(0, destinationArray.Length);

        for (int i = 0; i < destinationArray.Length; i++)
            destinationArray[i].layer = LayerMask.NameToLayer("BuildingLayer");

        destination = destinationArray[randDestination];
        originalDestinationMaterial = destination.GetComponent<MeshRenderer>().material;
        destination.GetComponent<MeshRenderer>().material = highlightedDestination;
        destination.layer = LayerMask.NameToLayer("Destination");
    }

    void EngagePizzaLauncher()
    {
        //pizzaList = new List<GameObject>();

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
        deliveryComplete = true;
        deliveryCounter++;
        temp = resetTemp;

        foreach (GameObject pizza in pizzaList)
        {
            Destroy(pizza);
        }

        pizzaList.Clear();

        if (deliveryCounter >= numOfDeliveries)
        {
            //game over scene transition
        }

        destination.GetComponent<MeshRenderer>().material = originalDestinationMaterial;

        SetDestination();
    }

    void OnDrawGizmosSelected()
    {
        // look radius visualiser
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, deliveryRange);
    }
}
