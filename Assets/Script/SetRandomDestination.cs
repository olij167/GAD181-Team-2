using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.ParticleSystemJobs;


public class SetRandomDestination : MonoBehaviour
{
    // destination indicator variables
    public GameObject[] destinationArray; // all potential destinations
    public GameObject destination, arrow; // selected destination
    public Material highlightedDestination;
    Material  originalDestinationMaterial, originalArrowMaterial;

    //destination finding and delivery complete variables
    public float deliveryRange, feedbackTimer, feedbackTimerReset, deliveryCounter, numOfDeliveries; // look radius
    public bool destinationSet, destinationInRange, playerFeedback, deliveryLimit; //check if destination is set, check if destination is in range, check if delivery complete, check if player requires feedback
    public LayerMask destinationLayer;
    public Vector3 distanceToDestination;


    // projectile variables
    public Transform shootPos;
    public GameObject pizza;
    public List<GameObject> pizzaList; // control pizzas
    public float shotPower, stopDistance; // pizza speed, pizza delivered proximity
    public bool launcherActive;

    int destinationNum;

    // delivery complete placeholder UI
    public TextMeshProUGUI deliveriesCompleteUI, deliveryNumUI, pizzaLauncherText, engagedText, distanceToDestinationText;
    public Image pizzaEngagedBackground, pizzaEngagedBorder;
    public Material goodFeedback;
    
    //public GameObject deliveryCompleteParticles;

    //temp variables
    public float temp, resetTemp;
    public TextMeshProUGUI tempUIValue;
    public Image tempBar;
    [HideInInspector] public Color hot, warm, cold;

    //condition ui variables


    // audio variables
    public AudioSource pizzaLaunched;
    public AudioSource pizzaDelivered;
    public AudioSource deliveryCheer;

    //gameover chances
    bool strike1, strike2, strike3;
    public List<GameObject> strikeUIList;
  

    void Start()
    {
        cold = new Color(0.2282118f, 0.2282118f, 0.6235294f, 1f);
        warm = highlightedDestination.color;
        hot = new Color(0.6235294f, 0.2282118f, 0.227451f, 1f);

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
        }

        if (temp <= 0 && strike1 && !strike2)
        {
            strike2 = true;
            Debug.Log("strike 2");
            strikeUIList[1].SetActive(true);
            SetDestination();
        }

        if (temp <= 0 && strike2 && !strike3)
        {
            strike3 = true;
            Debug.Log("strike 3");
            strikeUIList[2].SetActive(true);
            SetDestination();
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
        
    }
    void OnDrawGizmosSelected()
    {
        // look radius visualiser
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, deliveryRange);
    }
    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Fuel")
        //{
        //    points = points + 50;
        //    TriggeringObj = other.gameObject;
        //    Destroy(TriggeringObj);
        //}
    }
}
