using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpinout : MonoBehaviour
{
    Rigidbody rb;
    public float spinSpeed = 500f, sleeptime, sleeptimeReset;
    public GameObject trafficPrefab;
    public bool playerFeedback;
    public Material badFeedback;
    Material originalMaterial;
    public float feedbackTimer = 3, feedbackTimerReset = 3;

    public GameObject collisionParticles;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void LateUpdate()
    {

        if (gameObject.GetComponent<NavMeshAgent>().enabled == false)
            sleeptime -= Time.deltaTime;

        if (sleeptime <= 0)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            sleeptime = sleeptimeReset;
        }

        if (playerFeedback)
        {
            gameObject.GetComponent<MeshRenderer>().material = badFeedback;
            collisionParticles.SetActive(true);
            feedbackTimer -= Time.deltaTime;

            if (feedbackTimer <= 0)
            {
                collisionParticles.SetActive(false);
                gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
                feedbackTimer = feedbackTimerReset;
                playerFeedback = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;


            Vector3 smashForce = new Vector3(collision.relativeVelocity.x * spinSpeed, 0, collision.relativeVelocity.z * spinSpeed);
            rb.AddForce(smashForce * Time.deltaTime);

            
            if (collision.gameObject)
                playerFeedback = true;

            if (collision.gameObject.CompareTag("Pizza"))
            {
                Destroy(collision.gameObject);
            }
            
        }
    }
}
