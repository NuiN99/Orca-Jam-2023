using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTurret : Turret
{
    [SerializeField] float splashRadius = 1.5f;

    [SerializeField] GameObject explosionEffectBlack;
    [SerializeField] GameObject explosionEffectWhite;

    void Awake()
    {
        onHit += SplashDamage;
    }

    public override void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab, shootPos.position, Quaternion.identity);
        projectile.Init(target, projectileSpeed, damage, onHit);
    }

    void SplashDamage()
    {
        if (target == null) return;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(target.transform.position, splashRadius, Vector3.forward, 0, targetMask);
        foreach (var hit in hits)
        {
            if (!hit.collider.TryGetComponent(out BasicEnemy enemy)) continue;
            enemy.health.TakeDamage(damage);
        }

        GameObject effect1 = Instantiate(explosionEffectBlack, target.transform.position, Quaternion.identity);
        GameObject effect2 = Instantiate(explosionEffectWhite, target.transform.position, Quaternion.identity);
        Destroy(effect1, 2f);
        Destroy(effect2, 2f);
    }
}
