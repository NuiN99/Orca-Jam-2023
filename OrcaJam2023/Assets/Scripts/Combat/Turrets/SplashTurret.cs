using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTurret : Turret
{
    [SerializeField] float splashRadius = 1.5f;
    Action<BasicEnemy> onReachedEnemy;
    void Awake()
    {
        onReachedEnemy += SplashDamage;
    }

    public override void Shoot()
    {
        base.Shoot();
        Projectile projectile = Instantiate(projectilePrefab, shootPos.position, Quaternion.identity);
        projectile.Init(target, projectileSpeed, damage, onReachedEnemy);
    }

    void SplashDamage(BasicEnemy test)
    {
        if (target == null) return;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(target.transform.position, splashRadius, Vector3.forward, 0, targetMask);
        foreach (var hit in hits)
        {
            if (!hit.collider.TryGetComponent(out BasicEnemy enemy)) continue;
            enemy.health.TakeDamage(damage);
            onHit?.Invoke(enemy);
        }

        ParticlesController.instance.SpawnExplosion(target.transform.position);
    }
}
