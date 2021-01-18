using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIController : MonoBehaviour
{
    [SerializeField] private GameObject[] allCarsInScene;
    [SerializeField] private GameObject closestCar;

    private CarMovement movement;
    private CarDriftLine driftLine;
    private CarDriftDust carDriftDust;
    private ColliderCommon carColliderCommon;
    private ColliderCommon ballColliderCommon;
    private CarPowerUpControl carPowerUpControl;

    private enum AIStates { None, ClosestCarFound, GetReadyToDrift, Drift}
    private AIStates aIState;

    private enum CarDirection { None, Left, Right }
    private CarDirection carDirection;

    private int aiCarNumber;

    private float minimumDistance;
    private float driftingDistance = 2f;
    private float driftingTimeInSecondsMinimum = 0.3f;
    private float driftingTimeInSecondsMaximum = 1f;

    private bool isDriftingStopped = false;

    [SerializeField] private Transform ball;
    private Transform ballPosition;

    private void Start()
    {
        movement = GetComponent<CarMovement>();
        driftLine = GetComponent<CarDriftLine>();
        carDriftDust = GetComponent<CarDriftDust>();
        carColliderCommon = GetComponent<ColliderCommon>();
        ballColliderCommon = ball.GetComponent<ColliderCommon>();
        carPowerUpControl = GetComponent<CarPowerUpControl>();

        InitializeTempDistance();

        aIState = AIStates.None;

        FindAllCarsExceptThis();
    }

    private void Update()
    {
        #region AI
        if (aIState == AIStates.None)
            FindClosestCar();

        if (aIState == AIStates.ClosestCarFound)
        {
            movement.TurnToObject(closestCar.transform);
            CheckDriftingDistance(closestCar.transform);
        }

        if (aIState == AIStates.GetReadyToDrift)
            IdentifyDriftTimeAndRotation();

        if(aIState == AIStates.Drift)
        {
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
        }

        if (isDriftingStopped)
        {
            driftLine.StopEmitting();
            StartCoroutine(carDriftDust.DriftDust());
            isDriftingStopped = false;
        }
        #endregion

        carPowerUpControl.CheckForPowerUp(ballColliderCommon, carColliderCommon, ballPosition, ball);
    }

    private void FixedUpdate()
    {
        movement.Move();
    }

    public int AICarNumber
    {
        get { return aiCarNumber; }
        set { aiCarNumber = value; }
    }

    private void FindAllCarsExceptThis()
    {
        allCarsInScene = GameObject.FindGameObjectsWithTag("AICar");

        for(int i=0; i<allCarsInScene.Length; i++)
        {
            if (allCarsInScene[i].GetComponent<CarAIController>().AICarNumber == aiCarNumber)
            {
                allCarsInScene[i] = InputControlledCar();
            }
        }
    }

    private GameObject InputControlledCar()
    {
        return GameObject.FindGameObjectWithTag("Car");
    }

    private void FindClosestCar()
    {
        for(int i = 0; i < allCarsInScene.Length; i++)
        {
            float distance = Vector3.Distance(allCarsInScene[i].transform.position, transform.position);
            if(distance <= minimumDistance)
            {
                closestCar = allCarsInScene[i].gameObject;
                minimumDistance = distance;
            }
        }
        aIState = AIStates.ClosestCarFound;
        InitializeTempDistance();
    }

    private void IdentifyDriftTimeAndRotation()
    {
        int direction = Random.Range(0, 2);
        if (direction == 0)
            carDirection = CarDirection.Right;
        else
            carDirection = CarDirection.Left;

        StartCoroutine(Drifting(RandomDriftingTime()));

        aIState = AIStates.Drift;
    }

    private float RandomDriftingTime()
    {
        return Random.Range(driftingTimeInSecondsMinimum, driftingTimeInSecondsMaximum);
    }

    public IEnumerator Drifting(float time)
    {
        yield return new WaitForSeconds(time);
        isDriftingStopped = true;
        aIState = AIStates.None;
    }

    private void CheckDriftingDistance(Transform closestCar)
    {
        float distance = Vector3.Distance(closestCar.position, transform.position);
        if (distance <= driftingDistance)
        {
            aIState = AIStates.GetReadyToDrift;
        }
    }

    private void InitializeTempDistance()
    {
        minimumDistance = Mathf.Infinity;
    }

    public Transform Ball
    {
        set { ball = value; }
    }
}
