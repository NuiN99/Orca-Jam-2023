using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    public Health health;
    public int gold;

    public static event Action<int> OnDeathGold;
    public static event Action OnDeath;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void IDamageable.Damaged()
    {
        print("damaged");
    }

    void IDamageable.Die()
    {
        EnemyManager.instance.RemoveEnemy(this);

        if(health._health <= 0)
        {
            OnDeathGold?.Invoke(gold);
            OnDeath?.Invoke();
        }
        else
        {
            OnDeath?.Invoke();
        }

        Destroy(gameObject);
    }
}
