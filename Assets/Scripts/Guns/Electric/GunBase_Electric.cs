using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Base class for the 3 electric guns
/// </summary>

public class GunBase_Electric : GunBase
{
    // References to scripts that generate the lightning effect
    //
    protected lightning_start _lightningScript;
    protected GameObject _lightningTarget;
    public GameObject enemyTarget;
    
    // Script on player that raycasts and detects enemies in front of them.
    public DetectEnemies detectEnemiesScript;

    protected AudioClip _eletrictShotAudio;
    protected AudioClip reloadSound;
    protected AudioClip outOfAmmoSound;

    // Initializes common variables
    //
    protected virtual void Start()
    {
        base.Start();
        _lightningScript = GetComponent<lightning_start>();
        _lightningTarget = transform.GetChild(0).gameObject;
        _lightningScript.target = _lightningTarget;
        _lightningScript.targetOriginPosition = _lightningTarget.transform.position;
        _eletrictShotAudio = Resources.Load("Sounds/Guns/Electricity") as AudioClip;
        reloadSound = Resources.Load("Sounds/Guns/Reload") as AudioClip;
        outOfAmmoSound = Resources.Load("Sounds/Error") as AudioClip;
    }

    // Basic gameplay loop of electric guns.
    // Checks if there is ammo, checks if mouse has been pressed & shoots, and enables/disables crosshairs
    //
    protected virtual void Update()
    {
        if (currentAmmo <= 0 && hasAmmo)
        {
            hasAmmo = false;
            StartCoroutine(Reload());
        }
        else if (Input.GetMouseButtonDown(0) && hasAmmo)
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
                AudioManager.Instance.PlayOneShot(outOfAmmoSound,.6f);
            }
        }

        else if (totalCurrentAmmo == 0 && currentAmmo == 0)
        {
            UINoAmmoImage.SetActive(true);
            UIReloadImage.SetActive(false);
            UICrosshairImage.SetActive(false);
        }
    }

    // Calls the lightning zap function on the given gameobject, assigned in the DetectEnemies script
    //
    protected void NormalShot()
    {
        _lightningScript.ZapTarget(_lightningTarget);
        AudioManager.Instance.PlayOneShot(_eletrictShotAudio, .6f);
    }

    // Reload Coroutine that animates the crosshair
    //
    protected IEnumerator Reload()
    {
        if (totalCurrentAmmo >= 12)
        {
            UICrosshairImage.SetActive(false);
            UIReloadImage.SetActive(true);
            float currentReloadTimer = reloadTime;
            while (currentReloadTimer > 0)
            {
                float ratio = currentReloadTimer / reloadTime;
                currentReloadTimer -= Time.deltaTime;
                UIReloadImage.GetComponent<Image>().fillAmount = ratio;
                yield return new WaitForEndOfFrame();
            }
            totalCurrentAmmo -= magazineAmmo;
            currentAmmo = magazineAmmo;
            hasAmmo = true;
            UIReloadImage.SetActive(false);
            UICrosshairImage.SetActive(true);
            AudioManager.Instance.PlayOneShot(reloadSound, .4f);
        }
        else
        {
            AudioManager.Instance.PlayOneShot(outOfAmmoSound, .6f);
        }
    }
}
