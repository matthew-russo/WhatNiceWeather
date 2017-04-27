using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Medium Electric Gun -- The Machine Gun of Electricity
/// </summary>

public class MediumGun_Electric : GunBase_Electric
{
    // Sets local variables
    //
    protected override void Start()
    {
        base.Start();
        hasAmmo = true;
        totalPossibleAmmo = 500;
        damagePerHit = .03f;
        totalCurrentAmmo = totalPossibleAmmo;
        magazineAmmo = totalPossibleAmmo;
        currentAmmo = magazineAmmo;
    }

    // Overrides update function to fire electricity every frame mouse is held down rather than every click
    //
    protected override void Update () {
        if (currentAmmo <= 0 && hasAmmo)
        {
            hasAmmo = false;
            StartCoroutine(Reload());
        }
        else if (Input.GetMouseButton(0))
        {
            if (hasAmmo)
            {
                NormalShot();
                currentAmmo--;

                // Move this somewhere else
                if (enemyTarget != null)
                {
                    enemyTarget.GetComponent<BaseZombie>().TakeDamage(1f);
                }
            }

            else
            {
                AudioManager.Instance.PlayOneShot(outOfAmmoSound, .6f);
            }
        }

        else if (totalCurrentAmmo == 0 && currentAmmo == 0)
        {
            UINoAmmoImage.SetActive(true);
            UIReloadImage.SetActive(false);
            UICrosshairImage.SetActive(false);
        }
    }
}
