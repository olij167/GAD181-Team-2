using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    //moving speed of the car
    //in unity this needs to be atleast 600 to move 
    //1200 is best
    //click to drive
    [SerializeField] float acceleration = 8;
    //this is to set the speed of which the car follows the mouse
    [SerializeField] float turnSpeed = 5; // 100 feels pretty good for this ~~ Oli

    Quaternion targetRotation;
    Rigidbody _rigidBody;

    Vector3 lastPosition;
    float _sideSlipAmount = 0;

    public float SideSlipAmount
    {
        get
        {
            return _sideSlipAmount;
        }
    }

    void Start()
    {
        //this is generating a rigid body right at the start
        _rigidBody = GetComponent<Rigidbody>();
    }

     void Update()
    {
        SetRotationPoint();
        SetSideSlip();
    }

    private void SetSideSlip()
    {
        Vector3 direction = transform.position - lastPosition;
        Vector3 movement = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;

        _sideSlipAmount = movement.x;
    }



    private void SetRotationPoint()
    {
        //this is to get it to follow the mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if(plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);

        }
    }
    private void FixedUpdate()
    {

        float speed = _rigidBody.velocity.magnitude / 1000;

        //this is the code for the click to drive
        //connected with acceletation
        float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        _rigidBody.AddRelativeForce(Vector3.forward * accelerationInput);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Mathf.Clamp(speed, -1, 1) * Time.deltaTime);
    }
}
