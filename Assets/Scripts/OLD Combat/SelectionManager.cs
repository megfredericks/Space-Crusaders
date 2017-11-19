using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A singleton to handle managing selection
public class SelectionManager : MonoBehaviour
{
    private static SelectionManager _instance;
    public static SelectionManager Instance { get { return _instance; } }

    // Set of selected ships
    private HashSet<ShipController> _selectedShips = new HashSet<ShipController>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Adds a ship to selection
    public void AddShipToSelection(ShipController s)
    {
        if (!_selectedShips.Contains(s))
        {
            _selectedShips.Add(s);
            s.Select();
        }
    }

    // Removes a ship from selection
    public void RemoveShipFromSelection(ShipController s)
    {
        if(_selectedShips.Contains(s))
        {
            _selectedShips.Remove(s);
            s.Deselect();
        }
    }

    // Remove all ships from selection
    public void RemoveAllShipsFromSelection()
    {
        foreach(ShipController s in _selectedShips)
        {
            s.Deselect();
        }

        _selectedShips.Clear();
    }

    // Sets a waypoint for all selected ships
    public void SetWaypointForSelectedShips(Vector3 location)
    {
        foreach(ShipController s in _selectedShips)
        {
            s.SetWaypoint(location);
        }
    }
}
