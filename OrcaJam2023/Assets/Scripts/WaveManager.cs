using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentWave = 1;
    public int currentEnemyCount = 0;
    public static WaveManager instance;

    [SerializeField] BoxCollider2D spawnCollider;
    Bounds SpawnBounds => spawnCollider.bounds;

    Vector2 RandomPos
    {
        get
        {
            float x = Random.Range(-SpawnBounds.extents.x, SpawnBounds.extents.x);
            float y = Random.Range(-SpawnBounds.extents.y, SpawnBounds.extents.y);

            return new Vector3(x, y) + SpawnBounds.center;
        }
    }

    [SerializeField] BasicEnemy[] general;
    [SerializeField] BasicEnemy[] tank;
    [SerializeField] BasicEnemy[] fast;

    
    public static event Action OnCompletedWave;

    int EnemiesPerWave => currentLevel * currentWave + Random.Range(3, 8);
    float TimeBetweenEnemies => Random.Range(1, 3) * (1f / (currentLevel * currentWave));

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;

    }


    private void OnEnable()
    {
        GameManager.startGame += StartWave;
        BasicEnemy.OnDeath += ReduceEnemyCount;
    }

    private void OnDisable()
    {
        GameManager.startGame -= StartWave;
        BasicEnemy.OnDeath -= ReduceEnemyCount;
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
        float timeBetweenEnemies = TimeBetweenEnemies;
        float breakTime = Random.Range(0.5f, 2f);
        int breakCount = enemyCount / Random.Range(5, 10);

        while (totalSpawnedEnemies < enemyCount)
        {
            BasicEnemy randEnemy = GetEnemy();
            Instantiate(randEnemy, RandomPos, Quaternion.identity);
            currentEnemyCount++;
            miniWaveSpawnedEnemies++;
            totalSpawnedEnemies++;
            yield return new WaitForSeconds(timeBetweenEnemies);

            if (miniWaveSpawnedEnemies >= breakCount)
            {
                breakCount = enemyCount / Random.Range(5, 10);
                miniWaveSpawnedEnemies = 0;
                timeBetweenEnemies = TimeBetweenEnemies;
                yield return new WaitForSeconds(breakTime);
            }
        }

        yield return new WaitUntil(()=> currentEnemyCount == 0);

        currentWave++;

        currentLevel = currentWave switch
        {
            <= 5 => 1,
            > 5 and <= 10 => 2,
            > 10 => 3,
        };

        OnCompletedWave?.Invoke();
       
        yield return new WaitForSeconds(5);

        StartWave();
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
        int rand = Random.Range(0, 3);
        return rand switch
        {
            0 => general,
            1 => tank,
            2 => fast,
            _ => null
        };
    }

    void ReduceEnemyCount()
    {
        currentEnemyCount--;
    }
}
