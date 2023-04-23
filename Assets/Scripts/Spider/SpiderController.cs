using System.Collections;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
	public GameObject healthBar;
	public float maxHealth = 100;
	public float health = 100;
	public int deadScore = 1;
	public float speed = 10f;
	public AudioClip audio1, audio2, audio3;
	public bool sayOn;
	public string sayOnSaw, sayOnDead;
	public GameObject legs, head, blood, view;
	public bool seen, pdead, dead;

	private int yon;
	private AudioSource audioSource;
	private Rigidbody2D rigidbody2Da;
	[HideInInspector]
	public GameObject player;
	private Animator animator;
	private LevelManager levelManager;
	private int nextUpdate = 1;
	private Vector2 spawnpoint;
	private Hasar h;

	public bool firstBloodAchievemnt;
	// Start is called before the first frame update
	void Start()
	{
		h = GetComponent<Hasar>();

		spawnpoint = transform.position;
		levelManager = FindAnyObjectByType<LevelManager>();
		animator = GetComponent<Animator>();
		rigidbody2Da = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}

	public void Saw(GameObject collObject)
	{
		if (!seen)
		{
			player = collObject;
			pdead = false;
			audioSource.PlayOneShot(audio1);
			if (sayOn)
				levelManager.Say(sayOnSaw, .5f, true);
			if (!collObject.transform.name.Equals("PlayerUcak"))
				seen = true;
		}



	}

	public void Respawn()
	{
		pdead = true;
		seen = false;
		audioSource.Stop();
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(5);
			transform.position = spawnpoint;
		}
	}

	void UpdateEverySecond()
	{
		if (seen)
			yon = (int)(player.transform.position.x - transform.position.x);

	}

	public void Damage(float damage, int dtype)
	{
		health -= damage;


		Vector2 size = healthBar.transform.GetChild(0).localScale;
		if (health / maxHealth > 0)
			healthBar.transform.GetChild(0).localScale = new Vector2(health / maxHealth, size.y);
		else
			healthBar.transform.GetChild(0).localScale = new Vector2(0, size.y);
		healthBar.GetComponent<Animator>().Play("healthbarfade");

		audioSource.PlayOneShot(audio2);
		switch (dtype)
		{
			case 0:
				Instantiate(blood, this.transform.position, Quaternion.identity);
				break;
			case 1:
				break;
		}
		if (health < 0)
		{
			Dead(dtype);
		}
	}

	public void BounceBack()
	{
		seen = false;
		if (yon < 0)
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
			yield return new WaitForSeconds(1);
			if (!pdead && !dead)
				seen = true;
		}
	}

	void Update()
	{
		if (seen & !dead)
		{
			if (Time.time >= nextUpdate)
			{
				nextUpdate = Mathf.FloorToInt(Time.time) + 3;
				UpdateEverySecond();
			}

			if (yon < 0) //sol
			{
				rigidbody2Da.velocity = new Vector2(-speed, rigidbody2Da.velocity.y);
				if (!audioSource.isPlaying)
					audioSource.Play();
			}
			else //sað
			{
				rigidbody2Da.velocity = new Vector2(speed, rigidbody2Da.velocity.y);
				if (!audioSource.isPlaying)
					audioSource.Play();
			}

		}

	}

	public void Dead(int dtype)
	{
		dead = true;
		seen = false;
		audioSource.Stop();
		Destroy(rigidbody2Da);
		Destroy(GetComponent<BoxCollider2D>());
		GameObject.Destroy(gameObject, 5f);
		GameObject.Destroy(head);
		GameObject.Destroy(legs);
		GameObject.Destroy(view);
		if (levelManager.scoreEnabled)
			levelManager.Score("Örümcek öldürüldü.", deadScore);

		if (dtype == 0)
		{
			audioSource.PlayOneShot(audio3);
			animator.Play("SpiderDead");
		}
		else
		{
			audioSource.PlayOneShot(audio1);
			animator.Play("SpiderVaporize");
		}
		
		if (sayOn)
			levelManager.Say(sayOnDead, .5f, true);

		ScoreManager.AddSpiderKill(); 
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			Saw(collision.gameObject);
		}

	}
}
