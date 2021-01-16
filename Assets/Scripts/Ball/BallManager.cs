using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    BallMovement movement;

    [SerializeField] private Texture2D[] textures;
    MeshRenderer renderer;

    private void Awake()
    {
        movement = GetComponent<BallMovement>();
        renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        renderer.material.SetTexture("_MainTex", textures[Random.Range(0, textures.Length)]);
    }

    private void FixedUpdate()
    {
        movement.FollowCar();
    }
}
