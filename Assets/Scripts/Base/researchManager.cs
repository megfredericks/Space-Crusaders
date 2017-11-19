using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class researchManager : MonoBehaviour {

    BaseManager bm;


    public Text shieldLvlText;
    public Text energyLvlText;
    public Text boostLvlText;
    public Text workerLvlText;

    public Text shieldCostText;
    public Text energyCostText;
    public Text boostCostText;
    public Text workerCostText;

    public float increaseShieldValue;
    public float shieldRechargeRate;
    public float increaseEnergyValue;
    public float energyRechargeRate;
    public float increaseThrustValue;
    public float thrustRechargeRate;

    public Text shieldStrengthText;
    public Text shieldRechargeText;
    public Text weaponEnergyCapacityText;
    public Text energyRechargeText;
    public Text boostTankSizeText;
    public Text boostRefillText;



	// Use this for initialization
	void Start () {
        bm = GameObject.Find("GameManager").GetComponent<BaseManager>();
        shieldLvlText.text = GlobalController.Instance.shieldLvl.ToString() + " / 5";
        if(GlobalController.Instance.shieldLvl < 5)
        {
            shieldCostText.text = GlobalController.Instance.shieldCost.ToString() + " energy";
        }
        else
        {
            shieldCostText.text = "";
        }

        int tempEnergyLevel = GlobalController.Instance.energyLvl;
        energyLvlText.text = tempEnergyLevel.ToString() + " / 5";
        if (tempEnergyLevel < 5)
        {
            energyCostText.text = GlobalController.Instance.energyCost.ToString() + " energy";
        }
        else
        {
            energyCostText.text = "";
        }

        int tempBoostLevel = GlobalController.Instance.boostLvl;
        boostLvlText.text = tempBoostLevel.ToString() + " / 5";
        if (tempBoostLevel < 5)
        {
            boostCostText.text = GlobalController.Instance.boostCost.ToString() + " oil";
        }
        else
        {
            boostCostText.text = "";
        }

        int dispLvl = GlobalController.Instance.workerLevel - 1;
        workerLvlText.text = dispLvl.ToString() + " / 5";
        if (dispLvl < 5)
        {
            workerCostText.text = GlobalController.Instance.workerCost.ToString() + " food";
        }
        else
        {
            workerCostText.text = "";
        }
        shieldStrengthText.text = "Shield Strength: " + GlobalController.Instance.maxShieldValue; // increaseShieldValue
        shieldRechargeText.text = "Shield Recharge Rate: " + GlobalController.Instance.shieldRechargeRate; // shieldRechargeRate;
        weaponEnergyCapacityText.text = "Weapon Energy Capacity: " + GlobalController.Instance.maxEnergyValue; // increaseEnergyValue;
        energyRechargeText.text = "Weapon Recharge Rate: " + GlobalController.Instance.energyRechargeRate; // energyRechargeRate;
        boostTankSizeText.text = "Boost Tank Size: " + GlobalController.Instance.maxThrustValue; // increaseThrustValue;
        boostRefillText.text = "Boost Refill Rate: " + GlobalController.Instance.thrusterRechargeRate; // thrustRechargeRate;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Shield Upgrade function
    public void upgradeShield()
    {
        if (GlobalController.Instance.shieldLvl < 5)
        {
            if (GlobalController.Instance.energyAmount < GlobalController.Instance.shieldCost)
            {
                bm.setErrorPanel("Not enough energy to upgrade Shields");
            }
            else
            {
                GlobalController.Instance.energyAmount -= GlobalController.Instance.shieldCost;
                bm.energyDisplay.text = "Energy: " + GlobalController.Instance.energyAmount;
                // set val in playerStats

                GlobalController.Instance.maxShieldValue += increaseShieldValue;
                GlobalController.Instance.shieldRechargeRate += shieldRechargeRate;

                GlobalController.Instance.shieldCost *= 2;
                GlobalController.Instance.shieldLvl++;
                shieldLvlText.text = GlobalController.Instance.shieldLvl.ToString() + " / 5";
                shieldCostText.text = GlobalController.Instance.shieldCost.ToString() + " energy";

                shieldStrengthText.text = "Shield Strength: " + GlobalController.Instance.maxShieldValue; // increaseShieldValue
                shieldRechargeText.text = "Shield Recharge Rate: " + GlobalController.Instance.shieldRechargeRate; // shieldRechargeRate;

                if (GlobalController.Instance.shieldLvl == 5)
                {
                    shieldCostText.text = "";
                }
            }
        }
        else
        {
            bm.setErrorPanel("Shield Level is already maxed out");
            
        }
    }

    // Energy Upgrade function
    public void upgradeEnergy()
    {
        if (GlobalController.Instance.energyLvl < 5)
        {
            if (GlobalController.Instance.energyAmount < GlobalController.Instance.energyCost)
            {
                bm.setErrorPanel("Not enough energy to upgrade Energy");
            }
            else
            {
                GlobalController.Instance.energyAmount -= GlobalController.Instance.energyCost;
                bm.energyDisplay.text = "Energy: " + GlobalController.Instance.energyAmount;

                // set val in playerStats
                GlobalController.Instance.maxEnergyValue += increaseEnergyValue;
                GlobalController.Instance.energyRechargeRate += energyRechargeRate;

                GlobalController.Instance.energyCost *= 2;
                GlobalController.Instance.energyLvl++;
                energyLvlText.text = GlobalController.Instance.energyLvl.ToString() + " / 5";
                energyCostText.text = GlobalController.Instance.energyCost.ToString() + " energy";

                weaponEnergyCapacityText.text = "Weapon Energy Capacity: " + GlobalController.Instance.maxEnergyValue; // increaseEnergyValue;
                energyRechargeText.text = "Weapon Recharge Rate: " + GlobalController.Instance.energyRechargeRate; // energyRechargeRate;

                if (GlobalController.Instance.energyLvl == 5)
                {
                    energyCostText.text = "";
                }
            }
        }
        else
        {
            bm.setErrorPanel("Ship Energy Level is already maxed out");
        }
    }

    // Boost Upgrade function
    public void upgradeBoost()
    {
        if (GlobalController.Instance.boostLvl < 5)
        {
            if (GlobalController.Instance.oilAmount < GlobalController.Instance.boostCost)
            {
                bm.setErrorPanel("Not enough oil to upgrade Boosts");
            }
            else
            {
                GlobalController.Instance.oilAmount -= GlobalController.Instance.boostCost;
                bm.oilDisplay.text = "Oil: " + GlobalController.Instance.oilAmount;

                // set val in playerStats
                GlobalController.Instance.maxThrustValue += increaseThrustValue;

                GlobalController.Instance.thrusterRechargeRate += thrustRechargeRate;

                GlobalController.Instance.boostCost *= 2;   
                GlobalController.Instance.boostLvl++;

                boostLvlText.text = GlobalController.Instance.boostLvl.ToString() + " / 5";
                boostCostText.text = GlobalController.Instance.boostCost.ToString() + " oil";

                boostTankSizeText.text = "Boost Tank Size: " + GlobalController.Instance.maxThrustValue; // increaseThrustValue;
                boostRefillText.text = "Boost Refill Rate: " + GlobalController.Instance.thrusterRechargeRate; // thrustRechargeRate;

                if (GlobalController.Instance.boostLvl == 5)
                {
                    boostCostText.text = "";
                }
         
            }
        }
        else
        {
            bm.setErrorPanel("Ship Boost Level is already maxed out");
        }
    }

    // Worker Upgrade Function
    public void upgradeWorker()
    {
        if (GlobalController.Instance.workerLevel < 6)
        {
            if (GlobalController.Instance.foodAmount < GlobalController.Instance.workerCost)
            {
                bm.setErrorPanel("Not enough energy to upgrade Workers");
            }
            else
            {
                GlobalController.Instance.foodAmount -= GlobalController.Instance.workerCost;
                bm.foodDisplay.text = "Food: " + GlobalController.Instance.foodAmount;
                GlobalController.Instance.workerLevel++;
                bm.colonistLimit += 5;
                bm.updateWorkerText();
                GlobalController.Instance.workerCost *= 2;

                int dispLvl = GlobalController.Instance.workerLevel - 1;
                workerLvlText.text = dispLvl.ToString() + " / 5";
                workerCostText.text = GlobalController.Instance.workerCost.ToString() + " food";  
                if (GlobalController.Instance.workerLevel == 6)
                {
                    workerCostText.text = "";
                }
            }
        }
        else
        {
            bm.setErrorPanel("Worker Level is already maxed out");
        }
    }
}
