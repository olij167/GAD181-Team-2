using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.ParticleSystemJobs;


public class SetRandomDestination : MonoBehaviour
{
    [Header("Colour")]
    [SerializeField] public Material carColour;

    [Header("Destination Indicator Variables")]
    [SerializeField] public GameObject[] destinationArray; // all potential destinations
    [SerializeField] public GameObject destination, arrow; // selected destination
    [SerializeField] public Material highlightedDestination;
    [SerializeField] Material  originalDestinationMaterial, originalArrowMaterial;

    [Header("Destination Finding and Delivery Complete Variables")]
    [SerializeField] public float deliveryRange, feedbackTimer, feedbackTimerReset, deliveryCounter; // look radius
    [SerializeField] public bool destinationSet, destinationInRange, playerFeedback, deliveryLimit; //check if destination is set, check if destination is in range, check if delivery complete, check if player requires feedback
    [SerializeField] public LayerMask destinationLayer;
    [SerializeField] public Vector3 distanceToDestination;

    public float numOfDeliveries = 1;


    [Header("Projectile Variables")]
    [SerializeField] public Transform shootPos;
    [SerializeField] public GameObject pizza;
    [SerializeField] public List<GameObject> pizzaList; // control pizzas
    [SerializeField] public float shotPower, stopDistance; // pizza speed, pizza delivered proximity
    [SerializeField] public bool launcherActive;
    [SerializeField] int destinationNum;

    [Header("Delivery Complete Placeholder UI")]
    [SerializeField] public TextMeshProUGUI deliveriesCompleteUI, deliveryNumUI, pizzaLauncherText, engagedText, distanceToDestinationText;
    [SerializeField] public Image pizzaEngagedBackground, pizzaEngagedBorder;
    [SerializeField] public Material goodFeedback;

    //public GameObject deliveryCompleteParticles;

    [Header("Temp Variables")]
    [SerializeField] public float temp, resetTemp;
    [SerializeField] public TextMeshProUGUI tempUIValue;
    [SerializeField] public Image tempBar;
    [HideInInspector] public Color hot, warm, cold;

    //condition ui variables


    [Header("Audio Variables")]
    [SerializeField] public AudioSource pizzaLaunched;
    [SerializeField] public AudioSource pizzaDelivered;
    [SerializeField] public AudioSource deliveryCheer;

    [Header("Gameover Chances")]
    [SerializeField] bool strike1, strike2, strike3;
    [SerializeField] public List<GameObject> strikeUIList;


    [Header("Points")]
    [SerializeField] public int points = 0;
    [SerializeField] private GameObject TriggeringObj;
    [SerializeField] public TextMeshProUGUI text; 



    void Start()
    {
        cold = new Color(0.2282118f, 0.2282118f, 0.6235294f, 1f);
        warm = highlightedDestination.color;
        hot = new Color(0.6235294f, 0.2282118f, 0.227451f, 1f);

        gameObject.GetComponent<MeshRenderer>().material.color = carColour.color;

        strikeUIList[0].SetActive(false);
        strikeUIList[1].SetActive(false);
        strikeUIList[2].SetActive(false);

        if (deliveryLimit)
        {
            deliveryNumUI.enabled = true;
        }
        else deliveryNumUI.enabled = false;

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
            if (pizza != null)
            {
                pizza.transform.position = Vector3.MoveTowards(pizza.transform.position, destination.transform.position, shotPower * Time.deltaTime); // move the pizza towards the position of the delivery destination

                Vector3 distanceToWalkPoint = pizza.transform.position - destination.transform.position; // the distance between the pizza and the destination

                if (distanceToWalkPoint.magnitude < stopDistance) // check if pizza has reached destination
                    DeliveryComplete();
            }
            else pizzaList.Remove(pizza);
        }

        deliveriesCompleteUI.text = deliveryCounter.ToString();

        if (deliveryLimit)
        {
            deliveryNumUI.text = "/ " + numOfDeliveries.ToString();
        }

        temp -= Time.deltaTime;
        //Below is if the timer runs out three times the Game is over
        if (temp <= 0 && !strike1)
        {
            strike1 = true;
            Debug.Log("strike 1");
            strikeUIList[0].SetActive(true);
            SetDestination();
            points = points - 25;
            text.text = points.ToString();
        }

        if (temp <= 0 && strike1 && !strike2)
        {
            strike2 = true;
            Debug.Log("strike 2");
            strikeUIList[1].SetActive(true);
            SetDestination();
            points = points - 25;
            text.text = points.ToString();
        }

        if (temp <= 0 && strike2 && !strike3)
        {
            strike3 = true;
            Debug.Log("strike 3");
            strikeUIList[2].SetActive(true);
            SetDestination();
            points = points - 25;
            text.text = points.ToString();
        }

        if (temp <= 0 && strike1 && strike2 && strike3)
        {
            SceneManager.LoadScene("LoseScreen");
        }

        tempUIValue.text = temp.ToString("F0");
        float fillAmount = temp / resetTemp;
        tempBar.fillAmount = fillAmount;
        if (temp > resetTemp/2)
            tempBar.color = Color.Lerp(warm, hot, fillAmount);
        else tempBar.color = Color.Lerp(cold, warm, fillAmount);



        if (playerFeedback)
        {
            ParticleSystem goodFeedbackParticles = destination.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();

            goodFeedbackParticles.Play();

            originalArrowMaterial = arrow.GetComponent<MeshRenderer>().material;
            feedbackTimer -= Time.deltaTime;
            destination.GetComponent<MeshRenderer>().material = goodFeedback;
            arrow.GetComponent<MeshRenderer>().material = goodFeedback;
            

            if (feedbackTimer <= 0)
            {
                
                destination.GetComponent<MeshRenderer>().material = originalDestinationMaterial;
                arrow.GetComponent<MeshRenderer>().material = originalArrowMaterial;

                destination.layer = LayerMask.NameToLayer("BuildingLayer");
                feedbackTimer = feedbackTimerReset;


                SetDestination();
                goodFeedbackParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                playerFeedback = false;
            }
        }
    }

    public void SetDestination() // choose random destination
    {
        int randDestination = Random.Range(0, destinationArray.Length); //generate a random number within the amount of buildings

        if (destination != null)
        {
            destination.GetComponent<MeshRenderer>().material = originalDestinationMaterial;
        }
        
        //for (int i = 0; i < destinationArray.Length; i++)
        //    destinationArray[i].layer = LayerMask.NameToLayer("BuildingLayer");
        temp = resetTemp;
        gameObject.GetComponent<Condition>().condition = gameObject.GetComponent<Condition>().maxCondition;
        gameObject.GetComponent<Condition>().conditionImage.fillAmount = 1f;
        gameObject.GetComponent<Condition>().mytext.text = gameObject.GetComponent<Condition>().condition.ToString();


        destination = destinationArray[randDestination];
        originalDestinationMaterial = destination.GetComponent<MeshRenderer>().material;
        destination.GetComponent<MeshRenderer>().material = highlightedDestination;
        
        destination.layer = LayerMask.NameToLayer("Destination");
    }

    public void EngagePizzaLauncher()
    {
        
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

        pizzaDelivered.Play();
        deliveryCheer.Play();

        foreach (GameObject pizza in pizzaList)
        {
            Destroy(pizza);
        }

        pizzaList.Clear();

        destination.GetComponent<MeshRenderer>().material = originalDestinationMaterial;
        playerFeedback = true;

        //Below is the code for finishing the game after a certain amount of deliverys
        if (deliveryLimit && deliveryCounter >= numOfDeliveries)
        {
            SceneManager.LoadScene("LoseScreen");    //-- this should be going to a win scene
        }
        points = points + 100;
        text.text = points.ToString();
        
    }
    void OnDrawGizmosSelected()
    {
        // look radius visualiser
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, deliveryRange);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("HubWorldCar"))
        {
            points = points - 10;
            text.text = points.ToString();
        }
        else if (other.gameObject.CompareTag("Pedestrian"))
        {
            points = points - 1;
            text.text = points.ToString();
        }
        else if (other.gameObject.CompareTag("Building"))
        {
            points = points - 10;
            text.text = points.ToString();
        }
        else if (other.gameObject.CompareTag("Extra"))
        {
            points = points - 5;
            text.text = points.ToString();
        }
        else if (other.gameObject.CompareTag("Developer"))
        {
            points = points + 50;
            text.text = points.ToString();
        }
    }

}

