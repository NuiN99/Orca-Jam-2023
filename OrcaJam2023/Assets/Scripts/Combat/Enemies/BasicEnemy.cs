using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    public Health health;
    public int gold;

    public static event Action<int> OnDeathGold;
    public static event Action OnDeath;
    public AudioClip deathAudioClip;
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

        SoundPlayer.instance.PlaySound(deathAudioClip, Random.Range(0.6f,0.85f));

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
            if (!effect.UpdateEffect(0.5f))
            {
                curEffects.RemoveAt(i);
            }
        }
    }

    IEnumerator UpdateEffectsRepeating()
    {
        UpdateEffects();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UpdateEffectsRepeating());
    }
}
