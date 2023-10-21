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
        Debug.Log(this.gameObject.name + " Was Clicked.");
        BuildingPlacement.instance.selectedCard = this;
        BuildingPlacement.instance.currentPlaceable = Instantiate(CardData.turret);
        ToggleHighlight();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.localPosition.y <= -75)//if it hasn't moved 
            transform.position = transform.position + transform.up * 25;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(transform.localPosition.y > -75)//if it has moved 
            transform.position = transform.position + transform.up * -25;
    }


    public void ToggleHighlight() {
        highlight.SetActive(!highlight.activeSelf);
    }

}
    