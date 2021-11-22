using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDestinationArrow : MonoBehaviour
{
    public GameObject destinationArrow, player;

    void Update()
    {
        Vector3 targetPos = destinationArrow.GetComponent<DestinationArrow>().target.transform.position;
        Vector3 playerPos = player.transform.position;
        transform.LookAt(playerPos - targetPos);
        transform.rotation = destinationArrow.transform.rotation;
    }
}
