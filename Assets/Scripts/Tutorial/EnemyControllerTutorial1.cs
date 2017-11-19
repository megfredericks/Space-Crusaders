using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerTutorial1 : EnemyControllerBase
{
	public float triggerRadius;
	public GameObject tutoralThing;
	public GameObject nextShip;
	private bool _alreadyShown = false;

	protected override void Update ()
	{
        if (player != null)
        {
            if (!_alreadyShown && (player.transform.position - this.transform.position).sqrMagnitude < triggerRadius * triggerRadius)
            {
                tutoralThing.SetActive(true);
                _alreadyShown = true;
            }
        }
	}

	void OnDestroy()
	{
		nextShip.SetActive (true);
	}
}