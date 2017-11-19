using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalController : MonoBehaviour
{
    public static GlobalController Instance;

    public GameObject selectedBuildSite;
    public GameObject selectedBuilding;
    public Material baseMaterial;
    public Material highlightedMaterial;

    public AudioClip clickedOnFarmClip;
    public AudioClip clickedOnReactorClip;
    public AudioClip clickedOnScienceLabClip;
    public AudioClip clickedOnOilRigClip;
    public AudioSource clickedOnBuildingSound;

    public float mainThrusterForce;
    public float baseSpeed;
    public float boostSpeed;
    public float boostConsumptionRate;
    public float sideThrusterForce;
    public float energyRechargeRate;
    public float thrusterRechargeRate;
    public float shieldDecayTime;
    public float shieldRechargeRate;
    public float maxHealth;
    public float maxThrustValue;
    public float maxShieldValue;
    public float maxEnergyValue;
    public float currentHealth;
    public int resource;

    /* Base Variables */
    public int foodAmount;                          // food resource variable
    public int numFarms = 1;                        // Number of farms
    public int energyAmount;                        // energy resource variable
    public int numReactors = 1;                     // Number of reactors
    public int oilAmount;                           // oil resource variable
    public int numOilDrills;                        // Nubmer of oil drills
    public int numSciences;                         // Number of science labs
	public List<GameObject> inactiveBuildSites;

    public int resourceRate = 40;                   // Rate of resource collection
    public int upgradeResourceRate = 10;            // Rate of resource collection added per upgrade

    public int foodWorkers;
    public int oilWorkers;
    public int reactorWorkers;

	public string addTitleText;
	public string addNumText;
	public string addYieldText;

	public int numberOfColonists;
	public int colonistLimit;
	public int availableColonists = 0;					// Number of colonists can still be assigned
	public List<Transform> freeWorkers = new List<Transform>();
	public List<Transform> busyWorkers = new List<Transform>();

    public Dictionary<Vector3, int> spotWorkers = new Dictionary<Vector3, int>(); // # of workers at each building
	public Dictionary<Vector3, string> buildingSpots = new Dictionary<Vector3, string>();

    // research related variables
    public int shieldCost = 300;
    public int energyCost = 300;
    public int boostCost = 300;
    public int workerCost = 500;

    public int shieldLvl = 0;
    public int energyLvl = 0;
    public int boostLvl = 0;
    public int workerLevel = 1;

    public bool menuOpen;

	public bool buildSite1Active;
	public bool buildSite2Active;
	public bool buildSite3Active;
	public bool buildSite4Active;
	public bool buildSite5Active;
	public bool buildSite6Active;
	public bool buildSite7Active;
	public bool buildSite8Active;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
		addTitleText = "";
		addNumText = "Workers: ";
		addYieldText = "Current Yield: ";
        highlightedMaterial = Resources.Load("LightningParticle", typeof(Material)) as Material;
        baseMaterial = Resources.Load("Metal pattern 20", typeof(Material)) as Material;
        selectedBuilding = null;
        selectedBuildSite = null;
        clickedOnBuildingSound = gameObject.AddComponent<AudioSource>();

        foodWorkers = 0;
        oilWorkers = 0;
        reactorWorkers = 0;
    }

    public void playBuildingSound(string buildingType)
    {
        if (buildingType == "Oil")
        {
            clickedOnBuildingSound.clip = clickedOnOilRigClip;
            clickedOnBuildingSound.loop = true;
            clickedOnBuildingSound.volume = .4f;
            clickedOnBuildingSound.Play();
        }
        else if (buildingType == "Farm")
        {
            clickedOnBuildingSound.clip = clickedOnFarmClip;
            clickedOnBuildingSound.loop = true;
            clickedOnBuildingSound.volume = .2f;
            clickedOnBuildingSound.Play();
        }

        else if (buildingType == "Reactor")
        {
            clickedOnBuildingSound.clip = clickedOnReactorClip;
            clickedOnBuildingSound.loop = true;
            clickedOnBuildingSound.volume = .4f;
            clickedOnBuildingSound.Play();
        }
        else if (buildingType == "Science")
        {
            clickedOnBuildingSound.clip = clickedOnScienceLabClip;
            clickedOnBuildingSound.loop = true;
            clickedOnBuildingSound.volume = .05f;
            clickedOnBuildingSound.Play();
        }
    }




    // Manage both resource adds in one function to reduce Update overhead
    public void addResources()
    {
        // foodAmount += (foodWorkers * upgradeResourceRate) + (numFarms * resourceRate);
        // energyAmount += (reactorWorkers * upgradeResourceRate) + (numReactors * resourceRate);
        // oilAmount += (oilWorkers * upgradeResourceRate) + (numOilDrills * resourceRate);

        foodAmount += resourceRate + (numFarms * resourceRate) + (foodWorkers * workerLevel * upgradeResourceRate);
        energyAmount += resourceRate + (numReactors * resourceRate) + (reactorWorkers * workerLevel * upgradeResourceRate);
        oilAmount += resourceRate + (numOilDrills * resourceRate) + (oilWorkers * workerLevel * upgradeResourceRate);
    }
		
}