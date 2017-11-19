using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class OpenMenu : MonoBehaviour {

	public GameObject panel;
    public GameObject self;
    public GameObject GM;
    BaseManager bm;
    public AudioClip openPanelClip;

    // Use this for initialization
    void Start () {
		bm = GameObject.Find("GameManager").GetComponent<BaseManager>();
    }

    private void Update()
    {
        //Debug.Log("panel: " + panel.activeSelf + " , menuOpen: " + GlobalController.Instance.menuOpen
        //    + " , selected building: " + GlobalController.Instance.selectedBuildSite + " , material: " + GlobalController.Instance.highlightedMaterial);
    }

    void OnMouseUp()
    {
		// The user is clicking on an open menu, so we don't want to change any of this stuff, which
		// prevents buildings from being built in the wrong spot.
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}

        // if building panel isn't open and there isn't another panel already open
		if (!panel.activeSelf && !GlobalController.Instance.menuOpen) {
            
			GlobalController.Instance.selectedBuildSite = self;
			self.GetComponent<MeshRenderer> ().material = GlobalController.Instance.highlightedMaterial;
			GlobalController.Instance.menuOpen = true;
			panel.SetActive (true);
            bm.playSoundEffect(openPanelClip);
		}

        // if building panel is open, and you've selected a different build site
        else if (panel.activeSelf && self != GlobalController.Instance.selectedBuildSite) {
			GlobalController.Instance.selectedBuildSite.GetComponent<MeshRenderer> ().material = GlobalController.Instance.baseMaterial;
			GlobalController.Instance.selectedBuildSite = self; 
			self.GetComponent<MeshRenderer> ().material = GlobalController.Instance.highlightedMaterial;
            bm.playSoundEffect(openPanelClip);
        }

        // if a building panel is open and you've re-selected a build site
        else if (panel.activeSelf && self == GlobalController.Instance.selectedBuildSite) {
            
			if (GlobalController.Instance.selectedBuildSite) {
				GlobalController.Instance.selectedBuildSite.GetComponent<MeshRenderer> ().material = GlobalController.Instance.baseMaterial;
			}
			self.GetComponent<MeshRenderer> ().material = GlobalController.Instance.baseMaterial;
			panel.SetActive (false);
			GlobalController.Instance.menuOpen = false;
			GlobalController.Instance.selectedBuildSite = null;
		} 

		// This prevents the stuff below from happening when you click on the main ship.
		// This else statement prevents the buildings from replacing the main ship bug
		// we were getting.
		else {
			return;
		}

        Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bm.buildSite = self;
        bm.buildPosition = self.transform.position;
    }
}
