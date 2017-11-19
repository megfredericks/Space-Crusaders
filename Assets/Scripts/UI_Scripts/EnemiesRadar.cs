using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesRadar : MonoBehaviour {
    public GameObject player;
    public GameObject enemyPrefab;
    public Level level;
    public float radius = 100f;
    public float radiusMultiplier = 3.5f;

    private List<GameObject> _enemyLocatorList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
            return;

        List<GameObject> enemies = level.GetEnemies();

        if(_enemyLocatorList.Count < enemies.Count)
        {
            for (int i = _enemyLocatorList.Count; i < enemies.Count; i++)
                _enemyLocatorList.Add(Instantiate(enemyPrefab, this.gameObject.transform));
        }
        int index = 0;
        for(index = 0; index < enemies.Count; index++)
        {
            Vector3 p = (enemies[index].transform.position - player.transform.position) * radiusMultiplier;
            Vector3 pos = Vector3.ClampMagnitude(new Vector3(p.x, p.z, 0f), radius);
            _enemyLocatorList[index].transform.localPosition = pos;
            _enemyLocatorList[index].SetActive(true);
        }

        for(; index < _enemyLocatorList.Count; index++)
        {
            _enemyLocatorList[index].SetActive(false);
        }
	}
}
