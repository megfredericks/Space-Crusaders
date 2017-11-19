using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
	public GameObject explosion;
	public float speed;
	private Vector3 _direction;

	public void SetDirection(Vector3 dir)
	{
		_direction = dir;
	}
	// Update is called once per frame
	void Update () 
	{
		this.transform.position += _direction * speed * Time.deltaTime;
	}

	void OnDestroy()
	{
		GameObject clone = Instantiate (explosion);
        Destroy(clone, 3);
	}
}
