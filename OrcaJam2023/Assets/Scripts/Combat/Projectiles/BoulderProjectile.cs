using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderProjectile : Projectile
{
    [SerializeField] LayerMask enemyMask;

    List<Health> damagedEnemies = new();

    Vector3 _targetPos;
    void Start()
    {
        _targetPos = target.transform.position;
        Vector3 dir = (_targetPos - transform.position).normalized;
        _targetPos = transform.position + dir * 3.5f;
    }

    public override void Movement()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1f, Vector3.zero, 0, enemyMask);
        foreach (RaycastHit2D hit in hits)
        {
            if(hit.collider.TryGetComponent(out BasicEnemy enemy))
            {
                if (damagedEnemies.Contains(enemy.health)) continue;

                damagedEnemies.Add(enemy.health);
                enemy.health.TakeDamage(damage);
            }
        }

        Vector3 targetDir = (_targetPos - transform.position).normalized;
        transform.position += targetDir * (speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _targetPos) <= 0.25f)
        {
            Destroy(gameObject);
        }
    }
}
