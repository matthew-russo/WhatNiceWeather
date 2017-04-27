using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that handles updating the ammo UI with current ammo amounts
/// </summary>

public class AmmoUI : MonoBehaviour
{
    private Text _currentAmmoText;
    private Text _totalAmmoText;

    public GunBase gunScript;

	void Start ()
	{
	    _currentAmmoText = transform.GetChild(0).GetComponent<Text>();
	    _totalAmmoText = transform.GetChild(2).GetComponent<Text>();
	    _currentAmmoText.text = "0";
	    _totalAmmoText.text = "0";
	}
	
	void Update ()
	{
        // Checks to make sure player is holding a gun
        // If so, the two texts are updated with the current amount of ammo left in the magazine and the players total ammo with that weapon
	    if (gunScript != null)
	    {
            _currentAmmoText.text = gunScript.currentAmmo.ToString();
            _totalAmmoText.text = gunScript.totalCurrentAmmo.ToString();
        }

    }
}
