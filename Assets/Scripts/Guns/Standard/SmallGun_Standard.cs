using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Small standard gun script -- closest to player spawn
/// </summary>

public class SmallGun_Standard : GunBase_Standard
{
    // Calls base start function and then initializes all variables specific to this gun
    //
	protected override void Start ()
	{
        base.Start();

	    damagePerHit = 1.5f;

        totalPossibleAmmo = 180;
        totalCurrentAmmo = totalPossibleAmmo;
        magazineAmmo = 12;
	    reloadTime = 1.5f;

	    hasAmmo = true;
         
	    currentAmmo = magazineAmmo;
	    bullet = Resources.Load("bullet") as GameObject;
	}
}
