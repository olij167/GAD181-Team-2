using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianFeedback : MonoBehaviour
{
    public Material badFeedback;
    Material originalHeadMaterial;
    Color originalColour;
    GameObject child;
    public bool playerFeedback;
    public float feedbackTimer, feedbackTimerReset;
    public GameObject collisionParticles;

    void Start()
    {
        child = gameObject.transform.GetChild(0).gameObject;
        originalHeadMaterial = child.GetComponent<MeshRenderer>().material;
        originalColour = gameObject.GetComponent<MeshRenderer>().material.color;
    }

    void Update()
    {
        if (playerFeedback)
        {
            child.GetComponent<MeshRenderer>().material = badFeedback;
            gameObject.GetComponent<MeshRenderer>().material.color = badFeedback.color;

            collisionParticles.SetActive(true);
            feedbackTimer -= Time.deltaTime;

            if (feedbackTimer <= 0)
            {
                child.GetComponent<MeshRenderer>().material = originalHeadMaterial;
                gameObject.GetComponent<MeshRenderer>().material.color = originalColour;

                collisionParticles.SetActive(false);
                feedbackTimer = feedbackTimerReset;

                playerFeedback = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerFeedback = true;
        }

        if (collision.gameObject.tag.Equals("Pizza"))
        {
            playerFeedback = true;
            Destroy(collision.gameObject);
        }
    }
}
