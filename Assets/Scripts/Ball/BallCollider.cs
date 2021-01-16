using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour
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
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            colliderCommon.carUIController.GiveGoodEmojiToPlayer();
        }
    }
}
