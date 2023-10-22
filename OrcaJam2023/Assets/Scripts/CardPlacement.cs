using FullscreenEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacement : MonoBehaviour
{
    public static CardPlacement instance;
    
    public GameObject currentPlaceable;
    public Card selectedCard;

    [SerializeField] LayerMask turretMask;


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

        if (selectedCard == null) return;


        MainCamera mainCam = MainCamera.instance;
        Vector2 mousePos = mainCam.MousePos;
        float clampedX = Mathf.Clamp(mousePos.x, 
            mainCam.bounds.center.x + -mainCam.bounds.extents.x, 
            mainCam.bounds.center.x + mainCam.bounds.extents.x);
        float clampedY = Mathf.Clamp(mousePos.y,
            mainCam.bounds.center.y + -mainCam.bounds.extents.y,
            mainCam.bounds.center.y + mainCam.bounds.extents.y);

        Vector2 clampedMousePos = new Vector2(clampedX, clampedY);

        if (currentPlaceable != null)
        {
            currentPlaceable.transform.position = clampedMousePos;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (selectedCard.CardData.upgradeType != Upgrade.UpgradeType.None)
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward, 2f, turretMask);
                if (hit && hit.collider.TryGetComponent(out Turret turret))
                {
                    Upgrade.UpgradeTurret(selectedCard.CardData.upgradeType, turret);

                    GameManager.instance.gold -= selectedCard.CardData.cost;
                    PlayerUI.instance.UpdateGold();
                    Destroy(selectedCard.gameObject);
                    selectedCard = null;
                }
            }

            else if (currentPlaceable != null && currentPlaceable.GetComponent<Turret>().placeable)
            {
                GameManager.instance.gold -= selectedCard.CardData.cost;
                PlayerUI.instance.UpdateGold();
                currentPlaceable.GetComponent<IPlaceable>().Place(currentPlaceable.transform.position);
                currentPlaceable = null;
                Destroy(selectedCard.gameObject);
                selectedCard = null;
            }
            else
            {
                print("Can't Build There");
            }
            
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
