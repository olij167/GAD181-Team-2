using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionSound : MonoBehaviour
{
    public AudioSource collision;

    void Start()
    {
      
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Grass"))
        {
            collision.Play();
        }
        
   
    }

}
