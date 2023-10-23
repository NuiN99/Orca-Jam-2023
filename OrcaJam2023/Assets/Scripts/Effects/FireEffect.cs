using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : Effect
{
    int damage;
    public FireEffect(BasicEnemy target, float duration, int damage) : base(target, duration)
    {
        this.target = target;
        this.duration = duration;
        this.damage = damage;
    }

    public override void DealEffect()
    {
        target.health.TakeDamage(damage);
        ParticlesController.instance.SpawnFire(target.transform);
    }
}
