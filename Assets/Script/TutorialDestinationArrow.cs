using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDestinationArrow : MonoBehaviour
{
    public Transform target; //the delivery destination

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<RaceStarter>().nextCheckpoint.transform;

        transform.LookAt(target);
    }
}
