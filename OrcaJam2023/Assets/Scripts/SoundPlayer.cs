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
        if(instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    public void PlaySound(AudioClip clip, float vol)
    {
        source.PlayOneShot(clip, vol);
    }
}
