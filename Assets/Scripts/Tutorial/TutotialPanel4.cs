using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutotialPanel4 : MonoBehaviour 
{
	public GameObject player;

	public void OnEnable()
	{
		player.SendMessage ("Enable", false);
	}

	public void Close()
	{
		this.gameObject.SetActive (false);
		player.SendMessage ("Enable", true);
	}
}
