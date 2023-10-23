using System;
using System.Collections;
using System.Collections.Generic;
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
                turret.onHit += (enemy) =>
                {
                    if (enemy == null) return;
                    FireEffect fireEffect = new(enemy, 2f, 2);
                    enemy.AddEffect(fireEffect);
                };
                break;
            
            case UpgradeType.IceTipped:
                turret.onHit += (enemy) =>
                {
                    if (enemy == null) return;
                    IceEffect iceEffect = new(enemy, 2f);
                    enemy.AddEffect(iceEffect);
                };
                break;

            case UpgradeType.AddDamage:
                turret.UpgradeDamage(10);
                break;

            case UpgradeType.AddFirerate:
                turret.UpgradeFirerate(10f);
                break;

            case UpgradeType.AddRange:
                turret.UpgradeRange(10f);
                break;
        }
    }
}
