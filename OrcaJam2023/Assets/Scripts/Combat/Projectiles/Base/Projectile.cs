using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Health target;
    protected float speed;
    protected int damage;
    protected Action<BasicEnemy> onHit;

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

        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Vector2.Distance(transform.position, target.transform.position) <= 0.25f)
        {
            target.TakeDamage(damage);
            onHit?.Invoke(target.GetComponent<BasicEnemy>());
            Destroy(gameObject);
        }
    }

    public void Init(Health target, float speed, int damage, Action<BasicEnemy> onHit)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;
        this.onHit += onHit;
    }
}
