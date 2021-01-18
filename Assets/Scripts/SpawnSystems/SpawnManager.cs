using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    PowerUpSpawnManager powerUpSpawnManager;
    [SerializeField] private float PowerUpSpawningTimeInSeconds;
    private bool powerUpSpawned = false;

    [HideInInspector] public bool powerUpTaken;

    ColliderCommon colliderCommonBall;
    ColliderCommon colliderCommonCar;

    private void Awake()
    {
        powerUpSpawnManager = GameObject.FindGameObjectWithTag("PowerUpSpawnManager").GetComponent<PowerUpSpawnManager>();

        colliderCommonBall = GameObject.FindGameObjectWithTag("Ball").GetComponent<ColliderCommon>();
        colliderCommonCar = GameObject.FindGameObjectWithTag("Car").GetComponent<ColliderCommon>();

        powerUpTaken = false;
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

        if (powerUpTaken)
        {
            StartCoroutine(PowerUpSpawning(PowerUpSpawningTimeInSeconds));
            powerUpTaken = false;
        }
    }

    public IEnumerator PowerUpSpawning(float time)
    {
        yield return new WaitForSeconds(time);
        powerUpSpawned = false;
    }
}
