using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeapon : MonoBehaviour, IWeapon, IDamaging
{
    public float damagePerSecond;
    public float energyPerSecond;
    public AudioClip firingClip;
    public AudioClip hitClip;
    public AudioClip blockedClip;

    private CapsuleCollider _collider;
    private MeshRenderer _renderer;
    private bool _fireFlag = false;
    private AudioSource _firingAudioSource;
    private AudioSource _hitAudioSource;

    // Use this for initialization
    void Start ()
    {
        _collider = GetComponent<CapsuleCollider>();
        _renderer = GetComponent<MeshRenderer>();
        _firingAudioSource = AddAudio(firingClip, true, false, .3f);
        _hitAudioSource = AddAudio(hitClip, true, false, .5f);
	}

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;

        return newAudio;
    }

    #region IDamaging implementation

    public DamageType DamageType { get { return DamageType.OVER_TIME; } }

    public float DamageOverTime { get { return damagePerSecond; } }

    public float DamageInstantaneous { get { return 0.0f; } }

    public string IgnoreCollisionTag { get { return this.gameObject.tag; } }

    public void RegisterHit(bool blocked)
    {
        if(!_hitAudioSource.isPlaying)
            _hitAudioSource.Play();
    }

    public void UnregisterHit()
    {
        _hitAudioSource.Stop();
    }

    #endregion

    #region IWeapon implementation

    public void Fire()
    {
        _firingAudioSource.Play();
        _collider.enabled = true;
        _renderer.enabled = true;
        _fireFlag = true;
    }

    public void StopFire()
    {
        _firingAudioSource.Stop();
        _hitAudioSource.Stop();
        _collider.enabled = false;
        _renderer.enabled = false;
        _fireFlag = false;
    }

    public bool IsFiring()
    {
        return _fireFlag;
    }

    public float EnergyPerSecond()
    {
        return energyPerSecond;
    }

    public float EnergyToFire()
    {
        return 0;
    }

    #endregion
}
