using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAICollider : MonoBehaviour
{
    ColliderCommon colliderCommon;

    private void Awake()
    {
        colliderCommon = GetComponent<ColliderCommon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        colliderCommon.PowerUpTrigger(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            colliderCommon.carUIController.GiveFeedbackToPlayer();
        }

        if (collision.gameObject.CompareTag("DeadZone"))
        {
            GameObject.FindGameObjectWithTag("AICarSpawner").GetComponent<AICarSpawner>().DecreaseCarCount();
            Destroy(transform.parent.gameObject);
        }
    }
}
