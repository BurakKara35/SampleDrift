using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPowerUpTrail : MonoBehaviour
{
    TrailRenderer trailRenderer;

    private void Awake()
    {
        trailRenderer = transform.GetChild(0).GetComponent<TrailRenderer>();
        StopEmitting();
    }

    public void StartEmitting()
    {
        trailRenderer.emitting = true;
    }

    public void StopEmitting()
    {
        trailRenderer.emitting = false;
    }
}
