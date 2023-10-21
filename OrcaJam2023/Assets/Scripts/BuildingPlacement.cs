using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    public static BuildingPlacement instance;
    
    
    public Placeable currentPlaceable;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
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
    }

    public void SetHeldObject(Placeable placeable)      
    {
        currentPlaceable = placeable;
    }
}
