using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createToolTip : MonoBehaviour {

	// Use this for initialization
	void Start () {
      
	}

    void onGUI() {
        GUI.Button(new Rect(10, 10, 100, 20), new GUIContent("Tooltip?", "First upgrade in research tree."));
        GUI.Label(new Rect(10, 40, 100, 40), GUI.tooltip);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
