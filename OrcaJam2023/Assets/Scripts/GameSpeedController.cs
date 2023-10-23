using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedController : MonoBehaviour
{
    [SerializeField] Button speed1;
    [SerializeField] Button speed2;
    [SerializeField] Button speed4;
    [SerializeField] Button speed8;
    [SerializeField] Button speed32;

    void OnEnable()
    {
        speed1.onClick.AddListener(() => Time.timeScale = 1f);
        speed2.onClick.AddListener(() => Time.timeScale = 2f);
        speed4.onClick.AddListener(() => Time.timeScale = 4f);
        speed8.onClick.AddListener(() => Time.timeScale = 8f);
        speed32.onClick.AddListener(() => Time.timeScale = 32f);
    }

    void OnDisable()
    {
        speed1.onClick.RemoveAllListeners();
        speed2.onClick.RemoveAllListeners();
        speed4.onClick.RemoveAllListeners();
        speed8.onClick.RemoveAllListeners();
        speed32.onClick.RemoveAllListeners();
    }
}