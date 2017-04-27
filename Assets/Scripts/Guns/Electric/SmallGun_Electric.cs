using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Smallest Electric Gun - Single Shot Electricty
/// </summary>

public class SmallGun_Electric : GunBase_Electric
{
    // Sets local variables
    //
    protected override void Start()
    {
        base.Start();
        hasAmmo = true;
        totalPossibleAmmo = 75;
        totalCurrentAmmo = totalPossibleAmmo;
        magazineAmmo = totalPossibleAmmo;
        currentAmmo = magazineAmmo;
    }
}
