using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Large Standard Gun -- Big Bertha
/// </summary>

public class LargeGun_Standard : GunBase_Standard
{
    // Calls base start and initializes all local variables
    // Loads a different bullet than other standard guns and has higher damage
    //
    protected override void Start()
    {
        base.Start();

        damagePerHit = 5f;

        totalPossibleAmmo = 15;
        totalCurrentAmmo = totalPossibleAmmo;
        magazineAmmo = 1;
        reloadTime = 1.5f;

        hasAmmo = true;

        currentAmmo = magazineAmmo;
        bullet = Resources.Load("largeBullet") as GameObject;
    }
}
