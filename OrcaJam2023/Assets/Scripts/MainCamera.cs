using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [HideInInspector] public Camera cam;
    [HideInInspector] public Bounds bounds;
    public Vector3 MousePos => cam.ScreenToWorldPoint(Input.mousePosition);

    public static MainCamera instance;

    void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;

        bounds = GetComponent<BoxCollider2D>().bounds;
        cam = GetComponent<Camera>();
    }

    
}
