using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCommon : MonoBehaviour
{
    private bool powerUpFinished = false;
    private float powerUpTimeInSeconds = 5;

    private PowerUpSpawnManager powerUpSpawnManager;
    private SpawnManager spawnManager;

    [HideInInspector] public CarUIController carUIController;

    Coroutine powerCouroutine;

    private string powerUpName;

    private void Awake()
    {
        powerUpSpawnManager = GameObject.FindGameObjectWithTag("PowerUpSpawnManager").GetComponent<PowerUpSpawnManager>();
        carUIController = GameObject.FindGameObjectWithTag("CarUI").GetComponent<CarUIController>();
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManagers").GetComponent<SpawnManager>();
    }

    public void PowerUpTrigger(Collider other)
    {
        if (other.gameObject.CompareTag("CircleBallPower"))
        {
            powerUpSpawnManager.Discard(other.gameObject);

            if (!spawnManager.powerUpTaken)
            {
                spawnManager.powerUpTaken = true;
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
        spawnManager.powerUpTaken = false;
        powerUpFinished = true;
    }

    public bool PowerUpTaken
    {
        get { return spawnManager.powerUpTaken; }
        set { spawnManager.powerUpTaken = value; }
    }
}