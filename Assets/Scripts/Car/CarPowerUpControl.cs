using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPowerUpControl : MonoBehaviour
{
    private PowerUpBehaviours powerUpBehaviours;
    private bool isPowerActive = false;

    private void Awake()
    {
        powerUpBehaviours = null;
    }

    public void CheckForPowerUp(ColliderCommon ballColliderCommon, ColliderCommon carColliderCommon, Transform ballPosition, Transform ball)
    {
        if ((ballColliderCommon.PowerUpTaken || carColliderCommon.PowerUpTaken) && !isPowerActive)
        {
            // Behaviours can be increased in future and if it is increase enum can be used instead of string
            if (ballColliderCommon.PowerUpName == "CircleBallPower" || carColliderCommon.PowerUpName == "CircleBallPower")
            {
                powerUpBehaviours = new CircleBallPowerUp(ball, this.transform, ballPosition);
                isPowerActive = true;
            }
        }

        if (isPowerActive)
        {
            powerUpBehaviours.PowerUp();

            if ((ballColliderCommon.PowerUpFinished || carColliderCommon.PowerUpFinished))
            {
                powerUpBehaviours.FinishBehaviour();
                powerUpBehaviours = null;

                ballColliderCommon.PowerUpFinished = false;
                carColliderCommon.PowerUpFinished = false;

                isPowerActive = false;

                ball.GetComponent<BallManager>().enabled = true;
            }
        }
    }
}
