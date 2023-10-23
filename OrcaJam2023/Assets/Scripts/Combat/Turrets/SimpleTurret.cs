using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurret : Turret
{
    public override void Shoot()
    {
        base.Shoot();
        Projectile projectile = Instantiate(projectilePrefab, shootPos.position, Quaternion.identity);
        projectile.Init(target, projectileSpeed, damage, onHit);
    }
}
