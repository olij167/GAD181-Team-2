using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Condition : MonoBehaviour
{
    private int maxCondition = 100;
    public TextMeshProUGUI mytext;

    private void Start()
    {
        mytext.text = maxCondition.ToString();
    }
private void OnCollisionEnter(Collision collision)
    {
        //Cars driving around
        if (collision.gameObject.CompareTag("HubWorldCar"))
        {
            maxCondition = maxCondition - 5;
        }
        //little guys walking around
        else if (collision.gameObject.CompareTag("Pedestrian"))
        {
            maxCondition = maxCondition - 1;
        }
        //Buildins what can be delivered to
        else if (collision.gameObject.CompareTag("Building"))
        {
            maxCondition = maxCondition - 5;
        }
        //randoms buildings/environment around town
        else if (collision.gameObject.CompareTag("Decoration"))
        {
            maxCondition = maxCondition - 5;
        }
        mytext.text = maxCondition.ToString();
        //below is the code to load the game over screen
        if (maxCondition < 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
