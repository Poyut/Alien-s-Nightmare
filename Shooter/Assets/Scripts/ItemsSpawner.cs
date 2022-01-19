using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Items;

    private float time;
    private float spawnTime;


    void Start()
    {
        SetRandomTime();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if(time >= spawnTime)
        {
            SpawnItem(SetSpawnArea());
        }
    }

    private Vector3 SetSpawnArea()
    {
        Vector3 origin = transform.position;
        Vector3 randomRange = new Vector3(Random.Range(-8,8),
                                          Random.Range(-4.5f, 1f),
                                          Random.Range(0, 0));
        Vector3 randomCoordinate = origin + randomRange;
        return randomCoordinate;
    }
    private void SetRandomTime()
    {
        spawnTime = Random.Range(15f, 30f);
    }

    private void SpawnItem(Vector3 position)
    {
        time = 0;
        Instantiate<GameObject>(Items[Random.Range(0,Items.Length)],position,Quaternion.identity);
    }
}
