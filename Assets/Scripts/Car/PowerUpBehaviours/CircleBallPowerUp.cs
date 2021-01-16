using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBallPowerUp : PowerUpBehaviours
{
    private bool isPositionsInitialized = false;
    private float angleConstant = 200f;

    Transform ball;
    Transform car;
    Transform ballPositionChieldInCar;

    public CircleBallPowerUp(Transform ball, Transform car, Transform ballPositionChieldInCar)
    {
        this.ball = ball;
        this.car = car;
        this.ballPositionChieldInCar = ballPositionChieldInCar;
    }

    private void Initialize()
    {
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<BallManager>().enabled = false;
        ball.GetComponent<BallPowerUpTrail>().StartEmitting();
    }

    public void PowerUp()
    {
        if (!isPositionsInitialized)
            Initialize();

        ball.transform.RotateAround(car.transform.position, Vector3.down, angleConstant * Time.fixedDeltaTime);

    }

    public void FinishBehaviour()
    {
        ball.GetComponent<Rigidbody>().useGravity = true;
        ball.GetComponent<BallManager>().enabled = true;
        ball.GetComponent<BallPowerUpTrail>().StopEmitting();
        ball.transform.position = ballPositionChieldInCar.transform.position; // To avoid crashing between car and ball
    }
}
