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
    }


    public void UpdateGold(int gold)
    {
        playerGoldText.text = gold.ToString();
    }
}
