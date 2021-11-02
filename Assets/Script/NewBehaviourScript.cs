using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform shootpos;
    public GameObject sphere;
    public float shotpower;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(sphere, shootpos.position, shootpos.rotation).GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * shotpower);
        }
    }
}
