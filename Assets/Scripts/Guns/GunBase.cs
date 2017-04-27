using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all guns.
/// </summary>

public class GunBase : MonoBehaviour
{
    public int totalPossibleAmmo; // The absolute total amount of ammo the gun can have
    public int totalCurrentAmmo;  //The amount of ammo the player currently has
    public int magazineAmmo; // How much ammo the magazine can hold at once
    public int currentAmmo; // How much ammo is currently loaded into the magazine

    protected float reloadTime; // How long it takes to reload

    public float damagePerHit; // How much damage the gun does per hit

    protected bool hasAmmo; // Bool used to monitor reloading 

    protected GameObject gun; 
    protected GameObject bullet;

    // References to the three crosshair images
    //
    public GameObject UICrosshairImage;
    public GameObject UIReloadImage;
    public GameObject UINoAmmoImage;

    // Bool that is used to determine if a player can pick up a gun in the level
    //
    public bool ableToBePickedUp = true;

    // Virtual start function so inheriting classes can override
    // Initializes common variables
    //
    protected virtual void Start()
    {
        gun = GameObject.FindGameObjectWithTag("currentGun");
        GameObject canvas = GameObject.FindGameObjectWithTag("GunUI");
        UICrosshairImage = canvas.transform.GetChild(0).gameObject;
        UIReloadImage = canvas.transform.GetChild(1).gameObject;
        UINoAmmoImage = canvas.transform.GetChild(2).gameObject;

        UICrosshairImage.SetActive(true);
        UIReloadImage.SetActive(false);
        UINoAmmoImage.SetActive(false);
    }
}
