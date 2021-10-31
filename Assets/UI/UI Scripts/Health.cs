using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int health = 10;
    public Text winorlose;
    public Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText.text = health.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BadCar"))
        {
            health--;
        }


        if (collision.gameObject.CompareTag("Fuel"))
        {
            health +=5;
        }

        myText.text = health.ToString();

    }


}