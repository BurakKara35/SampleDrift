using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            GameObject.FindGameObjectWithTag("ObstacleManager").GetComponent<ObstacleManager>().DecreaseObstacleCountAndCheckForVictory();
            Destroy(gameObject);
        }
    }
}
