using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float atkInterval = 0.25f;
    [SerializeField] float projectileSpeed = 1f;

    [SerializeField] GameObject projectilePrefab;
}
