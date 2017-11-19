using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
	void Fire ();
	void StopFire();
    bool IsFiring();
    float EnergyPerSecond();
    float EnergyToFire();
}
