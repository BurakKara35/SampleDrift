using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 differeceThisAndTarget;

    public Transform Target
    {
        set { target = value; }
    }

    private void Start()
    {
        differeceThisAndTarget = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = differeceThisAndTarget + target.position;
    }
}
