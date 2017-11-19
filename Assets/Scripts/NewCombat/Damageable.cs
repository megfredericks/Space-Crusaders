using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private IDamageable _damageable;
	// Use this for initialization
	void Start ()
    {
	    foreach(MonoBehaviour m in GetComponents<MonoBehaviour>())
        {
            _damageable = m as IDamageable;
            if (_damageable != null)
                break;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        IDamaging damaging = GetIDamaging(other.gameObject);

        if (damaging != null && _damageable != null && this.gameObject.tag != damaging.IgnoreCollisionTag)
        {
            if (damaging.DamageType == DamageType.INSTANTANEOUS || damaging.DamageType == DamageType.BOTH)
            {
                bool blocked = _damageable.TakeDamage(damaging.DamageInstantaneous);
                damaging.RegisterHit(blocked);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        IDamaging damaging = GetIDamaging(other.gameObject);

        if (damaging != null && _damageable != null && this.gameObject.tag != damaging.IgnoreCollisionTag)
        {
            if (damaging.DamageType == DamageType.OVER_TIME || damaging.DamageType == DamageType.BOTH)
            {
                bool blocked =_damageable.TakeDamage(damaging.DamageOverTime * Time.deltaTime);
                damaging.RegisterHit(blocked);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamaging damaging = GetIDamaging(other.gameObject);

        if (damaging != null && _damageable != null && this.gameObject.tag != damaging.IgnoreCollisionTag)
        {            
             damaging.UnregisterHit();
        }
    }

    //private void OnCollisionEnter(Collision col)
    //{
    //    GameObject other = col.gameObject;
    //    IDamaging damaging = GetIDamaging(other);

    //    if (damaging != null && _damageable != null && this.gameObject.tag != damaging.IgnoreCollisionTag)
    //    {
    //        if (damaging.DamageType == DamageType.INSTANTANEOUS || damaging.DamageType == DamageType.BOTH)
    //        {
    //            bool blocked = _damageable.TakeDamage(damaging.DamageInstantaneous);
    //            damaging.RegisterHit(blocked);
    //        }
    //    }
    //}

    private IDamaging GetIDamaging(GameObject obj)
    {
        IDamaging d = null;

        foreach(MonoBehaviour b in obj.GetComponents<MonoBehaviour>())
        {
            d = b as IDamaging;

            if (d != null)
                break;
        }

        return d;
    }
}
