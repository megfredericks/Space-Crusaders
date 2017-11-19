using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveWeapon : MonoBehaviour, IWeapon 
{
	public GameObject explosive;
	public float cooldown;
	public float energyToFire;

	private GameObject _explosiveClone;
	private float _elapsedTime;
	private AudioSource _audioSource;

	// Use this for initialization
	void Start () 
	{
		_audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () 
	{
		_elapsedTime += Time.deltaTime;
	}

	#region IWeapon Implementation

	public void Fire()
	{
		if (_elapsedTime >= cooldown)
		{
			_audioSource.Play();
			_elapsedTime = 0f;
			_explosiveClone = Instantiate(explosive, this.transform.position, Quaternion.identity);
			_explosiveClone.tag = this.tag;
			_explosiveClone.GetComponent<ExplosiveProjectile>().SetDirection (this.transform.forward);
		}
	}

	public void StopFire()
	{
		Destroy (_explosiveClone);
	}

	public bool IsFiring()
	{
		return false;
	}

	public float EnergyPerSecond()
	{
		return 0;
	}

	public float EnergyToFire()
	{
		return energyToFire;
	}

	#endregion
}
