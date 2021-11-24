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
        if (collision.gameObject.CompareTag("HubWorldCar"))
        {
            maxCondition = maxCondition - 5;
        }
        else if (collision.gameObject.CompareTag("Pedestrian"))
        {
            maxCondition = maxCondition - 1;
        }
        else if (collision.gameObject.CompareTag("Building"))
        {
            maxCondition = maxCondition - 30;
        }
        mytext.text = maxCondition.ToString();

    }
    public void Update()
    {
        if (maxCondition <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
