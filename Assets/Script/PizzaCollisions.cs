using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PizzaCollisions : MonoBehaviour
{

    public Material destinationMat;


    private void Update()
    {
        Debug.Log("Pizza shot!");
        destinationMat = GameObject.FindGameObjectWithTag("Player").GetComponent<SetRandomDestination>().highlightedDestination;

        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>(), GetComponent<SphereCollider>());
    }

    private void OnTriggerEnter(Collider other)
    {
       
        Debug.Log(other.gameObject.GetComponent<MeshRenderer>().material);

        if (other.gameObject.GetComponent<MeshRenderer>().material != destinationMat)
        {
            Destroy(gameObject);
            
            Debug.Log("Target Missed");
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, other.transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<SetRandomDestination>().shotPower * Time.deltaTime);
        }
    }
}

