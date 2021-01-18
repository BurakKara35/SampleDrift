using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private CarMovement movement;
    private CarDriftLine driftLine;
    private ColliderCommon carColliderCommon;
    private ColliderCommon ballColliderCommon;
    private CarDriftDust carDriftDust;
    private CarPowerUpControl carPowerUpControl;

    [HideInInspector] public Animator animator;

    private bool swipe = false;
    private bool swipeFinished = true;
    private float swipeFirstPosition;
    private float differenceBetweenSwipePositions;
    private float swipingInSeconds = 0.1f;

    private enum CarDirection { None, Left, Right }
    private CarDirection carDirection;

    private Transform ball;
    private Transform ballPosition;

    private bool isDriftingStopped = false;

    [HideInInspector] public bool isAlive;

    private void Awake()
    {
        movement = GetComponent<CarMovement>();
        driftLine = GetComponent<CarDriftLine>();
        carColliderCommon = GetComponent<ColliderCommon>();
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
        ballPosition = GameObject.FindGameObjectWithTag("BallPosition").transform;
        ballColliderCommon = ball.GetComponent<ColliderCommon>();
        carDriftDust = GetComponent<CarDriftDust>();
        animator = GetComponent<Animator>();
        carPowerUpControl = GetComponent<CarPowerUpControl>();

        carDirection = CarDirection.None;

        isAlive = true;
    }

    private void Update()
    {
        #region Inputs
        if (Input.GetMouseButtonDown(0))
        {
            swipe = true;
            swipeFirstPosition = Input.mousePosition.x;
            StartCoroutine(Swiping());
        }
        if (Input.GetMouseButton(0) && swipe)
        {
            swipeFinished = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            swipe = false;
            swipeFinished = true;
            carDirection = CarDirection.None;
            StopCoroutine(Swiping());
            isDriftingStopped = true;
        }
        #endregion

        #region Rotations
        if (carDirection == CarDirection.Left)
        {
            movement.TurnLeft();
            driftLine.StartEmitting();
            carDriftDust.OpenDust();
        }
        else if (carDirection == CarDirection.Right)
        {
            movement.TurnRight();
            driftLine.StartEmitting();
            carDriftDust.OpenDust();
        }
        else
        {
            if(isDriftingStopped) // I check drifting status to call these operations once
            {
                driftLine.StopEmitting();
                StartCoroutine(carDriftDust.DriftDust());
                isDriftingStopped = false;
            }
        }
        #endregion

        carPowerUpControl.CheckForPowerUp(ballColliderCommon,carColliderCommon,ballPosition,ball);
    }

    private void FixedUpdate()
    {
        if (isAlive)
            movement.Move();
        else
            movement.Stop();
    }

    public IEnumerator Swiping()
    {
        yield return new WaitForSeconds(swipingInSeconds);
        if (swipe)
        {
            differenceBetweenSwipePositions = Input.mousePosition.x - swipeFirstPosition;

            if (differenceBetweenSwipePositions < 0)
                carDirection = CarDirection.Left;
            else if (differenceBetweenSwipePositions > 0)
                carDirection = CarDirection.Right;

            if (!swipeFinished)
            {
                StartCoroutine(Swiping());
            }
        }
    }
}
