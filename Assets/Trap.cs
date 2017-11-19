using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public GameObject player;

    public void OnEnable()
    {
        this.transform.position = player.transform.position;
    }

    public void Enable()
    {
        foreach (Transform g in transform)
        {
            g.gameObject.SendMessage("Enable");
        }
    }
}
