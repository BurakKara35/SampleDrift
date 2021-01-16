using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 differeceCameraAndTarget;

    private void Awake()
    {
        differeceCameraAndTarget = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = differeceCameraAndTarget + target.position;
    }
}
