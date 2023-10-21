using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentWave = 1;

    Path _path;

    [SerializeField] BasicEnemy[] general;
    [SerializeField] BasicEnemy[] tank;
    [SerializeField] BasicEnemy[] fast;

    
    public static event Action OnCompletedWave;

    int EnemiesPerWave => currentLevel * currentWave + Random.Range(3, 8);
    int TimeBetweenEnemies => Random.Range(1, 3) * (10 / (currentLevel * currentWave));

    void Awake()
    {
        _path = FindObjectOfType<Path>();
    }

    [ContextMenu("Start Wave")]
    public void StartWave()
    {
        StartCoroutine(StartWave(EnemiesPerWave));
    }

    IEnumerator StartWave(int enemyCount)
    {
        int totalSpawnedEnemies = 0;
        int miniWaveSpawnedEnemies = 0;
        int timeBetweenEnemies = TimeBetweenEnemies;
        int breakTime = timeBetweenEnemies * 10;
        int breakCount = enemyCount / Random.Range(5, 10);

        while (totalSpawnedEnemies < enemyCount)
        {
            BasicEnemy randEnemy = GetEnemy();
            Instantiate(randEnemy, _path.waypoints[0].position, Quaternion.identity);
            miniWaveSpawnedEnemies++;
            totalSpawnedEnemies++;
            yield return timeBetweenEnemies;

            if (miniWaveSpawnedEnemies >= breakCount)
            {
                breakCount = enemyCount / Random.Range(5, 10);
                miniWaveSpawnedEnemies = 0;
                timeBetweenEnemies = TimeBetweenEnemies;
                yield return new WaitForSeconds(breakTime);
            }
        }
    }

    BasicEnemy GetEnemy()
    {
        return currentLevel switch
        {
            <= 3 => GetRandomEnemyArray()[0],
            > 3 and <= 6 => GetRandomEnemyArray()[1],
            > 6 => GetRandomEnemyArray()[2],
        };
    }

    BasicEnemy[] GetRandomEnemyArray()
    {
        int rand = Random.Range(0, 4);
        return rand switch
        {
            1 => general,
            2 => tank,
            3 => fast,
            _ => null
        };
    }
}
