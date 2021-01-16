using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private Rigidbody rigidbody;

    [SerializeField] private float carSpeed;
    [SerializeField] private float turnSpeed;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Move()
    {
        rigidbody.velocity = transform.forward * Time.fixedDeltaTime * carSpeed;
    }

    public void TurnLeft()
    {
        transform.RotateAround(transform.position, transform.up, Time.fixedDeltaTime * -turnSpeed);
    }

    public void TurnRight()
    {
        transform.RotateAround(transform.position, transform.up, Time.fixedDeltaTime * turnSpeed);
    }

    public void Stop()
    {
        rigidbody.velocity = Vector3.zero;
    }
}
