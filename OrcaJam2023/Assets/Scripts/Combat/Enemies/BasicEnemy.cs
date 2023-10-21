using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    public Path path;
    public Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        path = FindObjectOfType<Path>();
    }

    void IDamageable.Damaged()
    {
        print("damaged");
    }

    void IDamageable.Die()
    {
        EnemyManager.instance.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
