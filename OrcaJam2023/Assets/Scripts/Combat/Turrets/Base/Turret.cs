using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    Health _target;

    
    [SerializeField] int damage = 1;
    [SerializeField] float atkInterval = 0.25f;
    [SerializeField] float projectileSpeed = 1f;

    [SerializeField] float rotationSpeed = 5f;

    [SerializeField] float detectionRadius = 2.5f;
    [SerializeField] LayerMask targetMask;

    [SerializeField] Projectile projectilePrefab;

    [SerializeField] Transform shootPos;

    IShooter _turret;

    float _curInterval;

    void Awake()
    {
        _turret = GetComponent<IShooter>();
    }

    void Start()
    {
        _curInterval = atkInterval;
    }

    void Update()
    {
        DetectTarget();
        if (!_target) return;

        RotateToTarget();
        ShootOnInterval();
    }

    void DetectTarget()
    {
        RaycastHit2D[] detections = Physics2D.CircleCastAll(transform.position, detectionRadius, Vector3.forward, 0, targetMask);

        float closestDist = float.MaxValue;
        foreach (var hit in detections)
        {
            if (!hit.collider.TryGetComponent(out Health health)) continue;

            if (hit.distance < closestDist)
            {
                closestDist = hit.distance;
                _target = health;
            }
        }

        if(detections.Length <= 0) _target = null;
    }

    void RotateToTarget()
    {
        Vector3 targetDir = _target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void ShootOnInterval()
    {
        _curInterval -= Time.deltaTime;
        if (!(_curInterval <= 0)) return;

        _curInterval = atkInterval;

        Projectile projectile = Instantiate(projectilePrefab, shootPos.position, Quaternion.identity);
        projectile.Init(_target, projectileSpeed, damage, _turret.OnTargetHit);
    }
}
