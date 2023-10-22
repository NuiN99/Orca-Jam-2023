using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    public float health;

    public static Village instance;


    public static event Action OnPlayerDeath;

    void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
        }
        
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyMovement enemy))
        {
            if (!enemy.Attacking){
                enemy.Attacking = true;
                enemy.AttackAnimate();
            }

            health -= 1;

            if (health <= 0)
            {
                OnPlayerDeath();
            }

        }
    }
}
