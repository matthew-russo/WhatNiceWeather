using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Base class for all guns that shoot standard bullets. Inherits from the master class -- "GunBase"
/// </summary>

public class GunBase_Standard : GunBase
{
    protected AudioClip shotSound;
    protected AudioClip reloadSound;
    protected AudioClip outOfAmmoSound;

    // Calls the GunBase's start method and also loads three audioClips
    //
    protected virtual void Start()
    {
        base.Start();
        shotSound = Resources.Load("Sounds/Guns/Pop") as AudioClip;
        reloadSound = Resources.Load("Sounds/Guns/Reload") as AudioClip;
        outOfAmmoSound = Resources.Load("Sounds/Error") as AudioClip;
    }

    protected virtual void Update ()
    {
        // Checks if the magazine is out of ammo but there is still total ammo left and reloads the gun, sets hasAmmo to false until reloading finishes
        //
	    if (currentAmmo <= 0 && hasAmmo)
	    {
	        hasAmmo = false;
	        StartCoroutine(Reload());
	    }

        // If the player presses the mouse button, checks if there is ammo.
        // If so, shoot, if not, play out of ammo sound.
        //
	    else if (Input.GetMouseButtonDown(0))
	    {
	        if (hasAmmo)
	        {
	            NormalShot();
	            currentAmmo--;
	        }
	        else
	        {
                AudioManager.Instance.PlayOneShot(outOfAmmoSound, .6f);
	        }
	    }

        // If there is no ammo left at all, set the crosshair to the "No Ammo Image"
        //
        else if (totalCurrentAmmo == 0 && currentAmmo == 0)
	    {
	        UINoAmmoImage.SetActive(true);
            UIReloadImage.SetActive(false);
            UICrosshairImage.SetActive(false);
	    }
    }

    protected void NormalShot()
    {   
        // Uses the Audio Manager to play a gun shot sound effect
        //
        AudioManager.Instance.PlayOneShot(shotSound, .3f);
        
        // Instantiates a new bullet, sets its parent to the gun and repositions it, adds a force to the rigidbody, and then removes the parent so it is not affected by player movement
        //
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.SetParent(gun.transform);
        newBullet.transform.localPosition = Vector3.forward * 2f;
        newBullet.GetComponent<Rigidbody>().AddForce(gun.transform.forward * 100f,ForceMode.Impulse);
        newBullet.transform.SetParent(null);
    }

    // Coroutine that changes the corsshair images to show reload times.
    //
    protected IEnumerator Reload()
    {
        if (totalCurrentAmmo >= magazineAmmo)
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
