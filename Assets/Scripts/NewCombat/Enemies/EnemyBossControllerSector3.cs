using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossControllerSector3 : EnemyControllerBase//, IFlockingObject
{
    public float maxDist;
    public float attackDist;
    public float fireInterval;
    public int fireBurstCount;
    public float maxTimeBetweenAttacks;
    public GameObject shield;
    public float maxShieldHealth;
    public float shieldRechargeTime;
    public GameObject shieldHitParticle;
    public Text enemyShieldText;
    public Slider enemyShieldSlider;

    [Header("Flocking")]
    public float alignmentWeight;
    public float cohesionWeight;
    public float separationWeight;
    public float playerWeight;
    public float radius;

    private float _elapsedSinceLastFire;
    private int _actualFireBurstCount = 0;
    private float _timeSinceLastAttack = 0f;
    private Vector3 _targetDirection = Vector3.zero;
    private bool _attacking = false;
    private float _accelerationPower;
    private float _currentShieldHealth;
    private float _shieldReactivateTimer;

    // Use this for initialization
    protected override void Start ()
    {
		base.Start ();
        _currentShieldHealth = maxShieldHealth;
        DoUpdate = true;
    }
	
	protected override void Update ()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
        targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, 0f);
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (!shield.activeSelf && _shieldReactivateTimer >= shieldRechargeTime)
        {
            shield.SetActive(true);
            _currentShieldHealth = maxShieldHealth;
        }

        _force = this.transform.forward * thrusterForce;
        if (player != null)
        {       
            Vector3 enemyToPlayer = player.transform.position - this.transform.position;
            _targetDirection = enemyToPlayer;



            if (enemyToPlayer.magnitude < maxDist)
                _accelerationPower = 0f;
            else
                _accelerationPower = 1f;

            _force *= _accelerationPower;

            if (Vector3.Dot(enemyToPlayer.normalized, player.transform.forward) <= -.8f)
            {
                _force = Vector3.Cross(this.transform.forward, Vector3.down) * thrusterForce * 1.5f;
            }

            if (_attacking)
            {                
                if(_elapsedSinceLastFire >= fireInterval)
                {
                    _weapon.Fire();
                    _elapsedSinceLastFire = 0f;
                    _actualFireBurstCount++;
                }
                
                if(_actualFireBurstCount >= fireBurstCount)
                {
                    _attacking = false;
                    DoUpdate = true;
                    _timeSinceLastAttack = 0f;
                    _actualFireBurstCount = 0;
                }
            }
            else
            {               
                if (_timeSinceLastAttack >= maxTimeBetweenAttacks)
                {
                    _attacking = true;
                    DoUpdate = false;
                }
            }
        }
        _elapsedSinceLastFire += Time.deltaTime;
        _timeSinceLastAttack += Time.deltaTime;    
    }

    public override bool TakeDamage (float damage)
	{
        bool blocked;
        enemyHealthText.gameObject.SetActive(true);
        enemyHealthSlider.gameObject.SetActive(true);
        enemyShieldText.gameObject.SetActive(true);
        enemyShieldSlider.gameObject.SetActive(true);


        if (shield.activeSelf)
        {
            if (damage >= _currentShieldHealth)
            {
                shield.SetActive(false);
                _shieldReactivateTimer = 0f;
                enemyShieldSlider.value = 0;
                _currentShieldHealth = 0;
            }
            else
            {
                _currentShieldHealth -= damage;
                GameObject shieldHitParticleClone = Instantiate(shieldHitParticle, transform.position, transform.rotation);
                Destroy(shieldHitParticleClone, 2.0f);
                enemyShieldSlider.value = (_currentShieldHealth / maxShieldHealth) * 100f;
                blocked = true;
            }
            blocked = true;
        }
        else
        {
            _currentHealth -= damage;
            enemyHealthSlider.value = (_currentHealth / maxHealth) * 100f;

            if (_currentHealth <= 0f)
            {
                enemyHealthSlider.gameObject.SetActive(false);
                enemyHealthText.gameObject.SetActive(false);
                enemyShieldSlider.gameObject.SetActive(false);
                enemyShieldText.gameObject.SetActive(false);
                ParticleSystem deathExplosionParticleClone = Instantiate(deathExplosionParticle, transform.position, transform.rotation).GetComponent<ParticleSystem>();
                level.DeleteShip(this.gameObject);
                EnemySoundManager.Instance.playEnemyExplosionSound();
                Destroy(this.gameObject);
                Destroy(deathExplosionParticleClone.gameObject, deathExplosionParticleClone.main.duration);
            }
            else
            {
                ParticleSystem hitExplosionParticleClone = Instantiate(hitExplosionParticle, transform.position, transform.rotation).GetComponent<ParticleSystem>();
                Destroy(hitExplosionParticleClone.gameObject, hitExplosionParticleClone.main.duration);
                SoundManager.Instance.PlaySound(hitAudio);
            }
            blocked = false;
        }
        return blocked;
    }

    #region IFlockingObject Implementation

    public float AlignmentWeight { get { return alignmentWeight; } set { alignmentWeight = value; } }
    public float CohesionWeight { get { return cohesionWeight; } set { cohesionWeight = value; } }
    public float SeparationWeight { get { return separationWeight; } set { separationWeight = value; } }
    public float PlayerWeight { get { return playerWeight; } set { playerWeight = value; } }
    public bool DoUpdate { get; set; }
    public float Radius { get { return radius; } set { radius = value; } }

    public void UpdateDirection(Vector3 direction)
    {
        _targetDirection = direction;
    }

    #endregion
}
