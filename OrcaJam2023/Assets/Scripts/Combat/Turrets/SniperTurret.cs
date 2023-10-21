using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTurret : MonoBehaviour, IShooter
{
    void IShooter.OnTargetHit()
    {
        print("target hit");
    }
}
