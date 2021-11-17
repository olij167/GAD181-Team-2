using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PizzaCollisions : MonoBehaviour
{
    public GameObject player;
    public Material destinationMat;
    


    private void Awake()
    {
        Debug.Log("Pizza shot!");
        destinationMat = player.GetComponent<SetRandomDestination>().highlightedDestination;
    }

    private void OnCollisionEnter(Collision other)
    {
       Physics.IgnoreCollision(player.GetComponent<BoxCollider>(), GetComponent<SphereCollider>(), true);

        Debug.Log(other.gameObject.GetComponent<MeshRenderer>().material);

        if (other.gameObject.GetComponent<MeshRenderer>().material == destinationMat)
        {
            Debug.Log("Delivery Complete");
        }
        
    }
}

