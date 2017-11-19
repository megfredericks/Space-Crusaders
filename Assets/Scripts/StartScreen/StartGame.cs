using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StartGame : MonoBehaviour {

	public Button startButton;

	void OnMouseDown(){
		SceneManager.LoadSceneAsync ("CombatTutorial");
	}

	// Use this for initialization
	void Start () {
		Button btn = startButton.GetComponent<Button> ();
		btn.onClick.AddListener (LoadBase);
	}

	void LoadBase(){
		SceneManager.LoadSceneAsync ("CombatTutorial");
	}
		
}
