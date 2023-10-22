using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] int maxHealth = 25;
    public int _health;

    IDamageable _damageable;

    void Awake()
    {
        _damageable = GetComponent<IDamageable>();
    }

    void Start()
    {
        _health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _damageable.Damaged();
        if (_health <= 0)
        {
            _damageable.Die();
        }
    }
}
