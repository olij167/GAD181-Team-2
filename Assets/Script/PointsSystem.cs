using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    public int points;
    private GameObject TriggeringObj;


    private void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fuel")
        {
            points += 50;
            TriggeringObj = other.gameObject;
            Destroy(TriggeringObj);
        }
    }
}
