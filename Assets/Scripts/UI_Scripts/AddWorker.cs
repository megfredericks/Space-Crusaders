using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AddWorker : MonoBehaviour
{
	BaseManager bm;
    private Vector3 selfpos;
    public GameObject panel;

    public string type;

    private Ray ray;
    private RaycastHit hit;


    // Use this for initialization
    void Start()
    {
		bm = GameObject.Find("GameManager").GetComponent<BaseManager>();
		panel = GameObject.Find ("Canvas").transform.GetChild (0).gameObject;


        Transform temp = GetComponent<Transform>();
        selfpos = temp.position;
        type = "";


    }

    void OnMouseUp()
    {
        GlobalController.Instance.selectedBuilding = gameObject;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        type = GlobalController.Instance.buildingSpots[selfpos];
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "workerBuilding" && !GlobalController.Instance.menuOpen)
            {
                panel.SetActive(true);      // Opens the research panel when clicked
                bm.addNumText.text = "Workers: " + GlobalController.Instance.spotWorkers[selfpos] + " / 5" ;
                GlobalController.Instance.menuOpen = true;
                bm.addYieldText.text = "Current Yield: " + 
                    (GlobalController.Instance.spotWorkers[selfpos] * GlobalController.Instance.upgradeResourceRate + 40).ToString() + " / hr";

                bm.updateWorkerText();
                GlobalController.Instance.playBuildingSound(type);

            }
        }

    }
}
