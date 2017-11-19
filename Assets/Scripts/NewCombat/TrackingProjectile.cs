using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : EnemyControllerBase, IDamaging
{
    public float damage;
    public AudioClip blockedAudio;

    // Use this for initialization
    protected override void Start()
    {
        _currentHealth = maxHealth;
        _rgidBdy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        _force = (player.transform.position-this.transform.position).normalized*thrusterForce;
    }

    #region IDamaging implementation

    public DamageType DamageType { get { return DamageType.INSTANTANEOUS; } }

    public float DamageOverTime { get { return 0.0f; } }

    public float DamageInstantaneous { get { return damage; } }

    public string IgnoreCollisionTag { get { return this.gameObject.tag; } }

    public void RegisterHit(bool blocked)
    {
        if (!blocked)
            SoundManager.Instance.PlaySound(hitAudio);
        else
            SoundManager.Instance.PlaySound(blockedAudio);
        Destroy(this.gameObject);
    }

    public void UnregisterHit()
    {
        // Does nothing
    }


    #endregion
}
