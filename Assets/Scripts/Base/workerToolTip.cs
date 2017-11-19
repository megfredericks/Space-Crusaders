using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class workerToolTip : MonoBehaviour {

    public Text title;
    public Text toolText;
    public GameObject toolTipPanel;

    // function to set text of tool tip
    public void setToolTip()
    {
        toolTipPanel.SetActive(true);
        title.text = "Worker Upgrades";
        toolText.text = "Upgrade the rate at which your workers work! Nothing like a little forced productivity!\n5 levels available";
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
