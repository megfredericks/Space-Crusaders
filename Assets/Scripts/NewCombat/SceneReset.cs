using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneReset : MonoBehaviour
{
    public Text resetText;
    private bool _activeReset = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(_activeReset)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Combat");
            }
        }
	}

    public void ResetActive()
    {
        resetText.gameObject.SetActive(true);
        _activeReset = true;
    }
}
