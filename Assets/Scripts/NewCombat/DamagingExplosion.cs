using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingExplosion : MonoBehaviour, IDamaging {
	public float damage;
	AudioClip hitSound;
	AudioClip blockedSound;
	public float radius;
	public float time;

	private float _elapsedTime = 0.0f;
	private float _currentRadius = 0.0f;
	private SphereCollider _collider;

	// Use this for initialization
	void Start () {
		_collider = GetComponent<SphereCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		_elapsedTime += Time.deltaTime;
		_currentRadius = Mathf.Lerp (0.0f, radius, _elapsedTime / time);
		_collider.radius = _currentRadius;

		if (_elapsedTime > time)
			Destroy (this.gameObject);
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
