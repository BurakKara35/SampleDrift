using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private CarMovement movement;
    private CarDriftLine driftLine;
    private CarCollider carCollider;
    private ColliderCommon carColliderCommon;
    private ColliderCommon ballColliderCommon;

    [HideInInspector] public Animator animator;

    private PowerUpBehaviours powerUpBehaviours;
    private bool isNewPowerTaken = false;

    private bool swipe = false;
    private bool swipeFinished = true;
    private float swipeFirstPosition;
    private float differenceBetweenSwipePositions;
    private float swipingInSeconds = 0.1f;

    private enum CarDirection { None, Left, Right }
    private CarDirection carDirection;

    private Transform ball;
    private Transform ballPosition;

    [SerializeField] private GameObject dustParticle;
    private float driftDustTimeInSecond;
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
        animator = GetComponent<Animator>();

        carDirection = CarDirection.None;

        powerUpBehaviours = null;

        dustParticle.SetActive(false);
        driftDustTimeInSecond = dustParticle.GetComponent<ParticleSystem>().main.duration;

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
            dustParticle.SetActive(true);
        }
        else if (carDirection == CarDirection.Right)
        {
            movement.TurnRight();
            driftLine.StartEmitting();
            dustParticle.SetActive(true);
        }
        else
        {
            if(isDriftingStopped) // I check drifting status to call these operations once
            {
                driftLine.StopEmitting();
                StartCoroutine(DriftDust());
            }
        }
        #endregion

        #region PowerUps
        if((ballColliderCommon.PowerUpTaken || carColliderCommon.PowerUpTaken) && !isNewPowerTaken)
        {
            // Behaviours can be increased in future and if it is increase enum can be used instead of string
            if (ballColliderCommon.PowerUpName == "CircleBallPower" || carColliderCommon.PowerUpName == "CircleBallPower")
            {
                powerUpBehaviours = new CircleBallPowerUp(ball,this.transform,ballPosition);
            }
            isNewPowerTaken = true;
        }

        if (powerUpBehaviours != null)
            powerUpBehaviours.PowerUp();

        if ((ballColliderCommon.PowerUpFinished || carColliderCommon.PowerUpFinished))
        {
            ballColliderCommon.PowerUpFinished = false;
            carColliderCommon.PowerUpFinished = false;
            
            if(powerUpBehaviours != null)
                powerUpBehaviours.FinishBehaviour();
            
            powerUpBehaviours = null;
            isNewPowerTaken = false;
        }
        #endregion
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

    public IEnumerator DriftDust()
    {
        yield return new WaitForSeconds(driftDustTimeInSecond);
        dustParticle.SetActive(false);
    }
}
