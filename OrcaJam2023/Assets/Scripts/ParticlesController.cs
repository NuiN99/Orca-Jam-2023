using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] GameObject blackExplosion;
    [SerializeField] GameObject whiteExplosion;

    public static ParticlesController instance;

    void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    public void SpawnExplosion(Vector3 pos)
    {
        GameObject effect1 = Instantiate(blackExplosion, pos, Quaternion.identity);
        GameObject effect2 = Instantiate(whiteExplosion, pos, Quaternion.identity);
        Destroy(effect1, .5f);
        Destroy(effect2, .5f);
    }

    public void SpawnWhiteExplosion(Vector3 pos)
    {
        GameObject effect = Instantiate(whiteExplosion, pos, Quaternion.identity);
        Destroy(effect, .5f);
    }
}
