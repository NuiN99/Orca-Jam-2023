using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] Path path;

    [SerializeField] float moveSpeed;

    [SerializeField] float pointDistThreshold;

    int _curPointIndex;
    Transform CurrentWaypoint => path.waypoints[Mathf.Clamp(_curPointIndex, 0, path.waypoints.Count - 1)];
    
    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_curPointIndex >= path.waypoints.Count) return;

        float distFromPoint = Vector2.Distance(transform.position, CurrentWaypoint.position);
        if (distFromPoint <= pointDistThreshold) _curPointIndex++;

        Vector2 waypointDir = (CurrentWaypoint.position - transform.position).normalized;
        _rb.AddForce(waypointDir * (moveSpeed * Time.deltaTime));
    }
}
