using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NO SHAME -- I TOOK THIS FROM THE INTERWEBS BUT I UNDERSTAND HOW IT WORKS
/// Script that sends out multiple raycasts in an angle to detect if enemies are in front of player
/// </summary>

public class DetectEnemies : MonoBehaviour
{
    // Reference to gun scripts to tell them whether or not there is a target
    //
    public GameObject _currentGun;
    public lightning_start _lightningScript;
    public GunBase_Electric _lightningGunScript;

    private Quaternion startingAngle = Quaternion.AngleAxis(-30, Vector3.up);   // the angle at which the raycasts will start
    private Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);   // the angle between each raycast

    BaseZombie enemy;

    private float timer = .2f; // How often to check for enemies

    // Initially set to false, when an electric weapon is picked up, this will be turned on
    //
    private void Start()
    {
        this.enabled = false;
    }

    // Only checks for enemies when timer runs out
    // This is just so we don't run raycasts every frame as we don't need it done that quickly
    //
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            DetectThings();
            timer = .2f;
        }
    }

    void DetectThings()
    {
        RaycastHit hit;
        Quaternion angle = transform.rotation * startingAngle;
        Vector3 direction = angle * Vector3.forward;
        Vector3 pos = transform.position;

        // Sends out a given number of rays. Current it is 12 rays at 5 degrees between rays so a 60 degree field of view in front of the player
        //
        for (int i = 0; i < 12; i++)
        {
            // If the raycast hits something
            //
            if (Physics.Raycast(pos, direction, out hit, 25))
            {
                // If the thing it hit is a zombie and the zombie is not currently dying, it is the next target for the lightning gun.
                //
                enemy = hit.collider.GetComponent<BaseZombie>();
                if (enemy != null && !enemy.isDying)
                {
                    _lightningScript.target.transform.position = new Vector3(enemy.gameObject.transform.position.x, _lightningScript.target.transform.position.y, enemy.gameObject.transform.position.z);
                    _lightningGunScript.enemyTarget = enemy.gameObject;
                }
            }

            direction = stepAngle * direction;
        }
    }
}
