using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    protected float duration;
    float curTime;
    protected BasicEnemy target;

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
