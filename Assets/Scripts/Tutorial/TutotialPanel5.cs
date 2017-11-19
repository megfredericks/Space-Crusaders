using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutotialPanel5 : MonoBehaviour 
{
	public GameObject player;
    public GameObject squad;
	public void OnEnable()
	{
		player.SendMessage ("Enable", false);
        squad.SetActive(true);
	}

	public void Close()
	{
		this.gameObject.SetActive (false);
		player.SendMessage ("Enable", true);
        player.SendMessage("CanDie");
        squad.SendMessage("Enable");
    }
}
