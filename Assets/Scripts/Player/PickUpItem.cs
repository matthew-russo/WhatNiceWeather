using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If the player runs into a gun, pick it up and properly set it up for use
/// If the player runs into ammo, refill ammo
/// </summary>

public class PickUpItem : MonoBehaviour
{
    private GameObject gunContainer;
    private DetectEnemies _detectEnemiesScript;
    public AmmoUI ammoUIScript;

    private AudioClip _reloadSound;

    void Start()
    {
        gunContainer = GameObject.FindGameObjectWithTag("currentGun");
        _detectEnemiesScript = GameObject.FindGameObjectWithTag("Player").GetComponent<DetectEnemies>();
        _reloadSound = Resources.Load("Sounds/Guns/Reload") as AudioClip;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gun")
        {
            Drop();
            PickUp(other.gameObject);
        }
        if (other.tag == "Ammo")
        {
            TakeAmmo(other.gameObject);
        }
    }

    // Takes the gun that was just collided with and sets it into the player gun slot.
    // Enables the proper gun scripts and if gun is electric, begin raycasting for enemies
    //
    void PickUp(GameObject Gun)
    {
        Gun.transform.SetParent(gunContainer.transform);
        Gun.transform.localPosition = Vector3.zero;
        Gun.transform.localEulerAngles = Vector3.zero;
        Gun.transform.localScale = Vector3.one * .1f;

        Destroy(Gun.GetComponent<RotateInPlace>());

        Gun.GetComponent<GunBase>().enabled = true;

        Gun.tag = "currentGun";

        if (Gun.name.Contains("electric"))
        {
            _detectEnemiesScript.enabled = true;
            _detectEnemiesScript._currentGun = Gun;
            _detectEnemiesScript._lightningScript = Gun.GetComponent<lightning_start>();
            _detectEnemiesScript._lightningGunScript = Gun.GetComponent<GunBase_Electric>();
        }
        else
        {
            _detectEnemiesScript.enabled = false;
        }

        ammoUIScript.gunScript = Gun.GetComponent<GunBase>();
    }

    // Drops the gun that is currently held, attaches the RotateInPlace script and turns off the gun scripts on the object.
    // Triggers a coroutine that disables the box collider for a few seconds so player doesn't immediately pick up the gun they just dropped
    //
    void Drop()
    {
        if (gunContainer.transform.childCount == 0)
        {
            return;
        }
        else
        {
            GameObject gunCurrentlyHeld = gunContainer.transform.GetChild(0).gameObject;
            gunCurrentlyHeld.transform.parent = null;
            gunCurrentlyHeld.transform.localEulerAngles = Vector3.zero;

            gunCurrentlyHeld.AddComponent<RotateInPlace>();
            
            gunCurrentlyHeld.GetComponent<GunBase>().enabled = false;
            gunCurrentlyHeld.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(GunPickUpCooldown(gunCurrentlyHeld.GetComponent<BoxCollider>()));

            gunCurrentlyHeld.tag = "Gun";
        }
    }

    // Coroutine that disables collider for a couple seconds so that player does not immediately pick up gun they just dropped
    //
    private IEnumerator GunPickUpCooldown(BoxCollider gun)
    {
        float timer = 2f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gun.enabled = true;
    }

    // If player runs into ammo and has a gun currently, refill ammo and destroy ammo box
    // If the player doesn't have a gun, do nothing
    //
    private void TakeAmmo(GameObject ammoItem)
    {
        if (gunContainer.transform.childCount == 0)
        {
            return;
        }
        else
        {
            Destroy(ammoItem);
            GunBase gunscript = gunContainer.GetComponentInChildren<GunBase>();
            gunscript.currentAmmo = gunscript.magazineAmmo;
            gunscript.totalCurrentAmmo = gunscript.totalPossibleAmmo;
            AudioManager.Instance.PlayOneShot(_reloadSound, .4f);
        }

    }
}
