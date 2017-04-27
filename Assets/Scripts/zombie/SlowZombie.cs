using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZombie : BaseZombie {

	protected override void Start ()
	{
        base.Start();
	    health = Random.Range(3, 5) * GameManager.Instance.multiplier + 1;
	    speed = Random.Range(3, 5) * GameManager.Instance.multiplier + 1;
	}
}
