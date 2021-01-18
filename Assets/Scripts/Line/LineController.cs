using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private Transform car;
    [SerializeField] private Transform ball;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, car.position);
        lineRenderer.SetPosition(1, ball.position);
    }

    public Transform Car
    {
        set { car = value; }
    }

    public Transform Ball
    {
        set { ball = value; }
    }
}
