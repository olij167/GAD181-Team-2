using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionSound : MonoBehaviour
{
    public AudioClip impact;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Grass"))
        {
            audioSource.PlayOneShot(impact, 0.7F);
        }
        //audioSource.PlayOneShot(impact, 0.7F);
   
    }

}
