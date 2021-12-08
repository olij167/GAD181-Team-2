using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Condition : MonoBehaviour
{
    [HideInInspector] public float condition, maxCondition = 100;
    public TextMeshProUGUI mytext;

    public bool playerFeedback;
    public float feedbackTimer, feedbackTimerReset;
    public GameObject collisionParticles;

    public Image conditionImage;
    Color badConditionColour;

    private void Start()
    {

        badConditionColour = new Color(0.509804f, 1f, 0.5490196f, 1f);

        mytext.text = condition.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hi");
        //Cars driving around
        if (collision.gameObject.CompareTag("HubWorldCar"))
        {
            condition = condition - 5;
            conditionImage.fillAmount = conditionImage.fillAmount - 0.05f;
        }
        //little guys walking around
        else if (collision.gameObject.CompareTag("Pedestrian"))
        {
            condition = condition - 1;
            conditionImage.fillAmount = conditionImage.fillAmount - 0.01f;
        }
        //Buildins what can be delivered to
        else if (collision.gameObject.CompareTag("Building"))
        {
            condition = condition - 5;
            conditionImage.fillAmount = conditionImage.fillAmount - 0.05f;
            collisionParticles.SetActive(false);

            if (feedbackTimer != feedbackTimerReset)
            {
                feedbackTimer = 0;
                feedbackTimer = feedbackTimerReset;
            }
            playerFeedback = true;
        }
        else if (collision.gameObject.CompareTag("Extra"))
        {
            condition = condition - 5;
            conditionImage.fillAmount = conditionImage.fillAmount - 0.05f;
        }
            mytext.text = condition.ToString();
    }
    private void Update()
    {
        if (condition <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }


        if (playerFeedback)
        {
            collisionParticles.SetActive(true);
            conditionImage.color = Color.Lerp(badConditionColour, Color.white, Mathf.Clamp(condition / maxCondition, 0, 1f));
            feedbackTimer -= Time.deltaTime;
            

            if (feedbackTimer <= 0)
            {
                collisionParticles.SetActive(false);
                feedbackTimer = feedbackTimerReset;

                playerFeedback = false;
            }
        }
    }
}
