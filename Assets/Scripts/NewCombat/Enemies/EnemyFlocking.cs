using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlocking : MonoBehaviour
{
    public Level level;
    public GameObject player;
	
	// Update is called once per frame
	void Update ()
    {
        List<GameObject> enemies = level.GetEnemies();

        foreach(GameObject e in enemies)
        {
            IFlockingObject enemy = null;

            foreach(MonoBehaviour b in e.GetComponents<MonoBehaviour>())
            {
                enemy = b as IFlockingObject;
                if (enemy != null)
                    break;
            }

            if (enemy == null || !enemy.DoUpdate)
                continue;

            Vector3 sep = Vector3.zero;
            Vector3 coh = Vector3.zero;
            Vector3 ali = Vector3.zero;
            CalculateProperties(ref sep, ref coh, ref ali, enemies, e, enemy.Radius);

			if (player) {
				Vector3 dest = player.transform.position - e.transform.position;
				enemy.UpdateDirection(((sep * enemy.SeparationWeight + coh * enemy.CohesionWeight + ali * enemy.AlignmentWeight + dest * enemy.PlayerWeight) * .25f).normalized);
			}
            
        }
	}

    private void CalculateProperties(ref Vector3 sep, ref Vector3 coh, ref Vector3 ali, List<GameObject> enemies, GameObject enemy, float radius)
    {
        int count = 0;
        foreach(GameObject e in enemies)
        {
            if (e != enemy)
            {
                Vector3 diff = enemy.transform.position - e.transform.position;
                if (diff.sqrMagnitude < radius * radius)
                {
                    count++;
                    ali += e.transform.forward;
                    coh += e.transform.position;
                    sep += diff * (1.0f / diff.magnitude);
                }            
            }
        }
        if(count > 0)
        {
            ali /= count;
            coh = (coh / count) - enemy.transform.position;
            sep /= count;
        }
    }
}
