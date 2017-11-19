using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

	public GameObject mainParticleExhaust;
	public GameObject leftParticleExhaust;
	public GameObject rightParticleExhaust;
	ParticleSystem.MainModule me;
	ParticleSystem.MainModule le;
	ParticleSystem.MainModule re;

    // Use this for initialization
    void Start () {
		me =  mainParticleExhaust.GetComponent<ParticleSystem>().main;
		le =  leftParticleExhaust.GetComponent<ParticleSystem>().main;
		re =  rightParticleExhaust.GetComponent<ParticleSystem>().main;
    }

    private void FixedUpdate()
    {

		// Handle user presses 'w'
        if (Input.GetKey(KeyCode.W))
        {
            me.maxParticles = 1000;
            if (me.startLifetimeMultiplier <= 0.3f)
            {
                me.startLifetimeMultiplier += 0.008f;
            } 
        }
        if (!(Input.GetKey(KeyCode.W)) && me.startLifetimeMultiplier >= 0.05f)
        {
            
            if (me.startLifetimeMultiplier >= 0.2f)
            {
                // kill particles
                me.startLifetimeMultiplier = 0.16f;
            }

            me.startLifetimeMultiplier -= 0.0058f;
        }
        if (me.startLifetimeMultiplier < 0.06f) 
        {
            me.maxParticles = 0;
        }

		// Handle user presses 'a'
		if (Input.GetKey(KeyCode.A))
		{
			rightParticleExhaust.SetActive (true);
			re.startLifetimeMultiplier = 0.07f;
		}
		if (!(Input.GetKey (KeyCode.A)) && re.startLifetimeMultiplier >= 0.05f) {
			re.startLifetimeMultiplier -= 0.002f;

			if (re.startLifetimeMultiplier <= 0.051f) {
				rightParticleExhaust.SetActive (false);
			}
		}

		// Handle user presses 'd'
		if (Input.GetKey(KeyCode.D))
		{
			leftParticleExhaust.SetActive (true);
			le.startLifetimeMultiplier = 0.07f;
		}
		if (!(Input.GetKey (KeyCode.D)) && le.startLifetimeMultiplier >= 0.05f) {
			le.startLifetimeMultiplier -= 0.002f;
		
			if (le.startLifetimeMultiplier <= 0.051f) {
				leftParticleExhaust.SetActive (false);
			}
		}
	} // end FixedUpdate()
}
