using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSkip : MonoBehaviour {

    KeyCode[] codes = { KeyCode.P, KeyCode.A, KeyCode.S, KeyCode.S };
    int index = 0;
    public string nextScene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (index < 4 && Input.GetKeyDown(codes[index]))
        {
            index++;
            if (index == 4)
                SceneManager.LoadScene(nextScene);

        }
	}
}
