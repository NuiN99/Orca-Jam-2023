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
    public int gold;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
      
    }

    private void OnEnable()
    {
        BasicEnemy.OnDeathGold += AddGold;   
    }
    private void OnDisable()
    {
        BasicEnemy.OnDeathGold -= AddGold;
    }

    private void Start()
    {
        // trigger draw card x times
        startGame();
        // 
    }

    void AddGold(int amount)
    {
        gold += amount;
        PlayerUI.instance.UpdateGold();
    }




}
