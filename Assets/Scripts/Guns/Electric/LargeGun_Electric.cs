using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Large Electric Gun -- Too Much Electricity
/// </summary>

public class LargeGun_Electric : GunBase_Electric
{
    // References to the 11 child gameobjects that will spawn lightning paths
    List<lightning_start> lightningScripts = new List<lightning_start>();

    // Initializes all local variables
    //
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < 11; i++)
        {
            lightningScripts.Add(transform.GetChild(i).GetComponent<lightning_start>());
        }
        hasAmmo = true;
        totalPossibleAmmo = 2000;
        damagePerHit = 4f;
        totalCurrentAmmo = totalPossibleAmmo;
        magazineAmmo = totalPossibleAmmo;
        currentAmmo = magazineAmmo;
    }

    // Overrides update function to 
    //     A) Change GetMouseButtonDown to GetMouseButton
    //     B) Add PowerShot Function
    //     Note NormalShot is just here so that enemies will die.
    //
    protected override void Update()
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
                NormalShot();
                PowerShot();
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


    // Purely visual, this doesn't actually hurt enemies, that is done via NormalShot in GunBase_Electric and should kill enemies very quickly due to the damage set in this script
    // Creates 11 lightning chains from children gameobjects to the gun
    // so much electricity
    //
    private void PowerShot()
    {
        foreach (lightning_start item in lightningScripts)
        {
            item.ZapTarget(this.gameObject);
        }

        AudioManager.Instance.PlayOneShot(_eletrictShotAudio, .6f);
    }
}
