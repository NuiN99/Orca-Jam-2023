using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Upgrade
{
    public enum UpgradeType
    {
        None,
        FireTipped,
        IceTipped,
        //AddProjectile,
        AddDamage,
        AddFirerate,
        AddRange,
    }

    public void UpgradeTurret(UpgradeType upgradeType, Turret turret)
    {
        Health health = turret.target;
        BasicEnemy enemy = health.GetComponent<BasicEnemy>();
        switch (upgradeType)
        {
            case UpgradeType.None:
                return;

            case UpgradeType.FireTipped:
                turret.onHit += () =>
                {
                    if (health == null) return;
                    FireEffect fireEffect = new(enemy, 2f);
                    enemy.AddEffect(fireEffect);
                };
                break;
            
            case UpgradeType.IceTipped:
                turret.onHit += () =>
                {
                    if (health == null) return;
                    IceEffect iceEffect = new(enemy, 2f);
                    enemy.AddEffect(iceEffect);
                };
                break;

            case UpgradeType.AddDamage:
                turret.UpgradeDamage(5);
                break;

            case UpgradeType.AddFirerate:
                turret.UpgradeFirerate(10f);
                break;

            case UpgradeType.AddRange:
                turret.UpgradeRange(5f);
                break;
        }
    }
}
