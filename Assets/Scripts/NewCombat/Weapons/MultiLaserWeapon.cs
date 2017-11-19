using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLaserWeapon : MonoBehaviour, IWeapon 
{
	public GameObject[] lasers;
	public float cooldown;
    public float energyToFire;

    private float _elapsedTime;
	
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
            foreach(GameObject l in lasers)
            {
                IWeapon w = l.GetComponent<MonoBehaviour>() as IWeapon;
                w.Fire();
            }
            _elapsedTime = 0f;
        }
	}

	public void StopFire()
	{
		// Does nothing for a basic laser
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
