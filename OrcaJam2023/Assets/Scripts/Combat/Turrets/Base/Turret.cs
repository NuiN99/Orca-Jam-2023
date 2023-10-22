using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IPlaceable
{
    protected Health target;

    protected bool detect = true;
    
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float atkInterval = 0.25f;
    [SerializeField] protected float projectileSpeed = 1f;

    [SerializeField] float rotationSpeed = 5f;

    [SerializeField] protected float detectionRadius = 2.5f;
    [SerializeField] float fov = 30f;
    [SerializeField] protected LayerMask targetMask;

    [SerializeField] protected Projectile projectilePrefab;

    [SerializeField] protected Transform shootPos;

    public CardData CardData { get; set; }

    float _curInterval;

    public Action onHit;

    public bool placeable;

    void Start() { 
    
        placeable = true;
    }


    void Update()
    {
        if (!detect) return;

        DetectTarget();
        if (target) RotateToTarget();

        _curInterval -= Time.deltaTime;
        if (!(_curInterval <= 0) || target == null) return;
        _curInterval = atkInterval;

        if (target) Shoot();
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
                target = enemy.health;
            }
        }

        if(detections.Length <= 0) target = null;
    }

    void RotateToTarget()
    {
        float dir = Mathf.Sign(target.transform.position.x - transform.position.x);
        transform.localScale = new Vector3(dir, 1, 1);
    }

    public virtual void Shoot()
    {

    }

    bool InFOV()
    {
        Vector3 targetDir = (target.transform.position - transform.position).normalized;
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

        if(target) Debug.DrawLine(shootPos.position, target.transform.position, Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Enter Trigger");
        if(placeable && collision.gameObject.TryGetComponent(out Turret turret))
        {
            placeable = !placeable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Exit Trigger");
        if (!placeable)
        {
            placeable = !placeable;
        }
    }

    void IPlaceable.Place(Vector3 pos)
    {   
        enabled = true;
        transform.position = pos;
    }

}
