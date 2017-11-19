using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles the player ship. Attach this script on any
// ship the player can control.
public class ShipController : MonoBehaviour
{
    public GameObject selectedIndicator;
    public float moveSpeed;

    private Vector3 _waypoint;

	// Use this for initialization
	void Start ()
    {
        _waypoint = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, _waypoint, moveSpeed * Time.deltaTime);
	}

    // This ship is selected
    public void Select()
    {
        selectedIndicator.SetActive(true);
    }

    // This ship is deselected
    public void Deselect()
    {
        selectedIndicator.SetActive(false);
    }

    // Sets the waypoint to travel to
    public void SetWaypoint(Vector3 p)
    {
        _waypoint = p;
    }
}
