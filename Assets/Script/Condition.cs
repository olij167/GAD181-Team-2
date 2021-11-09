using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    private int maxCondition = 100;
    public Text mytext;

    private void Start()
    {
        mytext.text = maxCondition.ToString();
    }
private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HubWorldCar"))
        {
            maxCondition = maxCondition - 5;
        }
        else if (collision.gameObject.CompareTag("Pedestrian"))
        {
            maxCondition = maxCondition - 1;
        }
        else if (collision.gameObject.CompareTag("Building"))
        {
            maxCondition = maxCondition - 5;
        }
        mytext.text = maxCondition.ToString();
    }
}
