using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerTutorial2 : EnemyControllerBase
{
	public GameObject tutoralThing;
	public GameObject nextTutorialThing;
	public float triggerRadius;

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
		nextTutorialThing.SetActive (true);
	}
}