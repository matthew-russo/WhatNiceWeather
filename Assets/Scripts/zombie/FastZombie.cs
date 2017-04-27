using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastZombie : BaseZombie {

    protected override void Start ()
    {
        base.Start();
        health = Random.Range(2, 3) * GameManager.Instance.multiplier;
        speed = Random.Range(8,12) * GameManager.Instance.multiplier;
    }
}