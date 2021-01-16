using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGroundController : MonoBehaviour
{
    [SerializeField] private float waitingTimeInSecond;

    private void Start()
    {
        InvokeRepeating("NarrowPlayGround", 0, waitingTimeInSecond);
    }

    void NarrowPlayGround()
    {
        transform.localScale -= new Vector3(0.2f, 0, 0.2f);
    }
}
