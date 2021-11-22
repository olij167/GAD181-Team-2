using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SetRandomDestination : MonoBehaviour
{
    // destination indicator variables
    public GameObject[] destinationArray; // all potential destinations
    public GameObject destination, arrow; // selected destination
    public Material highlightedDestination;
    Material  originalDestinationMaterial;

    //destination finding variables
    public float deliveryRange; // look radius
    public bool destinationSet, destinationInRange; //check if destination is set, check if destination is in range, check if delivery complete
    public LayerMask destinationLayer;
    public Vector3 distanceToDestination;


    public int numOfDeliveries, deliveryCounter;

    // projectile variables
    public Transform shootPos;
    public GameObject pizza;
    public List<GameObject> pizzaList; // control pizzas
    public float shotPower, stopDistance; // pizza speed, pizza delivered proximity
    public bool launcherActive;

    int destinationNum;

    // delivery complete placeholder UI
    public TextMeshProUGUI deliveriesCompleteUI, pizzaLauncherText, engagedText, distanceToDestinationText;
    public Image pizzaEngagedBackground, pizzaEngagedBorder;

    //temp variables
    public float temp, resetTemp;
    public TextMeshProUGUI tempUIValue;
    public Image tempBar;
    Color hot, warm, cold;

    // audio variables
    public AudioSource pizzaLaunched;
    public AudioSource pizzaDelivered;
  

    void Start()
    {

        cold = new Color(0.2282118f, 0.2282118f, 0.6235294f, 1f);
        warm = highlightedDestination.color;
        hot = new Color(0.6235294f, 0.2282118f, 0.227451f, 1f);

        pizzaEngagedBackground.color = hot;

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
        distanceToDestination = transform.position - destination.transform.position;
        distanceToDestinationText.text = distanceToDestination.magnitude.ToString("f0") + "m";

        if (destinationInRange)
        {
            pizzaLauncherText.enabled = true;
            pizzaEngagedBorder.enabled = true;
            pizzaEngagedBackground.enabled = true;
            
            EngagePizzaLauncher(); // press space to shoot pizza
            engagedText.text = "Engaged!";

            arrow.GetComponent<MeshRenderer>().material.color = highlightedDestination.color;

        }
        
        if (!destinationInRange)
        {
            engagedText.text = null;
            pizzaEngagedBorder.enabled = false;
            pizzaEngagedBackground.enabled = false;
            pizzaLauncherText.enabled = false;

            arrow.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.red, cold, distanceToDestination.magnitude / transform.position.magnitude);
        }

        // send pizza to destination
        foreach (GameObject pizza in pizzaList)
        {
            if (pizza == null) pizzaList.Remove(pizza);

            if (pizza != null)
            {
                pizza.transform.position = Vector3.MoveTowards(pizza.transform.position, destination.transform.position, shotPower * Time.deltaTime); // move the pizza towards the position of the delivery destination

                Vector3 distanceToWalkPoint = pizza.transform.position - destination.transform.position; // the distance between the pizza and the destination

                if (distanceToWalkPoint.magnitude < stopDistance) // check if pizza has reached destination
                    DeliveryComplete();
            }
        }

        deliveriesCompleteUI.text = deliveryCounter.ToString();

        temp -= Time.deltaTime;

        if (temp <= 0)
        {
            // game over
        }

        tempUIValue.text = temp.ToString("F0");
        float fillAmount = temp / resetTemp;
        tempBar.fillAmount = fillAmount;
        if (temp > resetTemp/2)
            tempBar.color = Color.Lerp(warm, hot, temp / resetTemp);
        else tempBar.color = Color.Lerp(cold, warm, temp / resetTemp);
    }

    public void SetDestination() // choose random destination
    {
        int randDestination = Random.Range(0, destinationArray.Length); //generate a random number within the amount of buildings

        //for (int i = 0; i < destinationArray.Length; i++)
        //    destinationArray[i].layer = LayerMask.NameToLayer("BuildingLayer");

        destination = destinationArray[randDestination];
        originalDestinationMaterial = destination.GetComponent<MeshRenderer>().material;
        destination.GetComponent<MeshRenderer>().material = highlightedDestination;
        destination.layer = LayerMask.NameToLayer("Destination");
    }

    public void EngagePizzaLauncher()
    {
        //pizzaList = new List<GameObject>();

        if (Input.GetKeyDown(KeyCode.Space)) // press space to shoot pizzas
        {
            
            pizzaLaunched.Play();
            Instantiate(pizza, shootPos.position, shootPos.rotation);

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Pizza").Length; i++)
            {
                GameObject newPizza = GameObject.FindGameObjectsWithTag("Pizza")[i];

                if (!pizzaList.Contains(newPizza))
                {
                    pizzaList.Add(newPizza);
                }
            }
        }
    }
    public void DeliveryComplete()
    {
        deliveryCounter++;
        temp = resetTemp;
        pizzaDelivered.Play();
        

        foreach (GameObject pizza in pizzaList)
        {
            Destroy(pizza);
        }

        pizzaList.Clear();

        if (deliveryCounter >= numOfDeliveries)
        {
            //put game over scene transition script here
        }
        
        destination.layer = LayerMask.NameToLayer("BuildingLayer");
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
