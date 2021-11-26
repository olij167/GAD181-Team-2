using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Condition : MonoBehaviour
{
    [HideInInspector] public int condition, maxCondition = 100;
    public TextMeshProUGUI mytext;


    private void Start()
    {
        mytext.text = condition.ToString();
    }
private void OnCollisionEnter(Collision collision)
    {
        //Cars driving around
        if (collision.gameObject.CompareTag("HubWorldCar"))
        {
            condition = condition - 5;
        }
        //little guys walking around
        else if (collision.gameObject.CompareTag("Pedestrian"))
        {
            condition = condition - 1;
        }
        //Buildins what can be delivered to
        else if (collision.gameObject.CompareTag("Building"))
        {
            condition = condition - 5;
        }
    }
    private void Update()
    {
        mytext.text = condition.ToString();
    }
    private void Update()
    {
       
    }
}
