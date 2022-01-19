using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject SpaceshipPrefab;

    private float lastSpawnTime;
    private float randomSpawnDelay;

    void Start()
    {
        Instantiate(SpaceshipPrefab, transform);
        randomSpawnDelay = Random.Range(5, 10);
    }

    void Update()
    {
        if(Time.time - lastSpawnTime >= randomSpawnDelay)
        {
            SpawnSpaceship();
        }
    }

    private void SpawnSpaceship()
    {
        Instantiate(SpaceshipPrefab,transform);
        lastSpawnTime = Time.time;
        randomSpawnDelay = Random.Range(10,20);
    }
}
