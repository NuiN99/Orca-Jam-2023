using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    public int health;

    public static Village instance;

    void Awake()
    {
        if(instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable enemy))
        {
            enemy.Die();
            health -= 1;
        }
    }
}
