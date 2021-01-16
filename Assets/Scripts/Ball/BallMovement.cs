using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody rigidBody;

    public Transform ballPosition;

    [SerializeField] private float damping;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void FollowCar()
    {
        Vector3 position = Vector3.Lerp(transform.position, ballPosition.position, Time.fixedDeltaTime * damping);
        rigidBody.MovePosition(position);
    }
}
