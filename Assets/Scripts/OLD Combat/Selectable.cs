using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put this script on any player ship that can be selected
public class Selectable : MonoBehaviour
{
    private ShipController _ship;

	// Use this for initialization
	void Start ()
    {
        _ship = GetComponent<ShipController>();	
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Selector"))
        {
            if (_ship != null)
                SelectionManager.Instance.AddShipToSelection(_ship);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Selector"))
        {
            if (_ship != null)
                SelectionManager.Instance.RemoveShipFromSelection(_ship);
        }
    }
}
