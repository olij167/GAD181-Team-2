using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationArrow : MonoBehaviour
{
    public Transform target; //the delivery destination

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<SetRandomDestination>().destination.transform;


        transform.LookAt(target);
    }
}
