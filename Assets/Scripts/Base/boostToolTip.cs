using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class boostToolTip : MonoBehaviour {

    public Text title;
    public Text toolText;
    public GameObject toolTipPanel;

    // function to set text of tool tip
    public void setToolTip()
    {
        toolTipPanel.SetActive(true);
        title.text = "Boost Upgrades";
        toolText.text = "Upgrade your ship's thrusters to\ngive a longer boost during space combat!\n5 levels available";
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
