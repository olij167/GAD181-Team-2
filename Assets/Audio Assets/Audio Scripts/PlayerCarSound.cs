using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarSound : MonoBehaviour
{

    AudioSource audioSource;
    public float minPitch = 0.1f;
    public float maxPitch = 2f;
    private float pitchFromCar;



    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minPitch;
    }

    // Update is called once per frame
    void Update()
    {
        pitchFromCar = PlayerCar.cc.carCurrentSpeed;
        if(pitchFromCar < minPitch)
        {
            audioSource.pitch = minPitch;
        }
        else
        {
            audioSource.pitch = pitchFromCar;
        }

    }
}
