using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSelection : MonoBehaviour
{
    public GameObject selectionCube;
    private Vector3 _pressPosition;
	
	// Update is called once per frame
	void Update ()
    {
        // For mouse input; 0 = left click, 1 = right click
		if(Input.GetMouseButtonDown(0))
        {
            if(!Input.GetKey(KeyCode.LeftShift))
                SelectionManager.Instance.RemoveAllShipsFromSelection();

            _pressPosition = MouseToXZPlane(Input.mousePosition);
            Debug.Log("Press: " + _pressPosition.ToString());
            selectionCube.SetActive(true);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            selectionCube.SetActive(false);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            SelectionManager.Instance.SetWaypointForSelectedShips(MouseToXZPlane(Input.mousePosition));
        }

        // Update the selection box bounds if it is active
        if(selectionCube.activeSelf)
        {
            Vector3 curPos = MouseToXZPlane(Input.mousePosition);
            selectionCube.transform.position = new Vector3((_pressPosition.x+curPos.x)/2f, 0f, (_pressPosition.z+curPos.z)/2f);
            selectionCube.transform.localScale = new Vector3(_pressPosition.x - curPos.x, selectionCube.transform.localScale.y, _pressPosition.z - curPos.z);
        }
	}

    // Translates a mouse position to a world position on the XZ plane.
    private Vector3 MouseToXZPlane(Vector3 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.point;
    }
}
