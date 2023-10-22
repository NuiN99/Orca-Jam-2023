using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    public Health health;
    public int gold;

    public static event Action<int> OnDeathGold;
    public static event Action OnDeath;

    List<Effect> curEffects = new();

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Start()
    {
        StartCoroutine(UpdateEffectsRepeating());
    }

    void IDamageable.Damaged()
    {
        print("damaged");
    }

    void IDamageable.Die()
    {
        EnemyManager.instance.RemoveEnemy(this);

        if(health._health <= 0)
        {
            OnDeathGold?.Invoke(gold);
            OnDeath?.Invoke();
        }
        else
        {
            OnDeath?.Invoke();
        }

        Destroy(gameObject);
    }

    public void AddEffect(Effect effect)
    {
        curEffects.Add(effect);
    }

    void UpdateEffects()
    {
        for (int i = 0; i < curEffects.Count; i++)
        {
            var effect = curEffects[i];
            if (!effect.UpdateEffect())
            {
                curEffects.RemoveAt(i);
            }
        }
    }

    IEnumerator UpdateEffectsRepeating()
    {
        UpdateEffects();
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(UpdateEffectsRepeating());
    }
}
