using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyShip;
    public Transform playerShip;
    public float spawnWidthOver2;

    private int _wave = 1;
    private List<GameObject> _ships = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        SpawnShips();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(_ships.Count == 0)
        {
        }
	}

    public void DeleteShip(GameObject g)
    {
        _ships.Remove(g);
    }

    private void SpawnShips()
    {
        _ships.Clear();

        for(int i = 0; i < _wave; i++)
        {
            Vector3 position = new Vector3(Random.Range(-spawnWidthOver2, spawnWidthOver2), 0, Random.Range(-spawnWidthOver2, spawnWidthOver2));
            GameObject ship = Instantiate(enemyShip, position+playerShip.position, Quaternion.identity);
            ship.SetActive(true);
            _ships.Add(ship);
        }
    }
}
