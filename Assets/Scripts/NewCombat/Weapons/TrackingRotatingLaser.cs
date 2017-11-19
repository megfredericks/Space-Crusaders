using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingRotatingLaser : MonoBehaviour
{
    public GameObject laser;
    public float cooldown;
    public float energyToFire;
    public float timeToDestroy = 3f;
    public float rotationRate;
    public float force;
    public GameObject player;

    private float _elapsedTime;
    private AudioSource _audioSource;
    private Vector3 _force;
    private Rigidbody _rgidBdy;
    private Vector3 _rotation;

    // Use this for initialization
    void Start()
    {
        _rgidBdy = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _rotation = new Vector3(0f, rotationRate, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            _force = (player.transform.position - this.transform.position).normalized * force;
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= cooldown)
            {
                Fire();
            }
        }
    }

    private void FixedUpdate()
    {
        _rgidBdy.AddForce(_force);
        _rgidBdy.angularVelocity = _rotation;
    }

    #region IWeapon Implementation

    public void Fire()
    {
        if (_elapsedTime >= cooldown)
        {
            _audioSource.Play();
            _elapsedTime = 0f;
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.x = 90f;
            GameObject l = Instantiate(laser, this.transform.position, Quaternion.Euler(rotation));
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