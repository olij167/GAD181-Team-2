using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
