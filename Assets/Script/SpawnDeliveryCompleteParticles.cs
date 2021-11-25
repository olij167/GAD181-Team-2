using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDeliveryCompleteParticles : MonoBehaviour
{
    public Material highlightMaterial;
    GameObject child;
    void Start()
    {
        child = transform.GetChild(0).gameObject;
        Debug.Log(child);
        child.GetComponent<ParticleSystem>().Pause();

        //child.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer.Equals("Destination") && gameObject.GetComponent<MeshRenderer>().material != highlightMaterial)
        {
           //child.SetActive(true);
           child.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            //child.SetActive(false);
            child.GetComponent<ParticleSystem>().Pause();

        }
    }
}
