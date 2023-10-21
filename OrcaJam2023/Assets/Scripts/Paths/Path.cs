using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> waypoints = new();

    [SerializeField] Vector2 start;
    [SerializeField] Vector2 end;

    [SerializeField] GameObject waypointPrefab;

    void Start()
    {
        /*foreach (Vector3 point in PathCreation.CreatePathPoints(start, end))
        {
            Transform waypoint = Instantiate(waypointPrefab, point, Quaternion.identity).transform;
            waypoints.Add(waypoint);
            waypoint.SetParent(transform);
        }*/
    }
}
