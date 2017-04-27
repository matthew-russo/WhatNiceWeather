using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that rotates objects around the y-axis
/// </summary>

public class RotateInPlace : MonoBehaviour {

	void Start ()
	{
	    StartCoroutine(Rotate());
	}

    // Coroutine that rotates an object 1 degree around the y-axis every frame
    //
    private IEnumerator Rotate()
    {
        while (true)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 1f, transform.localEulerAngles.z);
            yield return new WaitForEndOfFrame();
        }
    }
}
