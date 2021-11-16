using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeYAxisPosition : MonoBehaviour
{
    Vector3 grounded;

    private void Start()
    {
        grounded = new Vector3(gameObject.transform.position.x, -0.02f, gameObject.transform.position.z);
    }
    private void Update()
    {
        if (gameObject.transform.position.y > 0f || gameObject.transform.position.y < -0.1f)
        {
            gameObject.transform.position = grounded;
        }
    }
}
