using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Medium Standard Gun -- Machine Gun
/// </summary>

public class MediumGun_Standard : GunBase_Standard
{
    private float fireRate = .1f; // used as timer to controll how fast the machine gun shoots

    // Calls base start function and then initializes all local variables
    //
    protected override void Start () {
        base.Start();

	    damagePerHit = 1f;

        totalPossibleAmmo = 336;
        totalCurrentAmmo = totalPossibleAmmo;
        magazineAmmo = 28;
        reloadTime = 2.5f;

        hasAmmo = true;

        currentAmmo = magazineAmmo;
        bullet = Resources.Load("bullet") as GameObject;
    }

    // Overrides the update method to call the NormalShot() function every fram the mouse is held down as opposed to mouse clicks.
    // Uses fireRate timer to control the speed of the shooting
    //
    protected override void Update ()
	{
        if (currentAmmo <= 0 && hasAmmo)
        {
            hasAmmo = false;
            StartCoroutine(Reload());
        }
        else if (Input.GetMouseButton(0))
        {
            if (hasAmmo)
            {
                if (fireRate <= 0)
                {
                    NormalShot();
                    currentAmmo--;
                    fireRate = .1f;
                }
                else
                {
                    fireRate -= Time.deltaTime;
                }
            }
            else
            {
                AudioManager.Instance.PlayOneShot(outOfAmmoSound, .6f);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            fireRate = .1f;
        }

        else if (totalCurrentAmmo == 0 && currentAmmo == 0)
        {
            UINoAmmoImage.SetActive(true);
            UIReloadImage.SetActive(false);
            UICrosshairImage.SetActive(false);
        }
    }
}
