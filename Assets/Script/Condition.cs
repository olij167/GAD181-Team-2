using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        if (collision.gameObject.CompareTag("HubWorldCar"))
        {
            condition = condition - 5;
        }
        else if (collision.gameObject.CompareTag("Pedestrian"))
        {
            condition = condition - 1;
        }
        else if (collision.gameObject.CompareTag("Building"))
        {
            condition = condition - 5;
        }
        mytext.text = condition.ToString();
    }
}
