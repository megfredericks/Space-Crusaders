using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
	public int HealthLevel { get; set; }
	public int ShieldLevel { get; set; }
    public int EnergyLevel { get; set; }
    public int BoostLevel { get; set; }
	public GameObject PrimaryWeapon { get; set; }
	public GameObject SecondaryWeapon { get; set; }
	public float CurrentHealth { get; set; }

	private static PlayerStats _instance;

	public static PlayerStats Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}
}
