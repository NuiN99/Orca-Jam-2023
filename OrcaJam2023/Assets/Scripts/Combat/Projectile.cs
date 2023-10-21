using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Health _target;
    float _speed;
    int _damage;
    Action _onHit;

    void Update()
    {
        if (!_target)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetDir = (_target.transform.position - transform.position).normalized;
        transform.position += targetDir * (_speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _target.transform.position) <= 0.5f)
        {
            _target.TakeDamage(_damage);
            _onHit?.Invoke();
        }
    }

    public void Init(Health target, float speed, int damage, Action onHit)
    {
        _target = target;
        _speed = speed;
        _damage = damage;
        _onHit = onHit;
    }
}
