using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriftLine : MonoBehaviour
{
    [SerializeField] private TrailRenderer[] trailRenderers;

    private void Awake()
    {
        StopEmitting();
    }

    public void StartEmitting()
    {
        for(int i=0; i<trailRenderers.Length; i++)
        {
            trailRenderers[i].emitting = true;
        }
    }

    public void StopEmitting()
    {
        for (int i = 0; i < trailRenderers.Length; i++)
        {
            trailRenderers[i].emitting = false;
        }
    }
}
