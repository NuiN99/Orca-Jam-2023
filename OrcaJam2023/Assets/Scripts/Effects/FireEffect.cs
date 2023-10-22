using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : Effect
{
    public FireEffect(BasicEnemy target, float duration) : base(target, duration)
    {
        this.target = target;
        this.duration = duration;
    }

    public override void DealEffect()
    {
        target.health.TakeDamage(1);
        ParticlesController.instance.SpawnFire(target.transform);
    }
}
