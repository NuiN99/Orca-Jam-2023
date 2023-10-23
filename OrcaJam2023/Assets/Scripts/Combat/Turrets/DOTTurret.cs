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
        StartCoroutine(ApplyBurnToEnemiesInRangeOverTime());
    }

    void ApplyBurnToEnemiesInRange()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, detectionRadius, Vector2.zero, 0, targetMask);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent(out BasicEnemy enemy))
            {
                FireEffect effect = new(enemy, 3f, damage);
                enemy.AddEffect(effect);
                onHit?.Invoke(enemy);
            }
        }

        ParticlesController.instance.SpawnPriestExplosion(transform.position);
    }

    
        IEnumerator ApplyBurnToEnemiesInRangeOverTime()
    {
        SoundPlayer.instance.PlaySound(shootAudioClip, clipVolume);
        ApplyBurnToEnemiesInRange();
        yield return new WaitForSeconds(atkInterval);
        StartCoroutine(ApplyBurnToEnemiesInRangeOverTime());
    }
}
