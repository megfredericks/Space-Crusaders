using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Script to set Text variables on Building Info Panels
public class SetPanelInfo : MonoBehaviour {

	public Text BuildingName;
	public Text BuildingDesc;
	public Text WorkerText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Set Building Name
	void setName(string name){
		BuildingName.text = name;
	}

	void setDesc(string buildingType){
		if (buildingType == "food") {
			BuildingDesc.text = "Grow food to sustain your colony\\ in a safe, relatively eco friendly,\\ environment!";
		}
	}
}
