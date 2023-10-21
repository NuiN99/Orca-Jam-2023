using FullscreenEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    public static BuildingPlacement instance;
    
    public GameObject currentPlaceable;
    public Card selectedCard;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    void Update()
    {

        if (currentPlaceable == null) return;


        MainCamera mainCam = MainCamera.instance;
        Vector2 mousePos = mainCam.MousePos;
        float clampedX = Mathf.Clamp(mousePos.x, 
            mainCam.bounds.center.x + -mainCam.bounds.extents.x, 
            mainCam.bounds.center.x + mainCam.bounds.extents.x);
        float clampedY = Mathf.Clamp(mousePos.y,
            mainCam.bounds.center.y + -mainCam.bounds.extents.y,
            mainCam.bounds.center.y + mainCam.bounds.extents.y);

        Vector2 clampedMousePos = new Vector2(clampedX, clampedY);
        currentPlaceable.transform.position = clampedMousePos;


        if (Input.GetMouseButtonDown(0))
        {

           
            GameManager.instance.gold -= selectedCard.CardData.cost;
            PlayerUI.instance.UpdateGold();
            currentPlaceable.GetComponent<IPlaceable>().Place(currentPlaceable.transform.position);
            currentPlaceable = null;
            Destroy(selectedCard.gameObject);
            selectedCard = null;


        }

        if (Input.GetMouseButtonDown(1))
        {
            ReleaseCard();
        }

       
    }

    void ReleaseCard()
    {
        Destroy(currentPlaceable);
        selectedCard.ToggleHighlight();
        selectedCard = null;
    }

    /*
    public void SetHeldObject()      
    {
        currentPlaceable = Instantiate(selectedCard.CardData.turret);
    }
    */
}
