// Random movement algorithm adapted from http://answers.unity3d.com/questions/23010/ai-wandering-script.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonistControllerScript : MonoBehaviour {

	private float speed = 5;
	private Vector3 waypoint;
	private int Range = 10;
	private float directionChange = 1;
	private float maxHeadingChange;
	private bool wandering;

	CharacterController controller;
	float heading;
	Vector3 targetRotation;

	void Awake () {
		controller = GetComponent<CharacterController> ();
		wandering = true;
	}

	// Use this for initialization
	void Start () {
		Wander ();
	}
	
	// Update is called once per frame
	void Update () {
		if (wandering) {
			transform.position += transform.TransformDirection (Vector3.forward) * speed * Time.deltaTime;
			if ((transform.position - waypoint).magnitude < 3) {
				Wander ();
			}
		}
	}

	void Wander () {
		waypoint = new Vector3 (Random.Range (transform.position.x - Range, transform.position.x + Range), 1, Random.Range (transform.position.z - Range, transform.position.z + Range));
		waypoint.y = 1;
		transform.LookAt (waypoint);
	} 
}



