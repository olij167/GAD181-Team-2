using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPizzaCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pizza"))
        {
            Debug.Log(gameObject.layer);
            if (!gameObject.layer.Equals(9))
                Destroy(collision.gameObject);
        }
    }
}
