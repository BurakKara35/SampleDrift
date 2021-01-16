using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    GameManager gameManager;

    [HideInInspector] public int obstacleCount;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        obstacleCount = transform.childCount;
    }

    public void DecreaseObstacleCountAndCheckForVictory()
    {
        obstacleCount--;

        if(obstacleCount == 0)
        {
            gameManager.StopGameVictory();
        }

    }
}
