using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerSector1 : EnemyControllerBase, IFlockingObject
{
    public float agroRadius;
    public AnimationCurve agroRadiusCurve;
    public float attackDist;
    public float fireInterval;
    public int fireBurstCount;
    public float maxTimeBetweenAttacks;

    [Header("Flocking")]
    public float alignmentWeight;
    public float cohesionWeight;
    public float separationWeight;
    public float playerWeight;
    public float radius;

    private float _elapsedSinceLastFire;
    private bool _battleStateAttack = false;
    private int _actualFireBurstCount = 0;
    private float _timeSinceLastAttack = 0f;
    private Vector3 _targetDirection = Vector3.zero;
    private bool _agro = false;
    private bool _attacking = false;

    // Use this for initialization
    protected override void Start ()
    {
		base.Start ();
        DoUpdate = true;
    }
	
	protected override void Update ()
    {
        if(_targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
            targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, 0f);
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        

       
        _force = this.transform.forward * thrusterForce;
        if (player != null)
        {
            Vector3 enemyToPlayer = player.transform.position - this.transform.position;

            if (_attacking)
            {
                _targetDirection = enemyToPlayer;
                _force = Vector3.zero;

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
                if (!_agro && enemyToPlayer.sqrMagnitude <= agroRadius * agroRadius)
                    _agro = true;

                if (_agro)
                {
                    playerWeight = agroRadiusCurve.Evaluate(enemyToPlayer.magnitude / agroRadius);
                }

                if (_timeSinceLastAttack >= maxTimeBetweenAttacks && enemyToPlayer.sqrMagnitude <= attackDist*attackDist)
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
        _agro = true;
		return base.TakeDamage (damage);      
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
