using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    protected float duration;
    float curTime;
    protected BasicEnemy target;

    public Effect(BasicEnemy target, float duration)
    {
        this.duration = duration;
        this.target = target;
    }

    public bool UpdateEffect()
    {
        if (target == null) return false;
        curTime += Time.deltaTime;
        DealEffect();
        return !(curTime >= duration);
    }

    public virtual void DealEffect()
    {

    }
}
