using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutotialPanel3 : MonoBehaviour 
{
	public GameObject player;
	public GameObject enemy;

	public void OnEnable()
	{
		player.SendMessage ("Enable", false);
	}

	public void Close()
	{
		this.gameObject.SetActive (false);
		player.SendMessage ("Enable", true);
		enemy.SendMessage ("Enable");
		player.SendMessage ("EnableShield");
	}
}
