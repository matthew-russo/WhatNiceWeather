using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that handles player movement via Rigidbody forces
/// </summary>

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float maxSpeed = 15; // limit the max speed a player can go

	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {

        float horizontal = Input.GetAxis("Horizontal"); // Left and Right movement
        float vertical = Input.GetAxis("Vertical"); // Up and Down movement

        // Checks to make sure player is below maxSpeed before applying force
        //
	    if (_rigidbody.velocity.magnitude < maxSpeed)
	    {
            _rigidbody.AddForce(transform.forward * Time.deltaTime * vertical * 10000f);
            _rigidbody.AddForce(transform.right * Time.deltaTime * horizontal * 10000f);
        }
    }
}
