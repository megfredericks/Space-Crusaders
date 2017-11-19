using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour {
    private Text resourceText;
	// Use this for initialization
	void Start () {
        resourceText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        resourceText.text = "Resource: " + GlobalController.Instance.resource.ToString();
	}
}
