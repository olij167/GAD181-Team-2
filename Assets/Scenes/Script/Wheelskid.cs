using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheelskid : MonoBehaviour
{
    [SerializeField] float intensityModifier = 1.5f;

    Skidmarks skidMarkcontroller;
    PlayerCar playerCar;

    int LastSkidId = -1;

    // Start is called before the first frame update
    void Start()
    {
        skidMarkcontroller = FindObjectOfType<Skidmarks>();
        playerCar = GetComponentInParent<PlayerCar>();
    }

    // Update is called once per frame
    void lateupdate()
    {

        float intensity = playerCar.SideSlipAmount;
        if (intensity < 0)
            intensity = -intensity;

        if (intensity > 0.2f)
        {
            //show skids
            LastSkidId = skidMarkcontroller.AddSkidMark(transform.position, transform.up, intensity * intensityModifier, LastSkidId);
        }
        else
        {
            //stop skids
            LastSkidId = -1;
        }
    }

}
