using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPlayer : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(player == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, -player.transform.rotation.eulerAngles.y);
        }
	}
}
