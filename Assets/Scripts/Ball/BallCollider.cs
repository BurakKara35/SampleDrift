using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour
{
    ColliderCommon colliderCommon;

    private string enemyTag;

    private bool isThisAIBall;

    private void Awake()
    {
        colliderCommon = GetComponent<ColliderCommon>();

        if (isThisAIBall)
            enemyTag = "Car";
        else
            enemyTag = "AICar";
    }

    public bool IsThisAIBall
    {
        set { isThisAIBall = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        colliderCommon.PowerUpTrigger(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(enemyTag))
        {
            colliderCommon.carUIController.GiveGoodEmojiToPlayer();
        }
    }
}
