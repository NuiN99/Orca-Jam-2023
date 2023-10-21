using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action startGame;
    public static Action drawCard;


    //player data
    public int gold = 9999;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

      
    }

    private void Start()
    {
        // trigger draw card x times
        startGame();
        // 
    }




}
