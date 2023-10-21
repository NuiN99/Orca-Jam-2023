using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    void IDamageable.Damaged()
    {
        print("damaged");
    }

    void IDamageable.Die()
    {
        Destroy(gameObject);
    }
}
