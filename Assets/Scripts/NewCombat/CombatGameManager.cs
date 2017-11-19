using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGameManager : MonoBehaviour
{
    private static CombatGameManager _instance;
    public static CombatGameManager Instance { get { return _instance; } }

    public int Level { get; private set; }

    private bool _boss = false;

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

        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start()
    {
        // Load level
    }

    public void NextState()
    {
        if(!_boss)
        {
            // spawn ships
            _boss = true;
        }
        else
        {
            // spawn boss
            _boss = false;
        }
    }
}
