using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpleenTween;
using Random = UnityEngine.Random;

public class FollowPath : MonoBehaviour
{
    [SerializeField] Path path;

    [SerializeField] float moveSpeed;

    [SerializeField] float pointDistThreshold;

    int _curPointIndex;
    Transform CurrentWaypoint => path.waypoints[Mathf.Clamp(_curPointIndex, 0, path.waypoints.Count - 1)];
    
    Rigidbody2D _rb;

    [SerializeField] float animateAngle = 30f;
    [SerializeField] float animateScale;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        path = FindObjectOfType<Path>();
    }

    void Start()
    {
        Animate();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (_curPointIndex >= path.waypoints.Count) return;

        float distFromPoint = Vector2.Distance(transform.position, CurrentWaypoint.position);
        if (distFromPoint <= pointDistThreshold) _curPointIndex++;

        Vector2 waypointDir = (CurrentWaypoint.position - transform.position).normalized;
        _rb.AddForce(waypointDir * (moveSpeed * Time.deltaTime));
    }

    void Animate()
    {
        float randSpeed = Random.Range(75f, 100f) / moveSpeed;
        Spleen.ScaleY(transform, transform.localScale.y - animateScale, randSpeed, Ease.InOutQuad).SetLoop(Loop.Yoyo);
        Spleen.ScaleX(transform, transform.localScale.y + animateScale, randSpeed, Ease.InOutQuad).SetLoop(Loop.Yoyo);
        //Spleen.RotZ(transform, -animateAngle, animateAngle, (Random.Range(75f, 100f) / moveSpeed), Ease.InOutSine).SetLoop(Loop.Yoyo);
    }
}
