using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TravelButtonScript : MonoBehaviour {

	public Button travelButton;

	// Use this for initialization
	void Start () {
		Button btn = travelButton.GetComponent<Button> ();
		btn.onClick.AddListener (loadLevel);
	}
	
	void loadLevel(){
        SceneManager.LoadSceneAsync("CombatSector1");
	}
}
