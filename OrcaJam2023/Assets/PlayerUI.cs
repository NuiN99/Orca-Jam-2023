using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text playerGoldText;

    public static PlayerUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }


    private void OnEnable()
    {
        GameManager.startGame += UpdateGold;
    }

    private void OnDisable()
    {
        GameManager.startGame -= UpdateGold;
    }



    public void SetGold(int gold)
    {
        playerGoldText.text = gold.ToString();
    }

    public void UpdateGold()
    {
        playerGoldText.text = GameManager.instance.gold.ToString();
    }

}
