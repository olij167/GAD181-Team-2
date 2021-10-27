using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
    private int condition = 100;

    private void Start()
    {
        
        //mytext.text = Condition.Tostring();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BadCar"))
        {
            condition = condition -20 ;
        }
        if (collision.gameObject.CompareTag("Pedestrian"))
        {
            condition = condition -10;
        }
        if (collision.gameObject.CompareTag("HubWorldCar"))
        {
            condition = condition -20;
        }
        //myText.text = condition.ToString();
    }
    private void Update()
    {
        if (condition == 90||condition == 100);
        {
            Console.WriteLine("Hot");
        }
        else if (condition == 80|| condition == 70);
        {
            myText.text = .ToString("Just right");
        }
        else
        {
            myText.text = condition.ToString();
        }
    }
}
