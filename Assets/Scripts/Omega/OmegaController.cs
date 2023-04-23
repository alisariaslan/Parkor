using System.Collections;
using UnityEngine;

public class OmegaController : MonoBehaviour
{
	public GameObject healthbar;
	public int maxHealth = 100;
	public int health = 100;

	public int secondsBetweenAttacks = 3;
	public int randomizeValue = 3;
	public GameObject bleed, rock, flame;
	public AudioClip audio1, audio2, audio3;

	public bool sayOn;
	public string sayOnSaw, sayOnDead;

	private LevelManager levelManager;
	private Animator animator;
	private AudioSource audioSource;
	private bool deadState, firing;
	private float kalanX;
	private GameObject player;
	private SimsekManager simsekManager;
	private Rigidbody2D rigidbody2Da;
	private PlayerController playerController;
	private SpriteRenderer spriteRenderer;
	private Vector2 spawnpoint;


	// Start is called before the first frame update
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		levelManager = FindAnyObjectByType<LevelManager>();
		playerController = FindAnyObjectByType<PlayerController>();
		rigidbody2Da = GetComponent<Rigidbody2D>();
		simsekManager = FindAnyObjectByType<SimsekManager>();
		audioSource = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
		spawnpoint = transform.position;
	}
	void UpdateEverySecond()
	{
		Dondur();
		if (kalanX < 5 && kalanX > -5 && simsekManager != null)
		{
			animator.Play("omegaPulse");
		}
		else
		{
			int saldir = Random.Range(0, randomizeValue);
			switch (saldir)
			{

				case 0:
					if (simsekManager != null)
						animator.Play("omegaPulse");
					else
						return;
					break;
				default:
					animator.Play("omegaThrow");
					break;
			}
		}


	}

	public void PulseAttack()
	{
		simsekManager.Cak(false, transform.position);
		if (kalanX < 5 && kalanX > -5)
		{
			audioSource.PlayOneShot(audio1);

			playerController.Dead();
			CreateFlame(player.transform.position, new Vector2(5, 10));
		}
		else
		{
			CreateFlame(transform.position, new Vector2(10, 20));
		}

	}

	void CreateFlame(Vector2 pos, Vector2 size)
	{
		GameObject yeni = Instantiate(flame, pos, Quaternion.identity);
		yeni.transform.localScale = size;
		GameObject.Destroy(yeni, 3f);
	}
	public void ThrowAttack()
	{
		GameObject throwable = Instantiate(rock, transform.position, Quaternion.identity);
		GameObject.Destroy(throwable, 10f);
		Rigidbody2D rigidbody2Drock = throwable.GetComponent<Rigidbody2D>();
		//Debug.Log(kalanX);
		rigidbody2Drock.velocity = new Vector2(-kalanX, 10);
		rigidbody2Drock.AddTorque(20f, ForceMode2D.Force);
	}

	public void Dondur()
	{
		if (kalanX > 0)
			spriteRenderer.flipX = false;
		else
			spriteRenderer.flipX = true;
	}

	private int nextUpdate = 1;
	// Update is called once per frame
	void Update()
	{
		if (firing && !playerController.dead)
		{
			kalanX = transform.position.x - player.transform.position.x;
			kalanX = kalanX / 2;
			kalanX = ((int)kalanX);
			//Debug.Log(kalanX);
			//Debug.Log(kalanX);
			if (Time.time >= nextUpdate)
			{
				nextUpdate = Mathf.FloorToInt(Time.time) + secondsBetweenAttacks;
				UpdateEverySecond();
			}
		}

	}

	public void Respawn()
	{
		firing = false;
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(5);
			transform.position = spawnpoint;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && deadState == false && !firing)
		{
			player = collision.gameObject;
			firing = true;
			audioSource.PlayOneShot(audio1);
			if (sayOn)
				levelManager.Say(sayOnSaw, .5f, true);
		}
	}


	public void Damage(int hasar)
	{
		audioSource.PlayOneShot(audio2);
		health -= hasar;

		float oran = maxHealth - health;
		oran = maxHealth - oran;
		if (oran == 1 || oran < 1)
			oran = 0;
		Vector2 size = healthbar.transform.GetChild(0).localScale;
		healthbar.transform.GetChild(0).localScale = new Vector2(size.x / 100 * oran, size.y);
		healthbar.GetComponent<Animator>().Play("healthbarfade");

		if (health < 0)
			Dead();
	}

	public void Dead()
	{
		deadState = true;
		firing = false;
		audioSource.PlayOneShot(audio3);
		GameObject.Destroy(gameObject, 3f);
		rigidbody2Da.velocity = new Vector2(0, 20f);
		if (sayOn)
			levelManager.Say(sayOnDead, .5f, true);

		ScoreManager.AddOmegaKill();

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Bullet") && !deadState)
		{
			Bullet bullet = collision.transform.GetComponent<Bullet>();
			if (bullet.impacted == 0)
			{
				Instantiate(bleed, this.transform.position, Quaternion.identity);
				audioSource.Play();
				Damage(bullet.damage);

				int bulletBodyShots = PlayerPrefs.GetInt("bulletBodyShots", 0);
				bulletBodyShots++;
				PlayerPrefs.SetInt("bulletBodyShots", bulletBodyShots);

				GameObject.Destroy(bullet.gameObject);
			}

		}
	}


}
