using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
   public CardData CardData { get; set; }
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
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
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


    void ToggleHighlight() { 
        
    }

}
    