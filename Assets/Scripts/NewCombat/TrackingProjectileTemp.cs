using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectileTemp : MonoBehaviour, IDamaging
{
    public float speed;
    public float damage;
    public GameObject player;
    public AudioClip hitSound;
    public AudioClip blockedSound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            this.transform.position += (player.transform.position-this.transform.position).normalized * speed * Time.deltaTime;
    }

    #region IDamaging implementation

    public DamageType DamageType { get { return DamageType.INSTANTANEOUS; } }

    public float DamageOverTime { get { return 0.0f; } }

    public float DamageInstantaneous { get { return damage; } }

    public string IgnoreCollisionTag { get { return this.gameObject.tag; } }

    public void RegisterHit(bool blocked)
    {
        if (!blocked)
            SoundManager.Instance.PlaySound(hitSound);
        else
            SoundManager.Instance.PlaySound(blockedSound);
        Destroy(this.gameObject);
    }

    public void UnregisterHit()
    {
        // Does nothing
    }


    #endregion
}
