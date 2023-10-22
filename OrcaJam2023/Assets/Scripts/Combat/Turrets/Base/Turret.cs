using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IPlaceable
{
    Health _target;

    
    [SerializeField] int damage = 1;
    [SerializeField] float atkInterval = 0.25f;
    [SerializeField] float projectileSpeed = 1f;

    [SerializeField] float rotationSpeed = 5f;

    [SerializeField] float detectionRadius = 2.5f;
    [SerializeField] float fov = 30f;
    [SerializeField] LayerMask targetMask;

    [SerializeField] Projectile projectilePrefab;

    [SerializeField] Transform shootPos;

    public CardData CardData { get; set; }

    IShooter _turret;

    float _curInterval;

    void Awake()
    {
        _turret = GetComponent<IShooter>();
    }

    void Start()
    {
        _curInterval = atkInterval;

        //InvokeRepeating(nameof(DetectTarget), 0.25f, 0.25f);
    }

    void Update()
    {
        DetectTarget();
        //if (_target) RotateToTarget();

        _curInterval -= Time.deltaTime;
        if (!(_curInterval <= 0)) return;
        _curInterval = atkInterval;

        if (_target) Shoot();
    }

    void DetectTarget()
    {
        RaycastHit2D[] detections = Physics2D.CircleCastAll(transform.position, detectionRadius, Vector3.right, 0, targetMask);

        float closestDist = float.MaxValue;
        foreach (var hit in detections)
        {
            if (!hit.collider.TryGetComponent(out BasicEnemy enemy)) continue;

            float distFromEnd = Village.instance.transform.position.x - hit.transform.position.x;

            if (distFromEnd < closestDist)
            {
                closestDist = distFromEnd;
                _target = enemy.health;
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

    void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab, shootPos.position, Quaternion.identity);
        projectile.Init(_target, projectileSpeed, damage, _turret.OnTargetHit);
    }

    bool InFOV()
    {
        Vector3 targetDir = (_target.transform.position - transform.position).normalized;
        float targetAngle = Vector3.Angle(targetDir, transform.right);

        if (targetAngle <= fov / 2 && targetAngle >= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(shootPos.position, targetDir, detectionRadius, targetMask);
            if (hit)
            {
                return true;
            }
        }

        return false;
    }


    private void OnDrawGizmos()
    {
        /*Vector3 forwardRayPt = shootPos.position + transform.right * detectionRadius;
        Vector3 leftRayPt = shootPos.position + Quaternion.Euler(0, 0, fov / 2) * transform.right * detectionRadius;
        Vector3 rightRayPt = shootPos.position + Quaternion.Euler(0, 0, -(fov / 2)) * transform.right * detectionRadius;

        Debug.DrawLine(shootPos.position, forwardRayPt, Color.yellow);
        Debug.DrawLine(shootPos.position, leftRayPt, Color.yellow);
        Debug.DrawLine(shootPos.position, rightRayPt, Color.yellow);

        Debug.DrawLine(forwardRayPt, leftRayPt, Color.yellow);
        Debug.DrawLine(forwardRayPt, rightRayPt, Color.yellow);*/

        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if(_target) Debug.DrawLine(shootPos.position, _target.transform.position, Color.red);
    }

    void IPlaceable.Place(Vector3 pos)
    {   
        transform.position = pos;
    }
}
