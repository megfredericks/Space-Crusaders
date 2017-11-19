using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class WaitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Wait ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Wait() {
		yield return new WaitForSeconds(8);
		SceneManager.LoadSceneAsync ("Base");
	}
}
