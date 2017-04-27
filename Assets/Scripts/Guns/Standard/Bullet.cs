using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used to destroy bullets a given amount of time after fired
/// </summary>

public class Bullet : MonoBehaviour
{
    public float lifeTimer = 10f; // how long bullets last in game

	void Update ()
	{
	    lifeTimer -= Time.deltaTime;
	    if (lifeTimer <= 0)
	    {
	        Destroy(gameObject);
	    }
	}
}
