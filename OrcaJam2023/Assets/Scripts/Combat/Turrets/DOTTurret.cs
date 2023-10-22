using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTTurret : Turret
{
    [SerializeField] GameObject damageEffect;
    void Start()
    {
        detect = false;
        StartCoroutine(DamageOverTime());
    }

    void DamageEnemiesInRange()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, detectionRadius, Vector2.zero, 0, targetMask);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }
        }

        ParticlesController.instance.SpawnWhiteExplosion(transform.position);
    }

    IEnumerator DamageOverTime()
    {
        DamageEnemiesInRange();
        yield return new WaitForSeconds(atkInterval);
        StartCoroutine(DamageOverTime());
    }
}
