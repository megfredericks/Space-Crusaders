using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public GameObject bossShip;
    public string nextSector;
	public bool tutorial = false;

    private List<GameObject> _ships = new List<GameObject>();
    private bool _bossFlag = false;

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
		if (!tutorial) {
			if (_ships.Count == 0 && !_bossFlag)
				SpawnBoss ();
			else if (_ships.Count == 0 && _bossFlag)
				StartCoroutine (GoToNext (nextSector));
		}
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
		if(!tutorial)
			StartCoroutine (GoToNext ("Base"));
		else
			StartCoroutine (GoToNext ("Transition"));
	}

    private void SpawnBoss()
    {
        _bossFlag = true;
        if(bossShip != null)
            bossShip.SetActive(true);
    }
}
