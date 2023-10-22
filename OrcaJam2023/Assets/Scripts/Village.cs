using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    public int health;

    public static Village instance;

    void Awake()
    {
        if(instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }
}
