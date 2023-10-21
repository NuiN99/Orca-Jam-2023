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
        BuildingPlacement.instance.selectedCard = this;
        BuildingPlacement.instance.currentPlaceable = Instantiate(CardData.turret);
        ToggleHighlight();   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.localPosition.y == -75)//if it hasn't moved 
            runningCoroutine = StartCoroutine("MoveUp",0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.localPosition.y > -75)
        {
            StopCoroutine(runningCoroutine);
            transform.localPosition = new Vector3(transform.localPosition.x,-75,transform.localPosition.z);
        }//if it has moved 
            
    }


    public void ToggleHighlight() {
        highlight.SetActive(!highlight.activeSelf);
    }

    IEnumerator MoveUp(float LerpTime)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        Vector3 finalPosition = startPosition + transform.up * 25;

        while (time / LerpTime < 1) {
            time += Time.deltaTime;
            transform.position = Vector3.Slerp(startPosition, finalPosition, time / LerpTime);
            yield return null;
        }

    }


}
    