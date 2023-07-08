using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawn : MonoBehaviour
{
    [Header("Spawning Values")]
    [SerializeField] public SpawningPoint[] spawnSpots;

    public GameObject ChickenPrefab;

    public float minSpawnTime = 3f;
    public float maxSpawnTime = 6f;

    void Start()
    {
        SpawnChicken(spawnSpots[Random.Range(1, spawnSpots.Length)]);
        StartSpawn();
    }

    void StartSpawn()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        IEnumerator coroutine = WaitAndSpawn(spawnTime);
        StartCoroutine(coroutine);
    }

    IEnumerator WaitAndSpawn(float moveTime)
    {
        yield return new WaitForSeconds(moveTime);
        int selected = BasedRandom();
        SpawnChicken(spawnSpots[selected]);

        // Restart timer
        StartSpawn();
    }

    void SpawnChicken(SpawningPoint point)
    {
        Instantiate(ChickenPrefab, point.position, Quaternion.identity);
    }

    int BasedRandom()
    {
        return Random.Range(1, spawnSpots.Length);
        // We could use spawnProbability in SpawningPoint object to create smarter probability
    }
}
[System.Serializable]
public class SpawningPoint 
{
    public Vector3 position;
    public float spawnProbability;
}
