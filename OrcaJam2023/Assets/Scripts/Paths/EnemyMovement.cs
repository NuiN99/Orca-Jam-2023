using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpleenTween;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Rigidbody2D _rb;

    [SerializeField] float animateScale;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        _rb.AddForce(transform.right * (moveSpeed * Time.deltaTime));
    }

    void Animate()
    {
        float randSpeed = Random.Range(75f, 100f) / moveSpeed;
        Spleen.ScaleY(transform, transform.localScale.y - animateScale, randSpeed, Ease.InOutQuad).SetLoop(Loop.Yoyo);
        Spleen.ScaleX(transform, transform.localScale.y + animateScale, randSpeed, Ease.InOutQuad).SetLoop(Loop.Yoyo);
    }
}
