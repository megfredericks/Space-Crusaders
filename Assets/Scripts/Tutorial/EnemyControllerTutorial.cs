using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerTutorial : EnemyControllerBase
{
    public float attackDist;
    public float fireInterval;
    public int fireBurstCount;
    public float maxTimeBetweenAttacks;
	public float triggerRadius;
	public GameObject tutoralThing;
	public GameObject nextShip;
	private bool _alreadyShown = false;

    private float _elapsedSinceLastFire;
    private int _actualFireBurstCount = 0;
    private float _timeSinceLastAttack = 0f;
    private Vector3 _targetDirection = Vector3.zero;
	private bool _enabled = false;

    // Use this for initialization
    protected override void Start ()
    {
		base.Start ();
    }
	public void Enable()
	{
		_enabled = true;
	}

	void OnDestroy()
	{
        if(nextShip != null)
		    nextShip.SetActive (true);
	}

	protected override void Update ()
    {
        if (player != null)
        {
            if (!_alreadyShown && (player.transform.position - this.transform.position).sqrMagnitude < triggerRadius * triggerRadius)
            {
                if (tutoralThing != null)
                    tutoralThing.SetActive(true);
                else
                    _enabled = true;
                _alreadyShown = true;
            }
        }


        if (_enabled) {
			
			Quaternion targetRotation = Quaternion.LookRotation (_targetDirection);
			targetRotation.eulerAngles = new Vector3 (0f, targetRotation.eulerAngles.y, 0f);
			this.transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			_force = Vector3.zero;

			if (player != null) {
				Vector3 enemyToPlayer = player.transform.position - this.transform.position;

				_targetDirection = enemyToPlayer;

				if (_elapsedSinceLastFire >= fireInterval) {
					_weapon.Fire ();
					_elapsedSinceLastFire = 0f;
					_actualFireBurstCount++;
				}
            
				if (_actualFireBurstCount >= fireBurstCount) {
					_timeSinceLastAttack = 0f;
					_actualFireBurstCount = 0;
				}
			}
			_elapsedSinceLastFire += Time.deltaTime;
			_timeSinceLastAttack += Time.deltaTime; 
		}
    }

    public override bool TakeDamage (float damage)
	{
        if(!_enabled)
        {
            return false;
        }
		return base.TakeDamage (damage);      
	}
}
