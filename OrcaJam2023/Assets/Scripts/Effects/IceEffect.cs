using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : Effect
{
    public IceEffect(BasicEnemy target, float duration) : base(target, duration)
    {
        this.target = target;
        this.duration = duration;
    }

    public override void DealEffect()
    {
        target.GetComponent<EnemyMovement>().SlowEnemy(0.25f);
        ParticlesController.instance.SpawnIce(target.transform);
    }
}
