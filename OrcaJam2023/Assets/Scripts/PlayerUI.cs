using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text playerGoldText;
    public TMP_Text waveText;

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
        BasicEnemy.OnDeath += (_) => UpdateGold();
        WaveManager.OnCompletedWave += ShowWaveText;
    }

    private void OnDisable()
    {
        GameManager.startGame -= UpdateGold;
        BasicEnemy.OnDeath -= (_) => UpdateGold();
        WaveManager.OnCompletedWave -= ShowWaveText;
    }



    public void SetGold(int gold)
    {
        playerGoldText.text = gold.ToString();  
    }

    public void UpdateGold()
    {
        playerGoldText.text = GameManager.instance.gold.ToString();
    }


    public void ShowWaveText(int w)
    {
        StartCoroutine("ShowWave",w);
    }

    public IEnumerator ShowWave(int wave)
    {
        waveText.enabled = true;
        waveText.text = "Wave " + wave;
        yield return new WaitForSeconds(2.5f);

        waveText.enabled = false;
    }

}
