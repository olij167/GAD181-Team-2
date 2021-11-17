using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpinout : MonoBehaviour
{
    Rigidbody rb;
    public float spinSpeed = 500f, sleeptime, sleeptimeReset;
    public GameObject trafficPrefab;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;


            Vector3 smashForce = new Vector3(collision.relativeVelocity.x * spinSpeed, 0, collision.relativeVelocity.z * spinSpeed);
            rb.AddForce(smashForce * Time.deltaTime);
        }
    }
}
