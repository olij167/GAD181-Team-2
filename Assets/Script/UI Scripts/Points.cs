using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public int points = 0;
    public AudioClip MySound;
    public Text text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CarCheckpoint"))
        {

            GetComponent<AudioSource>().PlayOneShot(MySound);

            other.gameObject.SetActive(false);

            ++points;
            text.text = points.ToString();
            
        }
    }

}
