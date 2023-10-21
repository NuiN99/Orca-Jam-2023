using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour , IPlaceable
{
    void IPlaceable.Place(Vector3 pos)
    {
       transform.position = pos;
    }
}
