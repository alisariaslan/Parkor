using System.Collections;
using UnityEngine;

public class CasperController : MonoBehaviour
{
	public bool dead;
	public bool seen;
	public int timecheck = 5;
	public int behindValue = 15;
	public float speed = 0.5f;
	public AudioClip casperDead;
	[Header("Messages")]
	public bool sayOn = false;
	public string sayOnSaw;
	public string sayOnDead;

	private LevelManager levelManager;
	private AudioSource audioSource;
	private Vector2 startpos;
	private PlayerController playerController;
	private GameObject player;
	private Animator animator;
	private float yon;
	private SpriteRenderer spriteRenderer;
	private Vector2 playerPosBehind;
	private int oldvalue;
	private int first = 0;
	private int nextUpdate = 0;

	void Start()
	{
		levelManager = FindObjectOfType<LevelManager>();
		audioSource = GetComponent<AudioSource>();
		startpos = this.transform.position;
		oldvalue = behindValue;
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
		playerController = FindObjectOfType<PlayerController>();
		player = GameObject.FindGameObjectWithTag("Player");
		animator = GetComponent<Animator>();

		if (dead)
			Dead();
	}

	void UpdateEverySecond()
	{
		CheckYon();
	}

	private void CheckYon()
	{

		animator.Play("casperWhoosh", 0);
		audioSource.Play();
		if (first == 1)
		{
			if (playerController.moving)
				behindValue = oldvalue * 2;
			else
				behindValue = oldvalue;
		}
		first = 1;

		if (yon > 1)
		{
			spriteRenderer.flipX = true;
			playerPosBehind = new Vector2(player.transform.position.x + behindValue, player.transform.position.y);
		}
		else
		{
			playerPosBehind = new Vector2(player.transform.position.x - behindValue, player.transform.position.y);
			spriteRenderer.flipX = false;
		}
	}

	void Update()
	{
		if (seen)
		{
			yon = (player.transform.position.x - transform.position.x);
			transform.position = Vector2.Lerp(transform.position, playerPosBehind, speed * Time.deltaTime);

			if (Time.time >= nextUpdate)
			{
				nextUpdate = Mathf.FloorToInt(Time.time) + timecheck;
				UpdateEverySecond();
			}
		}

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			seen = true;
			animator.enabled = true;
			if (sayOn)
				levelManager.Say(sayOnSaw, 0.5f, false);
		}
	}

	public void Respawn()
	{
		seen = false;
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(5);
			this.transform.position = startpos;
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			if (collision.transform.name.Equals("PlayerHuman"))
			{
				seen = false;
			}
		}
	}


	public void Dead()
	{
		if (sayOn)
			levelManager.Say(sayOnDead, .5f, true);
		Destroy(GetComponent<BoxCollider2D>());
		Destroy(this);
		GameObject.Destroy(this.gameObject, 3f);
		audioSource.PlayOneShot(casperDead);
		animator.Play("casperDead");
		int casperKills = PlayerPrefs.GetInt("casperKills", 0);
		casperKills++;
		PlayerPrefs.SetInt("casperKills", casperKills);
	}
}
