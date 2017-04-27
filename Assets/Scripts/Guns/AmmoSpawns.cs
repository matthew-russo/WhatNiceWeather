using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the spawning of ammo boxes
/// </summary>

public class AmmoSpawns : MonoBehaviour
{
    public GameObject ammoPrefab;
    private float spawnTimer = 25f;
    private List<GameObject> spawnLocations = new List<GameObject>();

    // Get references to all spawn locations
    //
	void Start () {
	    foreach (Transform child in transform)
	    {
	        spawnLocations.Add(child.gameObject);
	    }
	}
	
    // Spawn ammo box when timer hits 0. If an ammo box already exists at the randomly chosen spawn location, no ammo is spawned
    //
	void Update () {
	    if (spawnTimer > 0)
	    {
	        spawnTimer -= Time.deltaTime;
	    }

	    else
	    {
	        GameObject spawnLoc = spawnLocations[Random.Range(0, spawnLocations.Count)];
	        if (spawnLoc.transform.childCount == 0)
	        {
                GameObject newAmmo = Instantiate(ammoPrefab, spawnLoc.transform);
                newAmmo.transform.localPosition = new Vector3(0f,1f,0f);
                newAmmo.transform.localEulerAngles = new Vector3(-90f,0f,0f);
            }
	        spawnTimer = 25f;
	    }
	}
}
