using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public BasicEnemy furthestEnemy;
    public List<BasicEnemy> currentEnemies = new();

    void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;

        currentEnemies = FindObjectsByType<BasicEnemy>(FindObjectsSortMode.None).ToList();
    }

    void Update()
    {
        SetFurthestEnemy();
    }

    void SetFurthestEnemy()
    {
        float curDist = float.MaxValue;
        foreach (var enemy in currentEnemies)
        {
            float distFromEnd = Vector2.Distance(enemy.transform.position, enemy.path.waypoints[^1].position);

            if (distFromEnd < curDist)
            {
                curDist = distFromEnd;
                furthestEnemy = enemy;
            }
        }
    }

    public void RemoveEnemy(BasicEnemy enemy)
    {
        currentEnemies.Remove(enemy);
    }

    void OnDrawGizmos()
    {
        if (furthestEnemy != null)
        {
            Gizmos.DrawWireSphere(furthestEnemy.transform.position, .25f);
        }
    }
}