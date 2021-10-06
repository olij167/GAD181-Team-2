using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    
    public Transform player;

    void LateUpdate()
    {
        Vector3 CameraFollowPostion = player.position;
        CameraFollowPostion.y = transform.position.y;
        transform.position = CameraFollowPostion;
    }
}
