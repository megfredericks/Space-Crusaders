using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipControllerCombat : MonoBehaviour, IDamageable 
{
    private float mainThrusterForce;
    private float baseSpeed;
    private float boostSpeed;
    private float boostConsumptionRate;
    private float sideThrusterForce;
    //private float maxHealth;
    private float energyRechargeRate;
    private float thrusterRechargeRate;
    private float shieldDecayTime;
    private float shieldRechargeRate;

	public Level level;
    public Slider mainThrusterSlider;
	public Slider laserEnergySlider;
	public Slider shieldEnergySlider;
    public Slider healthSlider;
	public Text healthDisplay;
	public Text energyDisplay;
	public Text shieldDisplay;
	public Text boostDisplay;

    [Tooltip("Degrees/second")]
    public float rotationSpeed;
	public GameObject mainAttack;
	public GameObject altAttack;
    public GameObject shield;

	private Rigidbody _rgidBdy;
	private Vector3 _force = new Vector3();
	private IWeapon _mainAttackWeapon;
	private IWeapon _altAttackWeapon;
	//private float _currentHealth;
    private Boolean _shieldDeployed;

    public GameObject playerHitExplosionParticle;
    public GameObject playerExplosionParticle;
    public GameObject mainBoostParticleBits;
    private GameObject playerHitExplosionParticleClone;
    private GameObject playerExplosionParticleClone;
    private GameObject mainBoostParticleBitsClone;
    public GameObject shieldHitParticle;
    private GameObject shieldHitParticleClone;



    public AudioClip mainThrusterClip;
    public AudioClip sideThrusterClip;
    public AudioClip mainBoostClip;
    public AudioClip sideBoostClip;
    public AudioClip backingUpBeepsClip;
    private AudioSource mainThrusterSound;
    private AudioSource sideThrusterSound;
    private AudioSource mainBoostSound;
    private AudioSource sideBoostSound;
    private AudioSource backingUpBeepsSound;

    private float _timeSinceLastBoost;
    private float _doubleTapTimeA;
    private float _doubleTapTimeD;
    private int _doubleTapBoostA;  
    private int _doubleTapBoostD;
    private float _timeBetweenBoosts;
    private float _boostLength;
    private float _currentSpeed;
    private bool _consumeBool = false;

    void Awake()
    {
        // add the necessary AudioSources:
        mainThrusterSound = AddAudio(mainThrusterClip, true, false, 0.4f);
        sideThrusterSound = AddAudio(sideThrusterClip, true, false, 0.2f);
        mainBoostSound = AddAudio(mainBoostClip, true, false, .5f);
        sideBoostSound = AddAudio(sideBoostClip, true, false, .5f);
        backingUpBeepsSound = AddAudio(backingUpBeepsClip, true, false, .5f);
    }

    // Use this for initialization
    void Start () 
	{

		_rgidBdy = GetComponent<Rigidbody> ();
		_mainAttackWeapon = mainAttack.GetComponent<MonoBehaviour>() as IWeapon;
		_altAttackWeapon = altAttack.GetComponent<MonoBehaviour>() as IWeapon;
		
        _doubleTapTimeA = Time.time;
        _doubleTapTimeD = Time.time;
        _doubleTapBoostA = 1;
        _doubleTapBoostD = 1;
        _timeBetweenBoosts = .5f;
        _boostLength = .45f;
        mainAttack.gameObject.tag = this.gameObject.tag;
        altAttack.gameObject.tag = this.gameObject.tag;

        mainThrusterSound.volume = 0;
        mainThrusterSound.Play();
        sideThrusterSound.volume = 0;
        sideThrusterSound.Play();

  
        mainThrusterForce = GlobalController.Instance.mainThrusterForce;
        baseSpeed = GlobalController.Instance.baseSpeed;
        boostSpeed = GlobalController.Instance.boostSpeed;
        boostConsumptionRate = GlobalController.Instance.boostConsumptionRate;
        sideThrusterForce = GlobalController.Instance.sideThrusterForce;
        //maxHealth = GlobalController.Instance.maxHealth;
        energyRechargeRate = GlobalController.Instance.energyRechargeRate;
        thrusterRechargeRate = GlobalController.Instance.thrusterRechargeRate;
        shieldDecayTime = GlobalController.Instance.shieldDecayTime;
        shieldRechargeRate = GlobalController.Instance.shieldRechargeRate;
        _currentSpeed = baseSpeed;
        //_currentHealth = maxHealth;
        _timeSinceLastBoost = Time.time - _timeBetweenBoosts;


        laserEnergySlider.maxValue = GlobalController.Instance.maxEnergyValue;
        laserEnergySlider.value = laserEnergySlider.maxValue;
        mainThrusterSlider.maxValue = GlobalController.Instance.maxThrustValue;
        mainThrusterSlider.value = mainThrusterSlider.maxValue;
        shieldEnergySlider.maxValue = GlobalController.Instance.maxShieldValue;
        shieldEnergySlider.value = shieldEnergySlider.maxValue;
        healthSlider.maxValue = GlobalController.Instance.maxHealth;
        healthSlider.value = GlobalController.Instance.currentHealth;
		healthDisplay.text = healthSlider.value.ToString();
		shieldDisplay.text = shieldEnergySlider.value.ToString ();
		energyDisplay.text = laserEnergySlider.value.ToString ();
		boostDisplay.text = mainThrusterSlider.value.ToString ();
    }



    // Update is called once per frame
    void Update () 
	{
		_force = Vector3.zero;

        // Look at the the mouse
		Vector3 mousePos = MouseToXZPlane (Input.mousePosition);
        Quaternion targetRotation = Quaternion.LookRotation(mousePos - this.transform.position);
        targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, 0f);

        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        
		// Get WASD key pressed. Calculate the trust
		if(Input.GetKey(KeyCode.W))
		{
            
			_force += transform.forward * mainThrusterForce;
            if (mainThrusterSlider.value > 1)
            {
                if(_consumeBool)
                {
                    mainThrusterSlider.value -= boostConsumptionRate * Time.deltaTime;
                }
                
            }
            else
            {
                mainThrusterSound.volume = .4f;
                mainBoostSound.Stop();
                _consumeBool = false;
                baseSpeed = _currentSpeed;
            }
        }
		if(Input.GetKey(KeyCode.A))
		{
			_force += transform.right * -sideThrusterForce * _doubleTapBoostA;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _force -= transform.forward * sideThrusterForce * .3f;
        }
        if (Input.GetKey(KeyCode.D))
		{
			_force += transform.right * sideThrusterForce * _doubleTapBoostD;
        }

        // get WASD keydown. Play thruster audio clips.
        if (Input.GetKeyDown(KeyCode.W))
        {
            mainThrusterSound.volume = .4f;
            
            float startKeyPress = Time.time;
            if (startKeyPress - _doubleTapTimeA < .185f && startKeyPress - _timeSinceLastBoost > _timeBetweenBoosts && mainThrusterSlider.value > 1)
            {
                mainBoostParticleBitsClone = Instantiate(mainBoostParticleBits, transform.position, transform.rotation);
                Destroy(mainBoostParticleBitsClone, 5.0f);
                _consumeBool = true;
                mainBoostSound.Play();
                mainThrusterSound.volume = 0f;
                baseSpeed = boostSpeed;
            }
            else _doubleTapBoostA = 1;
            _doubleTapTimeA = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            sideThrusterSound.volume = .2f;
            float startKeyPress = Time.time;
            if (startKeyPress - _doubleTapTimeA < .185f && startKeyPress - _timeSinceLastBoost > _timeBetweenBoosts)
            {
                _doubleTapBoostA = 80;
                sideBoostSound.Play();
                sideThrusterSound.Stop();
                StartCoroutine(SideThrusterBoost());
            }
            else _doubleTapBoostA = 1;
            _doubleTapTimeA = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            backingUpBeepsSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            sideThrusterSound.volume = .2f;
            float startKeyPress = Time.time;
            if (startKeyPress - _doubleTapTimeD < .185f && startKeyPress - _timeSinceLastBoost > _timeBetweenBoosts)
            {
                _doubleTapBoostD = 80;
                sideBoostSound.Play();
                sideThrusterSound.Stop();
                StartCoroutine(SideThrusterBoost());
            }
            else _doubleTapBoostD = 1;
            _doubleTapTimeD = Time.time;
        }

        // Get WASD keyup. Fade thruster audio clips.
        if (Input.GetKeyUp(KeyCode.W))
        {
            for (int i = 0; i < 400; i++)
            {
                mainThrusterSound.volume -= .001f;
                mainBoostSound.Stop();
                _consumeBool = false;
                baseSpeed = _currentSpeed;
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            for (int i = 0; i < 200; i++)
            {
                sideThrusterSound.volume -= .001f;
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            backingUpBeepsSound.Stop();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            for (int i = 0; i < 200; i++)
            {
                sideThrusterSound.volume -= .001f;
            }
        }  

        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(shieldEnergySlider.value > 0)
            {
                shieldEnergySlider.value -= shieldDecayTime * Time.deltaTime;
				shieldDisplay.text = shieldEnergySlider.value.ToString("N0");
            }
            else
            {
                shield.SetActive(false);
                _shieldDeployed = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            shield.SetActive(true);
            _shieldDeployed = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            shield.SetActive(false);
            _shieldDeployed = false;
        }

        // recharge energy
        if(laserEnergySlider.value < laserEnergySlider.maxValue)
        {
            if (!_mainAttackWeapon.IsFiring())
            {
                if (!_altAttackWeapon.IsFiring())
                {
                    laserEnergySlider.value = Mathf.Min(laserEnergySlider.value + energyRechargeRate * Time.deltaTime, laserEnergySlider.maxValue);
                    energyDisplay.text = laserEnergySlider.value.ToString("N0");
                }
            }

        }
        // recharge shield
        if(shieldEnergySlider.value < shieldEnergySlider.maxValue && !_shieldDeployed)
        {
            shieldEnergySlider.value += shieldRechargeRate * Time.deltaTime;
			shieldDisplay.text = shieldEnergySlider.value.ToString ("N0");
        }

        // Get left mouse button down input
        if (Input.GetMouseButtonDown (0)) 
		{
            if (laserEnergySlider.value - _mainAttackWeapon.EnergyToFire() >= 0)
            {
                laserEnergySlider.value -= _mainAttackWeapon.EnergyToFire();
				energyDisplay.text = laserEnergySlider.value.ToString ("N0");
                _mainAttackWeapon.Fire();
            }
		}
        else if(Input.GetMouseButton(0) && _mainAttackWeapon.IsFiring())
        {
            float energyValue = _mainAttackWeapon.EnergyPerSecond() * Time.deltaTime;

            if(laserEnergySlider.value - energyValue >= 0)
            {
                laserEnergySlider.value -= energyValue;
				energyDisplay.text = laserEnergySlider.value.ToString ("N0");
            }
            else
            {
                _mainAttackWeapon.StopFire();
            }
        }
		else if(Input.GetMouseButtonUp(0))
		{
			_mainAttackWeapon.StopFire ();
		}

        // Get right mouse button down input
        if (Input.GetMouseButtonDown(1))
        {
            if (laserEnergySlider.value - _altAttackWeapon.EnergyToFire() >= 0)
            {
                laserEnergySlider.value -= _altAttackWeapon.EnergyToFire();
				energyDisplay.text = laserEnergySlider.value.ToString ("N0");
                _altAttackWeapon.Fire();
            }
        }
        else if (Input.GetMouseButton(1) && _altAttackWeapon.IsFiring())
        {
            float energyValue = _altAttackWeapon.EnergyPerSecond() * Time.deltaTime;

            if (laserEnergySlider.value - energyValue >= 0)
            {
                laserEnergySlider.value -= energyValue;
				energyDisplay.text = laserEnergySlider.value.ToString ("N0");
            }
            else
            {
                _altAttackWeapon.StopFire();
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _altAttackWeapon.StopFire();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mainThrusterSlider.value > 1)
            {
                mainBoostParticleBitsClone = Instantiate(mainBoostParticleBits, transform.position, transform.rotation);
                Destroy(mainBoostParticleBitsClone, 5.0f);
            }
            mainThrusterSound.Stop();
            mainBoostSound.Play();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            mainThrusterSlider.value -= boostConsumptionRate * Time.deltaTime;
			boostDisplay.text = mainThrusterSlider.value.ToString ("N0");
            if (mainThrusterSlider.value > 1)
            {
                
                baseSpeed = boostSpeed;
            }
            else
            {
                mainBoostSound.Stop();
                if (!mainThrusterSound.isPlaying)
                {
                    mainThrusterSound.Play();
                }
                baseSpeed = _currentSpeed;
            }
        }
        // recharge thruster
        else
        {
            if (mainThrusterSlider.value < mainThrusterSlider.maxValue)
            {
                mainThrusterSlider.value += thrusterRechargeRate * Time.deltaTime;
				boostDisplay.text = mainThrusterSlider.value.ToString ("N0");
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            
            mainThrusterSound.Play();
            mainBoostSound.Stop();
            baseSpeed = _currentSpeed;
        }       
    } // end Update()

    void FixedUpdate()
    {
        _rgidBdy.AddForce(_force);
        _rgidBdy.velocity = Vector3.ClampMagnitude(_rgidBdy.velocity, baseSpeed);
        _rgidBdy.angularVelocity = Vector3.zero;
    }

	public float HealthPercentage()
	{
		return GlobalController.Instance.currentHealth / GlobalController.Instance.maxHealth;
	}

    private Vector3 MouseToXZPlane(Vector3 mousePos)
	{
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		return hit.point;
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


    // amount of time a player can hold a double-tap boost. 
    IEnumerator SideThrusterBoost()
    {
        yield return new WaitForSeconds(_boostLength);
        _doubleTapBoostA = 1;
        _doubleTapBoostD = 1;
        _timeSinceLastBoost = Time.time;
        sideBoostSound.Stop();
        sideThrusterSound.Play();
    }

    #region IDamageable implementation

    public bool TakeDamage(float damage)
    {

        bool blocked = false;
        if (_shieldDeployed)
        {
            if (damage > shieldEnergySlider.value)
            {
                _shieldDeployed = false;
                shieldEnergySlider.value = 0;
                blocked = TakeDamage(damage - shieldEnergySlider.value);
            }
            else
            {
                shieldHitParticleClone = Instantiate(shieldHitParticle, transform.position, transform.rotation);
                Destroy(shieldHitParticleClone, 2.0f);
                shieldEnergySlider.value -= (damage * 4);
				shieldDisplay.text = shieldEnergySlider.value.ToString ("N0");
                blocked = true;
            }
        }
        else
        {
            playerHitExplosionParticleClone = Instantiate(playerHitExplosionParticle, transform.position, transform.rotation);
            Destroy(playerHitExplosionParticleClone, 2.0f);
            GlobalController.Instance.currentHealth -= damage;
            healthSlider.value = GlobalController.Instance.currentHealth;
			healthDisplay.text = healthSlider.value.ToString ("N0");
            if (GlobalController.Instance.currentHealth <= 0f)
            {
                healthSlider.value = 0;
				level.GoToBase ();
                playerExplosionParticleClone = Instantiate(playerExplosionParticle, transform.position, transform.rotation) as GameObject;
                EnemySoundManager.Instance.playEnemyExplosionSound();
                Destroy(this.gameObject);
                Destroy(playerExplosionParticleClone, 1.5f);
            }
        }
        return blocked;
    }

    #endregion 
}
