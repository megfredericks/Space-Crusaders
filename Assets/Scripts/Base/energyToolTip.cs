﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class energyToolTip : MonoBehaviour
{

    public Text title;
    public Text toolText;
    public GameObject toolTipPanel;

    // Use this for initialization
    void Start()
    {

    }

    // function to set text of tool tip
    public void setToolTip()
    {
        toolTipPanel.SetActive(true);
        title.text = "Energy Upgrades";
        toolText.text = "Upgrade your ship's energy to last longer during space combat!\n5 levels available";
    }

    // function to clear text of tool tip
    public void clearToolTip()
    {
        title.text = "";
        toolText.text = "";
        toolTipPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}