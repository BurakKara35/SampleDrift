using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPoints : MonoBehaviour
{
    public Transform SpawnPointX;
    public Transform SpawnPointZ;

    public Vector3 GetSpawnPoint()
    {
        return new Vector3(Random.Range(-SpawnPointX.position.x, SpawnPointX.position.x), 0, (Random.Range(-SpawnPointZ.position.z, SpawnPointZ.position.z)));
    }
}
