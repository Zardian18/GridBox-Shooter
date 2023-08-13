using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject[] enemy;
    [SerializeField]
    float timeBetweenEnemySpawn = 5f;
    
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        do
        {
            int enemyNo = Random.Range(0, enemy.Length);
            int pointNo = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[pointNo].transform.position;
            Instantiate(enemy[enemyNo], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenEnemySpawn);
        }
        while (true);// time not over
    }
}
