using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollisions : MonoBehaviour
{

    public AudioSource wrongDelivery;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pizza"))
        {
            Debug.Log(gameObject.layer);
            if (!gameObject.layer.Equals(9))
                Destroy(collision.gameObject);
            wrongDelivery.Play();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bounceDirection = collision.gameObject.GetComponent<SetRandomDestination>().distanceToDestination;

            playerRB.AddForce(bounceDirection);
        }
    }
}
