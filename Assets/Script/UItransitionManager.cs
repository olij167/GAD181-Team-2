using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UItransitionManager : MonoBehaviour
{

    public CinemachineVirtualCamera currentCamera;



    public void Start()
    {
        currentCamera.Priority++;
    }

    public void Updatecamera(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
    }



}
