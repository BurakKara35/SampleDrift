using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    PowerUpSpawnManager powerUpSpawnManager;
    [SerializeField] private float PowerUpSpawningTimeInSeconds;
    private bool powerUpSpawned = false;

    ColliderCommon colliderCommonBall;
    ColliderCommon colliderCommonCar;

    private void Awake()
    {
        powerUpSpawnManager = GameObject.FindGameObjectWithTag("PowerUpSpawnManager").GetComponent<PowerUpSpawnManager>();

        colliderCommonBall = GameObject.FindGameObjectWithTag("Ball").GetComponent<ColliderCommon>();
        colliderCommonCar = GameObject.FindGameObjectWithTag("Car").GetComponent<ColliderCommon>();
    }

    void Update()
    {
        if (!powerUpSpawned)
        {
            powerUpSpawnManager.Spawn("CircleBall"); // If there is another power ups, we can choose them randomly in a list.
            powerUpSpawned = true;

            if(!powerUpSpawnManager.IsAllObjectsSpawned) // If the size of object pool greater than 1, spawn the others as well
                StartCoroutine(PowerUpSpawning(PowerUpSpawningTimeInSeconds));
        }

        if(colliderCommonBall.PowerUpTaken || colliderCommonCar.PowerUpTaken)
        {
            StartCoroutine(PowerUpSpawning(PowerUpSpawningTimeInSeconds));
            colliderCommonBall.PowerUpTaken = false;
            colliderCommonCar.PowerUpTaken = false;
        }
    }

    public IEnumerator PowerUpSpawning(float time)
    {
        yield return new WaitForSeconds(time);
        powerUpSpawned = false;
    }
}
