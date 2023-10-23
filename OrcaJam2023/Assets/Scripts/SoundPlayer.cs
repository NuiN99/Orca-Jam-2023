using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource source;

    public static SoundPlayer instance;

    void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
                }
        instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, float vol)
    {
        source.PlayOneShot(clip, vol);
    }
}
