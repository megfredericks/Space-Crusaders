using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpenResearchMenu : MonoBehaviour
{

    public GameObject panel;
    public Vector3 selfPos;
    public string test = "Reached object";
    BaseManager bm;
	private Ray ray;			// Used to see what object is clicked on.
	private RaycastHit hit;		// Changes to be object that is clicked.



    // Use this for initialization
    void Start()
    {
        bm = GameObject.Find("GameManager").GetComponent<BaseManager>();
		bm.setResearchPanel (this.gameObject);
        Transform temp = GetComponent<Transform>();
        selfPos = temp.position;
    }

    void OnMouseDown()
    {
        // hit will become the thing that is clicked on.
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "Science Lab" && !GlobalController.Instance.menuOpen)
            {
				
				panel.SetActive(true);      // Opens the research panel when clicking a science lab.
                GlobalController.Instance.menuOpen = true;
                string type = (GlobalController.Instance.buildingSpots[selfPos]);
                GlobalController.Instance.playBuildingSound(type);
            }
        }

        bm.buildPosition = selfPos;

    }
}
