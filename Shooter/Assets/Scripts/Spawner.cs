using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject EnnemyPrefab;

    [SerializeField]
    private int MaxMonsters;
    [SerializeField]
    private int SpawnInterval;
    [SerializeField]
    private GameObject BossPrefab;
    [SerializeField]
    private int EnemyKilledBeforeBoss;

    private float TimeZero;
    private float m_LastSpawnTime;

    private void Start()
    {
        TimeZero = Time.time;
    }

    void Update()
    {
        if(GameManager.Instance.NumberOfMonsterOnMap < MaxMonsters && Time.time - m_LastSpawnTime >= SpawnInterval && GameManager.Instance.EnemyKilled < EnemyKilledBeforeBoss)
        {
            m_LastSpawnTime = Time.time;
            InstantiateEnemy();
        }

        if(GameManager.Instance.EnemyKilled >= EnemyKilledBeforeBoss && !GameManager.Instance.ActiveBoss)
        {
            ClearChildren();

            StartCoroutine(InstantiateBoss());
        }
    }

    private void InstantiateEnemy()
    {
        Instantiate(EnnemyPrefab,transform.position,Quaternion.identity); 
        GameManager.Instance.NumberOfMonsterOnMap++;
    }

    private IEnumerator InstantiateBoss()
    {
        GameManager.Instance.ActiveBoss = true;
        yield return new WaitForSeconds(0.1f);
        Instantiate(BossPrefab, transform.position, Quaternion.identity);
    }

    private void ClearChildren()
    {
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(child);
        }
    }
}
