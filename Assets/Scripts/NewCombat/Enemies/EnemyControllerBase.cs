using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControllerBase : MonoBehaviour, IDamageable
{
	public float maxHealth;
	public GameObject weapon;
	public GameObject player;
	public float rotationSpeed;
	public float topSpeed;
	public float thrusterForce;
	public Level level;
	public GameObject deathExplosionParticle;
	public GameObject hitExplosionParticle;
	public AudioClip deathAudio;
	public AudioClip hitAudio;
    public Text enemyHealthText;
    public Slider enemyHealthSlider;
    public int resourceValue;

	protected Rigidbody _rgidBdy;
	protected Vector3 _force;
	protected IWeapon _weapon;
    protected float _currentHealth;

	// Use this for initialization
	protected virtual void Start ()
	{
        _currentHealth = maxHealth;
		_rgidBdy = GetComponent<Rigidbody>();
		_weapon = weapon.GetComponent<MonoBehaviour>() as IWeapon;
		level.RegisterShip(this.gameObject);
	}

	// Update is called once per frame
	protected virtual void Update ()
	{
		// AI goes here
	}

	protected virtual void FixedUpdate()
	{
		_rgidBdy.AddForce(_force);
		_rgidBdy.velocity = Vector3.ClampMagnitude(_rgidBdy.velocity, topSpeed);
		_rgidBdy.angularVelocity = Vector3.zero;
	}

    public void OnDestroy()
    {
        GlobalController.Instance.resource += resourceValue;
    }

    #region IDamageable implementation

    public virtual bool TakeDamage(float damage)
	{
		_currentHealth -= damage;
        enemyHealthText.gameObject.SetActive(true);
        enemyHealthSlider.gameObject.SetActive(true);
        enemyHealthSlider.value = (_currentHealth / maxHealth) * 100f;

		if (_currentHealth <= 0f)
		{
            enemyHealthSlider.gameObject.SetActive(false);
            enemyHealthText.gameObject.SetActive(false);
			ParticleSystem deathExplosionParticleClone = Instantiate(deathExplosionParticle,transform.position, transform.rotation).GetComponent<ParticleSystem>();
			level.DeleteShip(this.gameObject);
			EnemySoundManager.Instance.playEnemyExplosionSound();
			Destroy(this.gameObject);
			Destroy(deathExplosionParticleClone.gameObject, deathExplosionParticleClone.main.duration);
		}
		else
		{
			ParticleSystem hitExplosionParticleClone = Instantiate(hitExplosionParticle,transform.position, transform.rotation).GetComponent<ParticleSystem>();
			Destroy(hitExplosionParticleClone.gameObject, hitExplosionParticleClone.main.duration);
			SoundManager.Instance.PlaySound (hitAudio);
		}

        return false;
	}

	#endregion
}
