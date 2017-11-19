using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutotialPanel1 : MonoBehaviour 
{
	public GameObject player;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
			
	}

	public void Close()
	{
		this.gameObject.SetActive (false);
		player.SendMessage ("Enable", true);
		player.SendMessage ("EnableMovement");
		player.SendMessage ("EnableBoost");
	}
}
