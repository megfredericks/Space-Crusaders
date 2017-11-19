using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BaseManager : MonoBehaviour
{

    /* Globals to track in Base Scene */

    // food related variables
    public Text foodDisplay;                        // Text display for food resource
    public GameObject farmPrefab;                    // Farm Prefab object

    // energy related variables
    public Text energyDisplay;                      // Text display for energy resource
    public GameObject reactorPrefab;                 // Reactor Prefab object

    // oil related variables
    public Text oilDisplay;                         // Text display for oil resource
    public GameObject oilPrefab;                     // Oil Rig Prefab object

    // public int numSciences;                         // Number of science labs
    public GameObject sciencePrefab;                 // Science lab prefab
    public Text scienceLabCost;


	public Text workerText;                         // text for # of workers display
    public Text availableText;
    public int workerLevel = 1;
    private float timer = 10;                       // Resource collection interval

    // colonist related variables
    private int starterColNum = 5;                  // Number of starting colonists

    // private int colonistLimit = 10;                 // Max number of colonists allowed
    public Transform colonistPrefab;                // Colonist Prefab object

    // build site and worker # related variables
    public Vector3 buildPosition;                   // Variable to hold current build position
    public GameObject buildSite;
    public List<Vector3> legalBuild;                // Array to hold used build positions


    // error panel related variables
    public GameObject errorPanel;                   // Error Panel GameObject
    public Text errorText;                          // Error Text GameObject
    private int errorActive = 0;                    // Flag for error timer
    private float errorTimer = 3;                   // Timer for error messages

	public GameObject buildSite1;                   // used to setActive(false) between scenes
    public GameObject buildSite2;                   // used to setActive(false) between scenes
    public GameObject buildSite3;                   // used to setActive(false) between scenes
    public GameObject buildSite4;                   // used to setActive(false) between scenes
    public GameObject buildSite5;                   // used to setActive(false) between scenes
    public GameObject buildSite6;                   // used to setActive(false) between scenes
    public GameObject buildSite7;                   // used to setActive(false) between scenes
    public GameObject buildSite8;                   // used to setActive(false) between scenes

    // audio clips & audio sources
    public AudioClip buildingPurchasedClip;
    private AudioSource buidlingPurchasedSound;
    private AudioSource upgradeSound;
    private AudioSource baseEffectSound;
    private string upgradeButtonClicked;

    OpenResearchMenu orm;
    public GameObject researchPanel;
	AddWorker work;
	public GameObject workerPanel;
	public GameObject helpMenu;

	public int numColonists;                        // Number of active colonists
	public int colonistLimit = 5;                 // Max number of colonists allowed
	public int availableColonists;					// Number of colonists can still be assigned
	// add worker panel variables
	public Text addTitleText;
	public Text addNumText;
	public Text addYieldText;

    // Use this for initialization
    void Start()
    {
		//helpMenu.SetActive (true); // does this open the help menu every time the base scene is loaded?

		buildSite1.SetActive (GlobalController.Instance.buildSite1Active);
		buildSite2.SetActive (GlobalController.Instance.buildSite2Active);
		buildSite3.SetActive (GlobalController.Instance.buildSite3Active);
		buildSite4.SetActive (GlobalController.Instance.buildSite4Active);
		buildSite5.SetActive (GlobalController.Instance.buildSite5Active);
		buildSite6.SetActive (GlobalController.Instance.buildSite6Active);
		buildSite7.SetActive (GlobalController.Instance.buildSite7Active);
		buildSite8.SetActive (GlobalController.Instance.buildSite8Active);

		addTitleText.text = GlobalController.Instance.addTitleText;
		addNumText.text = GlobalController.Instance.addNumText;
		addYieldText.text = GlobalController.Instance.addYieldText;

        GlobalController.Instance.currentHealth = GlobalController.Instance.maxHealth;

		if (GlobalController.Instance.buildingSpots.Count > 0) {
			foreach (Vector3 entry in GlobalController.Instance.buildingSpots.Keys) {
				if (GlobalController.Instance.buildingSpots [entry].Equals ("Farm")) {
					Instantiate (farmPrefab, entry, farmPrefab.transform.rotation);
				} else if (GlobalController.Instance.buildingSpots [entry].Equals ("Science")) {
					Instantiate (sciencePrefab, entry, sciencePrefab.transform.rotation);
				} else if (GlobalController.Instance.buildingSpots [entry].Equals ("Reactor")) {
					Instantiate (reactorPrefab, entry, reactorPrefab.transform.rotation);
				} else if (GlobalController.Instance.buildingSpots [entry].Equals ("Oil")) {
					Instantiate (oilPrefab, entry, oilPrefab.transform.rotation);
				}
			}
		}
		/* Set build sites to be inactive where buildings exist */
		if (GlobalController.Instance.inactiveBuildSites.Count > 0) {
			foreach (GameObject site in GlobalController.Instance.inactiveBuildSites) {
				site.SetActive (false);
			}
		}

        GlobalController.Instance.menuOpen = false;

        legalBuild = new List<Vector3>();

        buidlingPurchasedSound = AddAudio(buildingPurchasedClip, false, false, .7f);
        upgradeSound = AddAudio(null, false, false, .5f);
        baseEffectSound = AddAudio(null, false, false, .5f);

		numColonists = GlobalController.Instance.numberOfColonists;
		colonistLimit = GlobalController.Instance.colonistLimit;
		availableColonists = GlobalController.Instance.availableColonists;

		Transform cur;
		for (int i = 0; i < availableColonists; i++)
		{
			cur = Instantiate(colonistPrefab, new Vector3(421 + i*3, 6, 301), Quaternion.identity);
			GlobalController.Instance.freeWorkers.Add(cur);
		}

        GlobalController.Instance.foodAmount += GlobalController.Instance.resource;
        GlobalController.Instance.energyAmount += GlobalController.Instance.resource;
        GlobalController.Instance.oilAmount += GlobalController.Instance.resource;
        GlobalController.Instance.resource = 0;

        // set start resource text
        foodDisplay.text = "Food: " + GlobalController.Instance.foodAmount.ToString();
        energyDisplay.text = "Energy: " + GlobalController.Instance.energyAmount.ToString();
        oilDisplay.text = "Oil: " + GlobalController.Instance.oilAmount.ToString();
        workerText.text = "Workers: " + numColonists.ToString() + "/" + colonistLimit;
        availableText.text = "Available: " + availableColonists.ToString() + " / " + GlobalController.Instance.colonistLimit.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GlobalController.Instance.addResources();
            foodDisplay.text = "Food: " + GlobalController.Instance.foodAmount.ToString();
            energyDisplay.text = "Energy: " + GlobalController.Instance.energyAmount.ToString();
            oilDisplay.text = "Oil: " + GlobalController.Instance.oilAmount.ToString();
            timer = 10;
        }

        // disable error panel after 3 seconds
        if (errorActive == 1)
        {
            errorTimer -= Time.deltaTime;
            if (errorTimer <= 0)
            {
                errorPanel.SetActive(false);
                errorActive = 0;
                errorTimer = 3;
            }
        }
    }


	void OnDestroy()
	{
		GlobalController.Instance.numberOfColonists = numColonists;
		GlobalController.Instance.colonistLimit = colonistLimit;
		GlobalController.Instance.addTitleText = addTitleText.text;
		GlobalController.Instance.addNumText = addNumText.text;
		GlobalController.Instance.addYieldText = addYieldText.text;
		GlobalController.Instance.availableColonists = availableColonists;
	}

    public void updateWorkerText()
    {
        workerText.text = "Workers: " + numColonists + "/" + colonistLimit;
        availableText.text = "Available: " + availableColonists + "/" + colonistLimit;
    }

    public void assignWorker()
    {
        
        if (GlobalController.Instance.spotWorkers[GlobalController.Instance.selectedBuilding.transform.position] < 5 && availableColonists > 0)
        {
            
            GlobalController.Instance.spotWorkers[GlobalController.Instance.selectedBuilding.transform.position]++; // update amount
            string temp = GlobalController.Instance.spotWorkers[GlobalController.Instance.selectedBuilding.transform.position].ToString();
            addNumText.text = "Workers: "  + temp + " / 5";
            
			availableColonists--;
			Transform worker = GlobalController.Instance.freeWorkers [0];
			GlobalController.Instance.freeWorkers.RemoveAt (0);
			GlobalController.Instance.busyWorkers.Add (worker);
			worker.gameObject.SetActive (false);
            updateWorkerText();
            addYieldText.text = "Current Yield: " 
                + ((GlobalController.Instance.spotWorkers[GlobalController.Instance.selectedBuilding.transform.position] * 10) + 40).ToString() + " / hr";

            string buildingType = GlobalController.Instance.buildingSpots[GlobalController.Instance.selectedBuilding.transform.position];
            if (buildingType == "Oil")
            {
                GlobalController.Instance.oilWorkers++;
            }
            else if (buildingType == "Farm")
            {
                GlobalController.Instance.foodWorkers++;
            }

            else if (buildingType == "Reactor")
            {
                GlobalController.Instance.reactorWorkers++;
            }
        }
    }

    public void freeWorker()
    {
        if (GlobalController.Instance.spotWorkers[GlobalController.Instance.selectedBuilding.transform.position] > 0)
        {
            GlobalController.Instance.spotWorkers[GlobalController.Instance.selectedBuilding.transform.position]--;
			availableColonists++;
			Transform worker = GlobalController.Instance.busyWorkers [0];
			GlobalController.Instance.busyWorkers.RemoveAt (0);
			GlobalController.Instance.freeWorkers.Add (worker);
			worker.gameObject.SetActive (true);
            updateWorkerText();
            /* Set UI panel */
            addNumText.text = "Workers: " 
                + GlobalController.Instance.spotWorkers[GlobalController.Instance.selectedBuilding.transform.position] + " / 5";

            addYieldText.text = "Current Yield: " 
                + ((GlobalController.Instance.spotWorkers[GlobalController.Instance.selectedBuilding.transform.position] * 10) + 40).ToString() + " / hr";
            string buildingType = GlobalController.Instance.buildingSpots[GlobalController.Instance.selectedBuilding.transform.position];
            if (buildingType == "Oil")
            {
                GlobalController.Instance.oilWorkers--;
            }
            else if (buildingType == "Farm")
            {
                GlobalController.Instance.foodWorkers--;
            }

            else if (buildingType == "Reactor")
            {
                GlobalController.Instance.reactorWorkers--;
            }
        }
    }


    public void deselectBuildSite()
    {
        GlobalController.Instance.selectedBuildSite.GetComponent<MeshRenderer>().material = GlobalController.Instance.baseMaterial;
    }

    public void closePanel()
    {
        GlobalController.Instance.menuOpen = false;
    }

    public void openPanel()
    {
        GlobalController.Instance.menuOpen = true;
    }



    // create a new colonist as long as cost is available and limit has not been reached
    public void makeWorker()
    {
        if (GlobalController.Instance.foodAmount >= 100 && numColonists < colonistLimit)
        {
            // instantiate new colonist
            Transform cur = Instantiate(colonistPrefab, new Vector3(421, 6, 301), Quaternion.identity);
            GlobalController.Instance.freeWorkers.Add(cur);
            GlobalController.Instance.foodAmount -= 100;
            foodDisplay.text = "Food: " + GlobalController.Instance.foodAmount.ToString();
            numColonists++;
			availableColonists++;
            updateWorkerText();

        }
        else if (GlobalController.Instance.foodAmount < 100)
        {
            setErrorPanel("Not enough Food to build worker");
        }
        else
        {
            setErrorPanel("Worker Limit Reached");
        }
    }

    // create a new farm
    public void makeFarm()
    {
        if (legalBuild.Contains(buildPosition))
        {
            setErrorPanel("Build Site already taken");
        }
        else if (GlobalController.Instance.foodAmount < 250)
        {
            setErrorPanel("Not enough Food to build Farm");
        }
        // else make farm
        else
        {
			
            buidlingPurchasedSound.Play();
			deactivateBuildSite (buildSite);
            legalBuild.Add(buildPosition);          // Add build Position to legalBuild List
            GlobalController.Instance.spotWorkers.Add(buildPosition, 0);      // Add build Position to spot Workers dictionary
            GlobalController.Instance.foodAmount -= 250;
            foodDisplay.text = "Food: " + GlobalController.Instance.foodAmount;
            GlobalController.Instance.buildingSpots.Add(buildPosition, "Farm");
            GameObject clone = Instantiate(farmPrefab, buildPosition, farmPrefab.transform.rotation) as GameObject;
			setWorkerPanel (clone);
            GlobalController.Instance.numFarms++;
        }
    }

    // create a new reactor
    public void makeReactor()
    {
        if (legalBuild.Contains(buildPosition))
        {
            setErrorPanel("Build Site already taken");
        }
        else if (GlobalController.Instance.foodAmount < 250)
        {
            setErrorPanel("Not enough Food to build Reactor");
        }
        else
        {
            buidlingPurchasedSound.Play();
			deactivateBuildSite (buildSite);
            legalBuild.Add(buildPosition);          // Add build Position to legalBuild List

            GlobalController.Instance.spotWorkers.Add(buildPosition, 0);      // Add build Position to spot Workers dictionary

            GlobalController.Instance.foodAmount -= 250;
            foodDisplay.text = "Food: " + GlobalController.Instance.foodAmount;
			GlobalController.Instance.buildingSpots.Add(buildPosition, "Reactor");
            GameObject clone = Instantiate(reactorPrefab, buildPosition, reactorPrefab.transform.rotation) as GameObject;
            setWorkerPanel(clone);
            GlobalController.Instance.numReactors++;
        }
    }

    // create a new Science Facility
    public void makeScienceBuilding()
    {
        if (legalBuild.Contains(buildPosition))
        {
            setErrorPanel("Build Site already taken");
        }
        else if (GlobalController.Instance.foodAmount < 250)
        {
            setErrorPanel("Not enough Food to build Science Facility");
        }
        else if (GlobalController.Instance.numSciences > 0)
        {
            setErrorPanel("Science Lab Already Built!");
        }
        else
        {
            buidlingPurchasedSound.Play();
			deactivateBuildSite (buildSite);
            legalBuild.Add(buildPosition);          // Add build Position to legalBuild List
           // spotWorkers.Add(buildPosition, 0);      // Add build Position to spot Workers dictionary
            GlobalController.Instance.foodAmount -= 250;
            foodDisplay.text = "Food: " + GlobalController.Instance.foodAmount;
			GlobalController.Instance.buildingSpots.Add(buildPosition, "Science");
            GameObject clone = Instantiate(sciencePrefab, buildPosition, sciencePrefab.transform.rotation) as GameObject;
            setResearchPanel(clone);
            GlobalController.Instance.numSciences++;
            scienceLabCost.text = "Max Built";
        }
    }

    // create a new Oil Drill
    public void makeOilDrill()
    {
        if (legalBuild.Contains(buildPosition))
        {
            setErrorPanel("Build Site already taken");
        }
        else if (GlobalController.Instance.foodAmount < 250)
        {
            setErrorPanel("Not enough Food to build Oil Drill!");
        }
        else
        {
            buidlingPurchasedSound.Play();
			deactivateBuildSite (buildSite);
            legalBuild.Add(buildPosition);          // Add build Position to legalBuild List
            GlobalController.Instance.spotWorkers.Add(buildPosition, 0);      // Add build Position to spot Workers dictionary
            GlobalController.Instance.buildingSpots.Add(buildPosition, "Oil");
			GlobalController.Instance.foodAmount -= 250;
            foodDisplay.text = "Food: " + GlobalController.Instance.foodAmount;
            GameObject clone = Instantiate(oilPrefab, buildPosition, oilPrefab.transform.rotation) as GameObject;
            setWorkerPanel (clone);
            GlobalController.Instance.numOilDrills++;
        }
    }

    public void setErrorPanel(string text)
    {
        errorText.text = text;
        errorPanel.SetActive(true);
        errorActive = 1;
    }

	public void setResearchPanel(GameObject scienceLab)
    {
		orm = scienceLab.GetComponent<OpenResearchMenu>();
        orm.panel = researchPanel;
    }

	public void setWorkerPanel(GameObject workerBuilding)
	{
		workerBuilding.GetComponent<AddWorker>().panel = workerPanel;
		
	}

    public void setUpgradeSelection(string upgradeType)
    {
        upgradeButtonClicked = upgradeType;
    }

    public void playSound(AudioClip audioClip)
    {
        if ((upgradeButtonClicked == "shield" && GlobalController.Instance.energyAmount >= GlobalController.Instance.shieldCost)
            || (upgradeButtonClicked == "energy" && GlobalController.Instance.energyAmount >= GlobalController.Instance.energyCost)
            || (upgradeButtonClicked == "oil" && GlobalController.Instance.oilAmount >= GlobalController.Instance.boostCost)
            || (upgradeButtonClicked == "worker" && GlobalController.Instance.foodAmount >= GlobalController.Instance.workerCost))
        {
            stopBuildingSound();
            upgradeSound.clip = audioClip;
            upgradeSound.loop = false;
            upgradeSound.volume = .3f;
            upgradeSound.Play();
        }
    }

    public void playSoundEffect(AudioClip audio)
    {
        baseEffectSound.clip = audio;
        baseEffectSound.Play();
    }

    public void stopBuildingSound()
    {
        GlobalController.Instance.clickedOnBuildingSound.Stop();
    }

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;

        return newAudio;
    }

	public void deactivateBuildSite(GameObject bs)
	{
		bs.SetActive(false);
		if (bs == buildSite1) {
			GlobalController.Instance.buildSite1Active = false;
		}
		if (bs == buildSite2) {
			GlobalController.Instance.buildSite2Active = false;
		}
		if (bs == buildSite3) {
			GlobalController.Instance.buildSite3Active = false;
		}
		if (bs == buildSite4) {
			GlobalController.Instance.buildSite4Active = false;
		}
		if (bs == buildSite5) {
			GlobalController.Instance.buildSite5Active = false;
		}
		if (bs == buildSite6) {
			GlobalController.Instance.buildSite6Active = false;
		}
		if (bs == buildSite7) {
			GlobalController.Instance.buildSite7Active = false;
		}
		if (bs == buildSite8 ) {
			GlobalController.Instance.buildSite8Active = false;
		}
	}
}
