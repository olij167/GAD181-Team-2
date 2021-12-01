using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToSpawn : MonoBehaviour
{
    public GameObject restaurant;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("R"))
        {
            gameObject.transform.position = restaurant.transform.position;
        }
    }
}
