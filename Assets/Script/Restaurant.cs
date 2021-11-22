using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<SetRandomDestination>().temp = other.gameObject.GetComponent<SetRandomDestination>().resetTemp;
        }
    }
}
