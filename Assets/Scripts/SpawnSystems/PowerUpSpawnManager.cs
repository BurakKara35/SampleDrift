using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject gameObject;
        public int size;
    }

    [HideInInspector] public Transform objectsInHierarchy;
    Transform active;

    public List<Pool> poolList;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private SpawningPoints spawningPoints;

    private bool isAllObjectsSpawned;

    private void Awake()
    {
        objectsInHierarchy = transform.GetChild(0).transform;
        active = objectsInHierarchy.GetChild(0).transform;

        spawningPoints = GameObject.FindGameObjectWithTag("SpawnManagers").GetComponent<SpawningPoints>();

        CreatePool();
    }

    void CreatePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in poolList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.gameObject);
                obj.transform.parent = objectsInHierarchy;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject Spawn(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject obj = poolDictionary[tag].Dequeue();

        obj.SetActive(true);
        obj.transform.parent = active;
        obj.transform.position = spawningPoints.GetSpawnPoint(); ;

        poolDictionary[tag].Enqueue(obj);

        return obj;
    }

    public void Discard(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.parent = objectsInHierarchy;
    }

    public bool IsAllObjectsSpawned
    {
        get 
        { 
            if (objectsInHierarchy.childCount > 1)
                return false;
            else
                return true; 
        }
    }
}
