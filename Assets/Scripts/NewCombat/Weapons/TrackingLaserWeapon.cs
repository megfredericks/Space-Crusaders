using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingLaserWeapon : MonoBehaviour, IWeapon
{
    public GameObject trackingLaser;
    public GameObject player;
    public float cooldown;
    public float energyToFire;
    public float timeToDestroy = 10f;

    private float _elapsedTime;
    private AudioSource _audioSource;

    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
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
            Vector3 rotation = transform.rotation.eulerAngles;
            GameObject l = Instantiate(trackingLaser, this.transform.position, Quaternion.Euler(rotation));
            l.GetComponent<TrackingProjectileTemp>().player = player;
            l.tag = this.tag;
            Destroy(l, timeToDestroy);
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
