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

    public bool UpdateEffect(float interval)
    {
        if (target == null) return false;
        curTime += interval;
        Debug.Log(curTime);
        DealEffect();
        return !(curTime >= duration);
    }

    public virtual void DealEffect()
    {

    }
}
