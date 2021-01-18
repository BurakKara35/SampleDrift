using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject AICar;
    [SerializeField] private int aiCarCount;

    [SerializeField] private GameObject AICarBall;
    [SerializeField] private GameObject AICarCanvas;
    [SerializeField] private GameObject AILine;

    private SpawningPoints spawningPoints;
    private GameManager gameManager;


    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spawningPoints = GameObject.FindGameObjectWithTag("SpawnManagers").GetComponent<SpawningPoints>();
        SpawnCars();
    }

    private void SpawnCars()
    {
        for(int i=0; i < aiCarCount; i++)
        {
            GameObject aiCarParent = new GameObject("AICar"+i);
            aiCarParent.transform.parent = transform;

            GameObject car = Instantiate(AICar, spawningPoints.GetSpawnPoint(), Quaternion.identity);
            car.transform.parent = aiCarParent.transform;

            GameObject ball = Instantiate(AICarBall);
            car.GetComponent<CarAIController>().Ball = ball.transform;
            ball.GetComponent<BallMovement>().ballPosition = car.transform.Find("BallPosition");
            ball.transform.position = ball.GetComponent<BallMovement>().ballPosition.position;
            ball.GetComponent<BallCollider>().IsThisAIBall = true;
            ball.transform.parent = aiCarParent.transform;

            // positioning canvas does not work in editor, works in build
            GameObject canvas = Instantiate(AICarCanvas, new Vector3(car.transform.position.x, car.transform.position.y + 2, car.transform.position.z), Quaternion.Euler(45, -90, 0));
            canvas.GetComponent<CarUIController>().isThisCanvasAI = true;
            FollowTarget followTarget = canvas.GetComponent<FollowTarget>();
            followTarget.Target = car.transform;
            followTarget.enabled = true;
            canvas.transform.SetParent(aiCarParent.transform);

            GameObject line = Instantiate(AILine, car.transform.position, Quaternion.identity);
            LineController lineController = line.GetComponent<LineController>();
            lineController.Car = car.transform;
            lineController.Ball = ball.transform;
            line.transform.parent = aiCarParent.transform;

            car.GetComponent<CarAIController>().AICarNumber = i;
        }
    }

    public void DecreaseCarCount()
    {
        aiCarCount--;

        if (aiCarCount == 0)
        {
            gameManager.StopGameVictory();
        }

    }
}
