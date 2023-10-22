using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpleenTween;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    float maxMoveSpeed;
    public bool Attacking { get; set; }
    [SerializeField] public float moveSpeed;

    Rigidbody2D _rb;

    [SerializeField] float animateScale;

    Vector2 dir;

    [SerializeField] float minChangeDirInterval = 0.25f;
    [SerializeField] float maxChangeDirInterval = 1.5f;

    [SerializeField] float maxAngleChange = 30f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        maxMoveSpeed = moveSpeed;
        Attacking = false;
        Animate();
        StartCoroutine(UpdateDirection());
    }

    void Update()
    {
        if (!Attacking)
        {
            Move();
        }
        
    }

    void Move()
    {
        _rb.AddForce(dir * (moveSpeed * Time.deltaTime));
    }

    void Animate()
    {
        float randSpeed = Random.Range(75f, 100f) / moveSpeed;
        Spleen.ScaleY(transform, transform.localScale.y - animateScale, randSpeed, Ease.InOutQuad).SetLoop(Loop.Yoyo);
        Spleen.ScaleX(transform, transform.localScale.y + animateScale, randSpeed, Ease.InOutQuad).SetLoop(Loop.Yoyo);
    }

    public void AttackAnimate()
    {
        float randSpeed = Random.Range(75f, 100f) / moveSpeed;
        Spleen.AddPosX(transform, Random.Range(0.5f,1.5f), Random.Range(0.20f, 0.55f), Ease.InCubic).SetLoop(Loop.Rewind);
    }


    IEnumerator UpdateDirection()
    {
        float randomAngle = Random.Range(-maxAngleChange, maxAngleChange);
        float angleInRadians = randomAngle * Mathf.Deg2Rad;
        dir = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        yield return new WaitForSeconds(Random.Range(minChangeDirInterval, maxChangeDirInterval));
        StartCoroutine(UpdateDirection());
    }

    public void SlowEnemy(float percentAmount)
    {
        StartCoroutine(Slow(percentAmount));
    }

    IEnumerator Slow(float percentAmount)
    {
        moveSpeed *= 1 - percentAmount;
        yield return new WaitForSeconds(1);
        moveSpeed = maxMoveSpeed;
    }
}
