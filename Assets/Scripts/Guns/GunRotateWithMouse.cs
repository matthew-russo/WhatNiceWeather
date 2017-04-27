using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// As player looks up and down via mouse input, gun rotates up and down
/// </summary>

public class GunRotateWithMouse : MonoBehaviour {
    private float upDownLook = 0f; // Total mouse movement
    private float xOffset; //Gun's original Y rotation
    private float positionOffset; // Gun's original postion

    private void Start()
    {
        xOffset = transform.localEulerAngles.y;
        positionOffset = transform.localPosition.y;
    }

    void FixedUpdate () {
        // Get mouse input, track overall mouse delta in variable upDownLook
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * 150f; // mouseDelta per frame
        upDownLook -= mouseY;
        upDownLook = Mathf.Clamp(upDownLook, -80f + xOffset, 80f + xOffset);

        // Rotate the gun based upon where player is looking
        transform.localEulerAngles = new Vector3(upDownLook, 0f, 0f);

        // Slightly move the gun up or down depending on where the player is looking
        transform.localPosition = new Vector3(transform.localPosition.x, positionOffset + (-upDownLook/80), transform.localPosition.z);
    }
}
