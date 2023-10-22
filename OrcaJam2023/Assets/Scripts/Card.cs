using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
   public CardData CardData { get; set; }
   [SerializeField] GameObject highlight;
   [SerializeField] TMP_Text cost;
   [SerializeField] TMP_Text cardName;
   [SerializeField] TMP_Text description;
   [SerializeField] Coroutine runningCoroutine;

   public float startYPos;

   public void RenderData()
    {
        if (CardData != null)
        {
            cost.text = CardData.cost.ToString();
            cardName.text = CardData.cardName;
            description.text = CardData.description;    
              
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.gold >= CardData.cost)
        {
            BuildingPlacement.instance.selectedCard = this;
            BuildingPlacement.instance.currentPlaceable = Instantiate(CardData.turret);
            ToggleHighlight();
        }   
        else
        {
            print("Not Enough Gold");
            StartCoroutine(MoveDown(0.1f));
        }
          
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        runningCoroutine = StartCoroutine(MoveUp(0.1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(MoveDown(0.1f));
    }


    public void ToggleHighlight() {
        highlight.SetActive(!highlight.activeSelf);
    }

    IEnumerator MoveUp(float LerpTime)
    {
        float time = 0;
        Vector3 startPos = new Vector3(transform.position.x, startYPos);
        Vector3 finalPosition = startPos + transform.up * 75;

        while (time / LerpTime <= 1) {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, finalPosition, time / LerpTime);
            yield return null;
        }

    }

    IEnumerator MoveDown(float LerpTime)
    {
        StopCoroutine(runningCoroutine);
        float time = 0;
        Vector3 startPos = new Vector3(transform.position.x, startYPos);
        Vector3 finalPosition = startPos - transform.up * 75;

        while (time / LerpTime <= 1)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, finalPosition, time / LerpTime);
            yield return null;
        }
    }


}
    