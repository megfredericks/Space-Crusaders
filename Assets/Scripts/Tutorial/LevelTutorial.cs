using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTutorial : MonoBehaviour
{
	private List<GameObject> _ships = new List<GameObject>();

	// Use this for initialization
	void Start()
	{
		//SpawnShips();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void RegisterShip(GameObject g)
	{
		if (!_ships.Contains(g))
			_ships.Add(g);
	}

	public void DeleteShip(GameObject g)
	{
		if (_ships.Count > 0)
			_ships.Remove(g);
	}

	public List<GameObject> GetEnemies()
	{
		return _ships;
	}

	IEnumerator GoToNext(string next)
	{
		yield return new WaitForSeconds(4f);
		SceneManager.LoadScene(next);
	}

	public void GoToBase()
	{
		StartCoroutine (GoToNext ("Transition"));
	}
}
