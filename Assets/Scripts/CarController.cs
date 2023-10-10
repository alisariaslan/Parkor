using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
	[Header("Car Status")]
	public bool frozen;
	public bool regenable;

	[Header("Car General Settings")]
	public float speed;
	public float torque;

	[Header("Car Health Settings")]
	public float maxHealth = 100;
	public float carHealth = 100;
	public int regenValue = 10;
	public int regenRateSeconds = 10;

	[Header("Far Energy Settings")]
	public float maxEnergy = 100;
	public float energy = 100;
	public float energyRegen = .1f;
	public float energyDrain = 1f;
	public int energyRegenDelay = 3;
	
	[Header("Game Objects:")]
	public GameObject healthBar;
	public GameObject energyBar;

	[Header("Audio Clips:")]
	public AudioClip engineStart;
	public AudioClip engineOff;
	public AudioClip engine;
	public AudioClip pedal;
	public AudioClip idle;
	public AudioClip crash;

	private AudioSource engineSource;
	private AudioSource audioSource;
	private AudioSource farAudioSource;
	private Rigidbody2D myRigidBody2d;
	private GameObject engineGameObject;
	private GameObject arka, far, on;
	private WheelJoint2D arkaT, onT;
	private SpriteRenderer spriteRenderer;
	private float horizontal = 0f;
	private float vertical = 0f;
	private int a, f;
	private JointMotor2D JointMotor;
	private int nextUpdate = 1;
	private LevelManager levelManager;
	private CanvasManager canvasManager;

    [Header("Buttons")]
    public ControllersButtonScript gotoleftButtonScript, gotorightButtonScript, jumpButtonScript;

    public void Heal(float giveHealth)
	{
		if (carHealth < maxHealth)
			carHealth += giveHealth;
		if (carHealth > maxHealth)
			carHealth = 100;
		spriteRenderer.color = new Color(1, carHealth * 0.01f, carHealth * 0.01f);
		transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (maxHealth - carHealth) / 100);
		UpdateHealthBar();
	}

	private void UpdateHealthBar()
	{
		Vector2 healthBarSize = healthBar.transform.GetChild(0).localScale;
		if (carHealth / maxHealth > 0)
			healthBar.transform.GetChild(0).localScale = new Vector2(carHealth / maxHealth, healthBarSize.y);
		else
			healthBar.transform.GetChild(0).localScale = new Vector2(0, healthBarSize.y);
		healthBar.GetComponent<Animator>().Play("healthbarfade");
	}
	private void UpdateEnergyBar()
	{

		Vector2 energyBarSize = energyBar.transform.GetChild(0).localScale;
		if (energy / maxEnergy > 0)
			energyBar.transform.GetChild(0).localScale = new Vector2(energy / maxEnergy, energyBarSize.y);
		else
			energyBar.transform.GetChild(0).localScale = new Vector2(0, energyBarSize.y);
		energyBar.GetComponent<Animator>().Play("healthbarfade");
	}
	public void Damage(int loss)
	{
		carHealth -= loss;
		UpdateHealthBar();
		spriteRenderer.color = new Color(1, carHealth * 0.01f, carHealth * 0.01f);
		transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (maxHealth - carHealth) / 100);
		if (carHealth <= 0)
		{
			Crash();
			levelManager.Restart();
		}
	}

	private void Crash()
	{
		frozen = true;

		speed = 0;
		torque = 0;
		JointMotor.motorSpeed = speed * horizontal;
		JointMotor.maxMotorTorque = torque;
		arkaT.motor = JointMotor;
		onT.motor = JointMotor;
		far.SetActive(false);

		engineSource.Stop();
		audioSource.Stop();
		audioSource.PlayOneShot(crash);
	}

	private void UpdateEverySecond()
	{
		if (carHealth < maxHealth)
		{
			carHealth += regenValue;
			spriteRenderer.color = new Color(1, carHealth * 0.01f, carHealth * 0.01f);
			transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (maxHealth - carHealth) / 100);
			UpdateHealthBar();
		}
	}

	void Start()
	{
		levelManager = FindAnyObjectByType<LevelManager>();
		canvasManager = FindAnyObjectByType<CanvasManager>();

		levelManager = FindAnyObjectByType<LevelManager>();
		myRigidBody2d = GetComponent<Rigidbody2D>();
		engineGameObject = GameObject.Find("Engine");
		engineSource = engineGameObject.GetComponent<AudioSource>();
		a = 0;
		audioSource = GetComponent<AudioSource>();
		audioSource.PlayOneShot(engineStart);
		engineSource.clip = idle;
		engineSource.PlayDelayed(1f);
		arka = GameObject.Find("Arka");
		on = GameObject.Find("On");
		far = GameObject.Find("Far");
		farAudioSource = far.GetComponent<AudioSource>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		arkaT = arka.GetComponent<WheelJoint2D>();
		onT = on.GetComponent<WheelJoint2D>();
	}

	public void BounceBack()
	{
		frozen = true;
		if (horizontal > 0)
		{
			myRigidBody2d.velocity = new Vector2(-10, myRigidBody2d.velocity.y);
		}
		else
		{
			myRigidBody2d.velocity = new Vector2(10, myRigidBody2d.velocity.y);
		}
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(1);
			frozen = false;
		}
	}

	void Update()
	{
		if (!frozen)
		{
			if (regenable)
			{
				if (Time.time >= nextUpdate)
				{
					nextUpdate = Mathf.FloorToInt(Time.time) + regenRateSeconds;
					UpdateEverySecond();
				}
			}

			horizontal = Input.GetAxis("Horizontal");
			vertical = Input.GetAxis("Vertical");

			if (Input.GetKeyUp(KeyCode.Escape))
			{
				canvasManager.OpenMenu();
            }
            if (Input.GetKeyUp(KeyCode.BackQuote))
            {
                canvasManager.OpenConsole();
            }
            if (Application.isMobilePlatform || levelManager.forceMobile)
            {
                if (gotoleftButtonScript.buttonPressed)
                    horizontal = -1;
                else if (gotorightButtonScript.buttonPressed)
                    horizontal = 1;
                if (jumpButtonScript.buttonPressed)
                    vertical = 1;
            }

            if (horizontal > 0)
			{

				if (!audioSource.isPlaying)
				{
					a = 1;
					audioSource.Play();
					engineSource.PlayOneShot(pedal);
					engineSource.clip = engine;
					engineSource.PlayDelayed(1.73f);
				}

				JointMotor.motorSpeed = speed * horizontal;
				JointMotor.maxMotorTorque = torque;
			}
			else if (horizontal < 0)
			{

				if (!audioSource.isPlaying)
				{
					a = 1;

					audioSource.Play();
					engineSource.PlayOneShot(pedal);
					engineSource.clip = engine;
					engineSource.PlayDelayed(1.73f);
				}

				JointMotor.motorSpeed = speed * horizontal;
				JointMotor.maxMotorTorque = torque;
			}
			else
			{
				if (a == 1)
				{
					JointMotor.motorSpeed = speed * horizontal;
					JointMotor.maxMotorTorque = torque;
					audioSource.Stop();
					engineSource.Stop();
					engineSource.PlayOneShot(engineOff, 1);
					engineSource.clip = idle;
					engineSource.Play();
					a = 0;
				}
			}
			if (vertical > 0)
			{
				if (energy > 0)
					energy-=energyDrain;
				UpdateEnergyBar();
				if (energy <= 0)
				{
					far.SetActive(false);
					waitEnergyLoad = true;
					StartCoroutine(ExampleCoroutine());
					IEnumerator ExampleCoroutine()
					{
						yield return new WaitForSeconds(energyRegenDelay);
						waitEnergyLoad = false;
					}
				}
				else
				{
					far.SetActive(true);
					if (f == 0)
					{
						farAudioSource.Play();
						f = 1;
					}
				}
			}
			else
			{
				if (energy < maxEnergy && !waitEnergyLoad)
				{
					energy += energyRegen;
					UpdateEnergyBar();
				}
				far.SetActive(false);
				f = 0;
			}
			arkaT.motor = JointMotor;
			onT.motor = JointMotor;
		}
	}
	private bool waitEnergyLoad;
}
