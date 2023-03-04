using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Gameobjects")]
	public GameObject healthbar;
	public GameObject powerBar;
	public GameObject soundHelper;
	public GameObject blood;
	public GameObject fener;
	public GameObject isik;
	public GameObject pistol;
	public GameObject bullet;

	[Header("Ground Settings")]
	public Transform groundCheckPoint;
	public float groundCheckRadius;
	public LayerMask groundLayer;
	public LayerMask groundLayer2;

	[Header("Sound Sources")]
	public AudioSource audioSource;
	public AudioClip jumpSound;
	public AudioClip climbingStairsSound;
	public AudioClip gettingHitSound;
	public AudioClip deadSound;
	public AudioClip emptyGun;
	public AudioClip reloadGun;
	public AudioClip walkSound;
	public AudioClip concreteWalkSound;
	public AudioClip grassWalkSound;
	public AudioClip mudWalkSoundSound;
	public AudioClip woolWalkSound;
	public AudioClip woodWalkSound;
	public AudioClip woodStairsWalkSound;
	public AudioClip takeSound;

	[Header("Sound Levels")]
	public float WalkSoundVolume;
	public float JumpSoundVolume;

	[Header("Player Stats (These are controlled by the PlayerController script!)")]
	public bool moveable = false;
	public bool jumpable = false;
	public bool isTouchingGround = false;
	public bool itemEquipped = false;
	public bool jumping = false;
	public bool moving = false;
	public bool falling = false;
	public bool pause = false;
	public bool dead = false;
	public bool ladder = false;
	public bool inReplace = false;

	[Header("Player Attributes & Player Start Settings")]
	public bool god = false;
	public bool jumpEnabled = true;
	public bool runEnabled = false;
	public bool jumpAndRun = false;
	public bool takeFener = false;
	public bool takePistol = false;
	public bool noLeftAnim = false;
	public bool noRightAnim = false;

	[Header("Health & Speed & Damage")]
	public float maxHealth = 100;
	public float health = 100;
	public float speedX = 3f;
	public float jumpForce = 15f;
	public float jumpDamage = 150;

	[Header("Fener & Energy")]
	public float fenerEnergy = 100;
	public float maxFenerEnergy = 100;
	public float fenerEnergyRegen = .1f;
	public float fenerEnergyDrain = 1;
	public bool isInfinite = false;
	public int secsForInfinite = -1;

	[Header("Pistol & Ammo")]
	public int toplamMermi = 7;
	public int kalanMermi;

	private float horizontal = 0f;
	private float vertical = 0f;
	private int once = 0;
	private Animator animator;
	private LevelManager levelManager;
	private CanvasManager canvasManager;
	private CameraController cameraController;
	private Bullet bulletC;
	private Rigidbody2D rigidbody2Da;
	private AudioSource soundHelper_audioSource;
	private string activeWeapon = "";
	private bool release = false;

	private void Use(bool active)
	{
		if (activeWeapon == "Fener")
		{
			bool allow = false;
			if (active)
			{
				if (fenerEnergy > 0)
				{
					if (!isInfinite)
						fenerEnergy--;
					Vector2 vector2 = powerBar.transform.GetChild(0).transform.localScale;
					vector2 = new Vector2(fenerEnergy / maxFenerEnergy, vector2.y);
					powerBar.transform.GetChild(0).transform.localScale = vector2;
					powerBar.GetComponent<Animator>().Play("healthbarfade");
				}


			}
			else
			{
				if (fenerEnergy < maxFenerEnergy)
				{
					fenerEnergy += .1f;
					Vector2 vector2 = powerBar.transform.GetChild(0).transform.localScale;
					vector2 = new Vector2(fenerEnergy / maxFenerEnergy, vector2.y);
					powerBar.transform.GetChild(0).transform.localScale = vector2;
					powerBar.GetComponent<Animator>().Play("healthbarfade");
				}

			}
			if (fenerEnergy > 0)
			{
				allow = true;
			}
			else
			{
				allow = false;
			}
			if (allow)
			{
				isik.SetActive(active);
			}
			else
				isik.SetActive(false);
		}
		else if (activeWeapon == "Pistol")
		{
			if (active)
			{
				canvasManager.Sarjor.SetActive(true);
				if (horizontal > 0 || horizontal < 0)
				{
					if (kalanMermi > 0 && once == 0)
					{
						float yon = 1f;
						if (horizontal < 0)
						{
							yon = -1;
						}
						bulletC = bullet.GetComponent<Bullet>();
						bulletC.yon = yon;
						Instantiate(bullet, new Vector2(pistol.transform.position.x + .5f * yon, pistol.transform.position.y + 0.15f), Quaternion.identity);
						kalanMermi--;

						int gidecekler = 7 - kalanMermi;
						for (int i = 0; i < gidecekler; i++)
						{
							if (kalanMermi < 7)
								canvasManager.Sarjor.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
						}
						once = 1;
					}
					else if (kalanMermi == 0 && once == 0)
					{
						soundHelper_audioSource.PlayOneShot(emptyGun, 5f);
						once = 1;
					}

				}
			}

		}
	}

	public void Reload()
	{
		kalanMermi = toplamMermi;
		for (int i = 0; i < 7; i++)
		{
			canvasManager.Sarjor.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
		}
		soundHelper_audioSource.PlayOneShot(reloadGun, 5f);
	}

	public void ReLight(bool isSuper)
	{
		if (isSuper)
		{
			isInfinite = true;
			if (secsForInfinite > -1)
				StartCoroutine(LightsOut());
		}
		else
		{

		}
	}


	IEnumerator LightsOut()
	{
		yield return new WaitForSeconds(secsForInfinite);
		isInfinite = false;
	}

	public void NoItem()
	{
		levelManager.EscapePanel(false);
		activeWeapon = "";
		jumpEnabled = true;
		itemEquipped = false;
		fener.SetActive(false);
		pistol.SetActive(false);
	}

	public void ItemSet(GameObject weapon)
	{
		if (!ladder && isTouchingGround)
		{
			NoItem();
			jumpEnabled = false;
			weapon.SetActive(true);
			activeWeapon = weapon.name;
			itemEquipped = true;
		}
	}
	void Start()
	{
		cameraController = FindObjectOfType<CameraController>();
		levelManager = FindObjectOfType<LevelManager>();
		canvasManager = FindObjectOfType<CanvasManager>();
		rigidbody2Da = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		soundHelper_audioSource = soundHelper.GetComponent<AudioSource>();
		if (noLeftAnim)
			cameraController.facing = 1;
		if (noRightAnim)
			cameraController.facing = -1;
		if (!noLeftAnim && !noRightAnim)
			cameraController.facing = 1;
		EnableRun(runEnabled);
		speedX += 5;
		isTouchingGround = true;

		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(1);
			if (takePistol)
				ItemSet(pistol);

			if (takeFener)
				ItemSet(fener);
		}
	}

	public void EnableRun(bool active)
	{
		if (active)
		{
			animator.SetBool("run", active);
			speedX += 2;
		}
		else
		{
			animator.SetBool("run", active);
			speedX -= 5;
		}

	}

	public void Bounce()
	{
		rigidbody2Da.velocity = new Vector2(0, jumpForce);
	}

	public void Jump()
	{
		if (isTouchingGround && jumpable && !jumping)
		{
			jumpable = false;
			jumping = true;
			audioSource.Stop();
			soundHelper_audioSource.PlayOneShot(jumpSound, JumpSoundVolume);
			StartCoroutine(Block());
			IEnumerator Block()
			{
				yield return new WaitForSeconds(1);
				jumping = false;
				jumpable = true;
			}
			rigidbody2Da.velocity = new Vector2(0f, jumpForce);
		}
	}

	public void Dead()
	{
		if (!dead && !god)
		{
			Instantiate(blood, transform.position, Quaternion.identity);
			dead = true;
			pause = true;
			rigidbody2Da.constraints = RigidbodyConstraints2D.FreezePositionX;
			rigidbody2Da.constraints = RigidbodyConstraints2D.FreezeRotation;
			animator.Play("PlayerDead");
			audioSource.Stop();
			audioSource.PlayOneShot(deadSound);
			levelManager.Say("öldün!", .25f, false);
			levelManager.StopEnemies();
			if (levelManager.gotCheckpoint)
			{
				levelManager.Respawn(this.gameObject);
			}
			else
			{
				levelManager.Restart();

			}
		}
	}

	public void Spawn()
	{
		dead = false;
		pause = false;
		rigidbody2Da.constraints = RigidbodyConstraints2D.FreezeRotation;
		animator.Play("PlayerIdle");
		health = maxHealth;

		Vector2 size = healthbar.transform.GetChild(0).localScale;
		healthbar.transform.GetChild(0).localScale = new Vector2(health / maxHealth, size.y);
		healthbar.GetComponent<Animator>().Play("healthbarfade");
	}

	void Update()
	{
		if (!pause)
		{
			isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
			if (!isTouchingGround)
				isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer2);

			horizontal = Input.GetAxis("Horizontal");
			vertical = Input.GetAxis("Vertical");

			if (Input.GetKeyUp(KeyCode.Escape))
			{
				levelManager.EscapePanel();
			}
			if (Input.touchCount == 1)
			{
				Touch touch = Input.GetTouch(0);
				if (touch.position.y < Screen.height / 5)
				{
					vertical = Input.touches[0].maximumPossiblePressure;
				}
				else if (touch.position.y > Screen.height / 1.2f)
				{
					release = true;
				}
				else if (touch.position.x < Screen.width / 3)
				{
					horizontal = Input.touches[0].maximumPossiblePressure * -1;
				}
				else if (touch.position.x > Screen.width / 3)
				{
					horizontal = Input.touches[0].maximumPossiblePressure;
				}
			}
			else if (Input.touchCount == 2)
			{
				Touch touch1 = Input.GetTouch(0);
				Touch touch2 = Input.GetTouch(1);
				if (touch2.position.y < Screen.height / 5)
				{
					vertical = Input.touches[0].maximumPossiblePressure;
				}
				if (touch1.position.x < Screen.width / 3)
				{
					horizontal = Input.touches[0].maximumPossiblePressure * -1;
				}
				else if (touch1.position.x > Screen.width / 3)
				{
					horizontal = Input.touches[0].maximumPossiblePressure;
				}
			}
			else
			{
				if (release)
				{
					levelManager.EscapePanel();
					release = false;
				}
			}
			if (rigidbody2Da.velocity.y < -5f)
			{
				audioSource.Stop();
			}
			if (ladder & rigidbody2Da.velocity.x == 0)
			{
				if (vertical > 0f)
				{
					if (!audioSource.isPlaying)
						AudioPlay(climbingStairsSound, WalkSoundVolume, true);
					rigidbody2Da.velocity = new Vector2(0, 5);
					animator.SetBool("ladder", ladder);
				}
				else
				{
					animator.SetBool("ladder", false);
					audioSource.Stop();
				}
			}
			else
			{
				animator.SetBool("ladder", false);
			}
			if (horizontal > 0f & moveable)
			{
				moving = true;
				rigidbody2Da.velocity = new Vector2(horizontal * speedX, rigidbody2Da.velocity.y);
				if (!audioSource.isPlaying & isTouchingGround && !ladder)
				{
					AudioPlay(walkSound, WalkSoundVolume, true);

				}
				if (noRightAnim)
				{
					animator.SetInteger("facing", -1);

				}
				else
				{
					animator.SetInteger("facing", 1);
					if (isTouchingGround)
						cameraController.facing = 1;
				}
			}
			else if (horizontal < 0f & moveable)
			{
				moving = true;
				rigidbody2Da.velocity = new Vector2(horizontal * speedX, rigidbody2Da.velocity.y);
				if (!audioSource.isPlaying & isTouchingGround && !ladder)
				{
					AudioPlay(walkSound, WalkSoundVolume, true);

				}
				if (noLeftAnim)
				{
					animator.SetInteger("facing", 1);

				}
				else
				{
					animator.SetInteger("facing", -1);
					if (isTouchingGround)
						cameraController.facing = -1;
				}
			}
			else
			{
				moving = false;
				rigidbody2Da.velocity = new Vector2(0, rigidbody2Da.velocity.y);
				animator.SetInteger("facing", 0);
				if (isTouchingGround && !jumping && !moving)
					audioSource.Stop();
			}

			if (!ladder)
			{
				if (jumpEnabled)
				{
					if (!jumpAndRun)
					{
						if (vertical > 0 && isTouchingGround && jumpable && rigidbody2Da.velocity.x == 0)
						{
							Jump();

						}
					}
					else
					{
						if (vertical > 0 && isTouchingGround && jumpable)
						{
							Jump();
						}
					}
				}
				if (vertical > 0 && itemEquipped && !ladder)
				{

					Use(true);

				}
				else
				{
					if (itemEquipped)
					{
						Use(false);
						once = 0;
					}
				}
			}

			animator.SetFloat("speed", Mathf.Abs(rigidbody2Da.velocity.x));
			animator.SetBool("ongroun", isTouchingGround);
			animator.SetFloat("jumpSpeed", rigidbody2Da.velocity.y);
		}
		else
		{
			animator.StopPlayback();
		}
	}

	public void AudioPlay(AudioClip clip, float volume, bool loop)
	{
		audioSource.clip = clip;
		audioSource.loop = loop;
		audioSource.volume = volume;
		audioSource.Play();
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Wall"))
		{
			if (!isTouchingGround)
			{
				jumpable = false;
			}
			if (moving & !isTouchingGround)
			{
				moveable = false;
				jumpable = false;
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Wall"))
		{
			if (!isTouchingGround)
			{
				jumpable = false;
			}
			if (moving & !isTouchingGround)
			{
				moveable = false;
				jumpable = false;
			}
		}
		else
		{
			StartCoroutine(ExampleCoroutine());
			IEnumerator ExampleCoroutine()
			{
				yield return new WaitForSeconds(2);
				jumpable = true;
			}
			moveable = true;
			if (collision.transform.CompareTag("Ground") && !inReplace)
			{
				audioSource.Stop();
				walkSound = concreteWalkSound;

			}
			else if (collision.transform.CompareTag("DirtGround") && !inReplace)
			{
				audioSource.Stop();
				walkSound = mudWalkSoundSound;

			}
			else if ((collision.transform.CompareTag("GrassGround")) && !inReplace)
			{
				audioSource.Stop();
				walkSound = grassWalkSound;
			}
			else if ((collision.transform.CompareTag("WoodGround")) && !inReplace)
			{
				audioSource.Stop();
				walkSound = woodWalkSound;
				if (collision.transform.name.Equals("Tahta"))
				{
					if (rigidbody2Da.velocity.y < 0)
					{
						Tahta tahta = collision.transform.GetComponent<Tahta>();
						if (tahta != null)
							tahta.Break();

					}
				}
			}
			else if ((collision.transform.CompareTag("WoodStairs")) && !inReplace)
			{
				audioSource.Stop();
				walkSound = woodStairsWalkSound;
			}
			else if ((collision.transform.CompareTag("Caroet")) && !inReplace)
			{
				audioSource.Stop();
				walkSound = woolWalkSound;
			}
			else if (collision.transform.CompareTag("Item"))
			{
				switch (collision.transform.name)
				{
					case "FenerItem":
						ItemSet(fener);
						PlayerPrefs.SetInt("Envanter", 1);
						break;
					case "Pistol(item)":
						ItemSet(pistol);
						PlayerPrefs.SetInt("Envanter", 2);
						break;
					case "Sarjor":
						Reload();
						break;
					case "BatteryPackSuper":
						ReLight(true);
						soundHelper_audioSource.PlayOneShot(takeSound);
						break;
					case "HealthPack":
						health = maxHealth;
						UpdateHealthBar();
						soundHelper_audioSource.PlayOneShot(takeSound);
						break;
					default:
						break;
				}
				GameObject.Destroy(collision.gameObject);
			}
			else if (collision.transform.CompareTag("Enemy"))
			{
				TriggerColl(collision.gameObject);

			}
			else if (collision.transform.CompareTag("explosion"))
			{
				TriggerColl(collision.gameObject);

			}
			else if (collision.transform.CompareTag("HeatRock"))
			{
				HeatRock rock = collision.transform.GetComponent<HeatRock>();
				if (rock.deadly)
					Dead();
			} 
		}
	}

	public void TriggerColl(GameObject collision)
	{
		int yon;
		if (collision.transform.position.x < transform.position.x)
			yon = 1;
		else
			yon = -1;
		Hasar hasar = collision.transform.GetComponent<Hasar>();
		if (hasar != null)
			Damage(yon, hasar.hasar);
	}
	private void Damage(int yon, int hasar)
	{
		health -= hasar;

		Instantiate(blood, transform.position, Quaternion.identity);
		BounceBack(yon);
		UpdateHealthBar();

		if (health < 0)
		{
			Dead();
		}
	}

	private void UpdateHealthBar()
	{
		Vector2 size = healthbar.transform.GetChild(0).localScale;
		healthbar.transform.GetChild(0).localScale = new Vector2(health / maxHealth, size.y);
		if (health < 0)
			healthbar.transform.GetChild(0).localScale = new Vector2(0, size.y);
		healthbar.GetComponent<Animator>().Play("healthbarfade");
	}

	private void BounceBack(int yon)
	{
		audioSource.Stop();
		audioSource.PlayOneShot(gettingHitSound);
		pause = true;
		if (yon > 0)
		{
			rigidbody2Da.velocity = new Vector2(10, rigidbody2Da.velocity.y);
		}
		else
		{
			rigidbody2Da.velocity = new Vector2(-10, rigidbody2Da.velocity.y);
		}
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(.5f);
			if (!dead)
				pause = false;
		}
	}
}
