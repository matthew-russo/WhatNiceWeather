using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Script that lowers the final gun as the player gets kills
/// </summary>

public class UnlockFinalGun : MonoBehaviour {
    private float distance; // Original distance of originalHeight from destinationHeight
    private float originalHeight; // Gun's starting height
    private float destinationHeight = -1.5f; // Where we want the gun to end up
    private int requiredKills = 30; // How many kills before gun reaches destination height
    private float ratio; // Ratio of current kills vs required kills

    void Start()
    {
        originalHeight = transform.localPosition.y;
        distance = transform.localPosition.y - destinationHeight;
    }

    private void Update()
    {
        // If the player gets the required number of kills, destroy this script
        //
        if (GameManager.Instance.killCount >= requiredKills)
        {
            Destroy(this);
        }

        // Update the players current kill count
        ratio = (float)GameManager.Instance.killCount / (float)requiredKills;

        // Move the gun based upon above ratio
        transform.localPosition = new Vector3(transform.localPosition.x, originalHeight - (distance * ratio), transform.localPosition.z);
    }
}
