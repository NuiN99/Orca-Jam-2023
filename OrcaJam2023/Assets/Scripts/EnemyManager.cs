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
