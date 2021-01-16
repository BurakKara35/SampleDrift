using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCommon : MonoBehaviour
{
    private bool powerUpTaken = false;
    private bool powerUpFinished = false;
    private float powerUpTimeInSeconds = 5;

    PowerUpSpawnManager powerUpSpawnManager;

    [HideInInspector] public CarUIController carUIController;

    Coroutine powerCouroutine;

    private string powerUpName;

    private void Awake()
    {
        powerUpSpawnManager = GameObject.FindGameObjectWithTag("PowerUpSpawnManager").GetComponent<PowerUpSpawnManager>();
        carUIController = GameObject.FindGameObjectWithTag("CarUI").GetComponent<CarUIController>();
    }

    public void PowerUpTrigger(Collider other)
    {
        if (other.gameObject.CompareTag("CircleBallPower"))
        {
            powerUpSpawnManager.Discard(other.gameObject);

            if (!powerUpTaken)
            {
                powerUpTaken = true;
                powerUpName = "CircleBallPower";
                powerCouroutine = StartCoroutine(PoweringUp());
            }
            else
            {
                StopCoroutine(powerCouroutine);
                powerCouroutine = StartCoroutine(PoweringUp());
            }
        }
    }

    public bool PowerUpTaken
    {
        get { return powerUpTaken; }
        set { powerUpTaken = value; }
    }
    
    public bool PowerUpFinished
    {
        get { return powerUpFinished; }
        set { powerUpFinished = value; }
    }

    public string PowerUpName
    {
        get { return powerUpName; }
        set { powerUpName = value; }
    }

    public IEnumerator PoweringUp()
    {
        yield return new WaitForSeconds(powerUpTimeInSeconds);
        powerUpTaken = false;
        powerUpFinished = true;
    }
}
