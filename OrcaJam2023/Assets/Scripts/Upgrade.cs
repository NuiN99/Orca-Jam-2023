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

    public static void UpgradeTurret(UpgradeType upgradeType, Turret turret)
    {
        switch (upgradeType)
        {
            case UpgradeType.None:
                return;

            case UpgradeType.FireTipped:
                turret.onHit += () =>
                {
                    Health health = turret.target;
                    if (health == null) return;
                    BasicEnemy enemy = health.GetComponent<BasicEnemy>();
                    FireEffect fireEffect = new(enemy, 2f);
                    enemy.AddEffect(fireEffect);
                };
                break;
            
            case UpgradeType.IceTipped:
                turret.onHit += () =>
                {
                    Health health = turret.target;
                    if (health == null) return;
                    BasicEnemy enemy = health.GetComponent<BasicEnemy>();
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
