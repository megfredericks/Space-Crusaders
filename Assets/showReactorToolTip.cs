using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class showReactorToolTip : MonoBehaviour
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
        title.text = "Build Reactor";
        toolText.text = "Build a Reactor to increase energy production by 40/hr \n \nCost: 250 Food";
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


