using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Health target;
    protected float speed;
    protected int damage;
    protected Action onHit;

    void Update()
    {
        Movement();
    }

    public virtual void Movement()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetDir = (target.transform.position - transform.position).normalized;
        transform.position += targetDir * (speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.transform.position) <= 0.25f)
        {
            target.TakeDamage(damage);
            onHit?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Init(Health target, float speed, int damage, Action onHit)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;
        this.onHit = onHit;
    }
}
