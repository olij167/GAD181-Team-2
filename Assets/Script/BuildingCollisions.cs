using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollisions : MonoBehaviour
{

    public AudioSource wrongDelivery;

    public Material badFeedback;
    private Material originalMaterial;
    public float feedbackTimer, feedbackTimerReset;
    public bool playerFeedback;

    private void Start()
    {
        originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }
    private void Update()
    {
       if (playerFeedback)
        {
            gameObject.GetComponent<MeshRenderer>().material = badFeedback;

            feedbackTimer -= Time.deltaTime;

            if (feedbackTimer <= 0)
            {
                gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
                feedbackTimer = feedbackTimerReset;
                playerFeedback = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Pizza"))
        {
            if (!gameObject.layer.Equals(9))
            {
                playerFeedback = true;
                Destroy(collision.gameObject);
                wrongDelivery.Play();
            }
        }

        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    Rigidbody playerRB = collision.gameObject.GetComponent<Rigidbody>();
        //    Vector3 bounceDirection = collision.gameObject.GetComponent<SetRandomDestination>().distanceToDestination;

        //    playerRB.AddForce(bounceDirection);
        //    playerFeedback = true;
        //}
    }
}
