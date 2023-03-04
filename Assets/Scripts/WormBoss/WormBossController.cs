using System.Collections;
using UnityEngine;

public class WormBossController : MonoBehaviour
{
	[HideInInspector]
	public GameObject destroy;
	[HideInInspector]
	public bool aktarmaTamam;

	public AudioClip hello, ac1, ac2, ac3, ac4, ac5;
	public bool pause;
	public bool playerKilled, dead;
	public bool readyFight = false;
	public bool sawAtStart = false;
	public bool fightMusic = true;
	public GameObject head, back, back1, back2, back3, back4;
	public GameObject tailEnd;
	public bool letsGO = false;
	public float partCount = 5;

	[Header("Messages")]
	public bool sayOn = false;
	public string sayOnSaw;
	public string sayOnDead;

	private GameObject player;
	private AudioSource audioSource;
	private Vector2 sp1, sp2, sp3, sp4, sp5, sp6;
	private WormBossEyes wormBossEyes;
	private Rigidbody2D rigid_head, rigid_back, rigid_back1, rigid_back2, rigid_back3, rigid_back4;
	private PlayerController playerController;
	private LevelManager levelManager;
	private int nextUpdate = 1;
	private int pyon = -1;
	private int tyon = -1;
	private float power = 1;

	public void PlayerKilled()
	{
		if (!playerKilled)
		{
			playerKilled = true;
			playerController.Dead();
			letsGO = false;
			StartCoroutine(ExampleCoroutine());
			IEnumerator ExampleCoroutine()
			{
				yield return new WaitForSeconds(5);
				playerKilled = false;
				wormBossEyes.seen = 1;
				audioSource.Stop();
				head.transform.position = sp1;
				if (back != null)
					back.transform.position = sp2;
				if (back1 != null)
					back1.transform.position = sp3;
				if (back2 != null)
					back2.transform.position = sp4;
				if (back3 != null)
					back3.transform.position = sp5;
				if (back4 != null)
					back4.transform.position = sp6;

			}
		}
	}

	public void Dead()
	{
		Destroy(this);
		audioSource.PlayOneShot(ac5);
		dead = true;
		letsGO = false;
		GameObject.Destroy(gameObject, 5f);

		if (sayOn)
			levelManager.Say(sayOnDead, .5f, true);

		GameObject.Destroy(destroy);
		GameObject[] flames = GameObject.FindGameObjectsWithTag("Flame");
		foreach (var item in flames)
		{
			GameObject.Destroy(item, Random.Range(5, 15));
		}

		ScoreManager.AddBossKill();
	}

	void Start()
	{
		wormBossEyes = GetComponentInChildren<WormBossEyes>();
		sp1 = head.transform.position;
		sp2 = back.transform.position;
		sp3 = back1.transform.position;
		sp4 = back2.transform.position;
		sp5 = back3.transform.position;
		sp6 = back4.transform.position;
		levelManager = FindObjectOfType<LevelManager>();
		playerController = FindObjectOfType<PlayerController>();
		audioSource = GetComponent<AudioSource>();
		rigid_head = head.GetComponent<Rigidbody2D>();
		rigid_back = back.GetComponent<Rigidbody2D>();
		rigid_back1 = back1.GetComponent<Rigidbody2D>();
		rigid_back2 = back2.GetComponent<Rigidbody2D>();
		rigid_back3 = back3.GetComponent<Rigidbody2D>();
		rigid_back4 = back4.GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		if (readyFight)
		{

			head.GetComponent<Animator>().enabled = false;
			back.GetComponent<Animator>().enabled = false;
			back1.GetComponent<Animator>().enabled = false;
			back2.GetComponent<Animator>().enabled = false;
			back3.GetComponent<Animator>().enabled = false;
			back4.GetComponent<Animator>().enabled = false;

			head.transform.localScale = new Vector3(10, 10, 1);
			back.transform.localScale = new Vector3(10, 10, 1);
			back1.transform.localScale = new Vector3(10, 10, 1);
			back2.transform.localScale = new Vector3(10, 10, 1);
			back3.transform.localScale = new Vector3(10, 10, 1);
			back4.transform.localScale = new Vector3(10, 10, 1);
		}
		if (sawAtStart)
			Seen();
		if (!fightMusic)
			wormBossEyes.musicOff = true;
	}

	public void Seen()
	{
		if (!readyFight)
			if (!aktarmaTamam)
				head.GetComponent<Animator>().Play("getBigger", 1);
			else Fight();

		else
			Fight();

		if(sayOn)
			levelManager.Say(sayOnSaw, .5f, true);
		wormBossEyes.seen = 2;
	}
	private void AyaklanVeSicra(int yon)
	{
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(0);
			audioSource.PlayOneShot(ac2);
			rigid_head.velocity = new Vector2(rigid_head.velocity.x, 6f * partCount * power);
			yield return new WaitForSeconds(1);
			audioSource.PlayOneShot(ac3);
			rigid_head.velocity = new Vector2(16f * partCount * yon * power, rigid_head.velocity.y);
		}
	}
	private void Surun(int yon)
	{
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(0);
			audioSource.PlayOneShot(ac1);
			rigid_head.velocity = new Vector2(16f * partCount * yon * power, rigid_head.velocity.y);

		}
	}
	private void Don(int yon)
	{
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(0);
			audioSource.PlayOneShot(ac2);
			rigid_head.velocity = new Vector2(rigid_head.velocity.x, 10f * partCount * power);
			rigid_head.velocity = new Vector2(6f * partCount * yon * power, rigid_head.velocity.y);
		}
	}
	private void Sicra(int yon)
	{
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(0);
			audioSource.PlayOneShot(ac2);
			rigid_All(new Vector2(2f * partCount * yon, 2f * partCount * power));
		}
	}
	public void Fight()
	{
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			if (!readyFight & !aktarmaTamam)
				yield return new WaitForSeconds(5);
			audioSource.Play();
			audioSource.PlayOneShot(hello);

			yield return new WaitForSeconds(3);
			letsGO = true;
		}
	}
	public void rigid_All(Vector2 vector2)
	{
		rigid_head.velocity = vector2;
		if (back != null)
			rigid_back.velocity = vector2;
		if (back1 != null)
			rigid_back1.velocity = vector2;
		if (back2 != null)
			rigid_back2.velocity = vector2;
		if (back3 != null)
			rigid_back3.velocity = vector2;
		if (back4 != null)
			rigid_back4.velocity = vector2;
	}

	private void UpdateEverySecond()
	{

		if (tyon == pyon)
		{
			switch (Random.Range(0, 3))
			{
				case 0: AyaklanVeSicra(pyon); break;
				case 1: Surun(pyon); break;
				case 2: Sicra(pyon); break;
				default:
					AyaklanVeSicra(pyon);
					break;
			}
		}
		else
			Don(pyon);
	}

	void Update()
	{
		if (!dead)
		{
			switch (partCount)
			{
				case 4: power = 1.2f; break;
				case 3: power = 1.4f; break;
				case 2: power = 1.6f; break;
				case 1: power = 1.8f; break;
				case 0: power = 2f; break;
			}

			if (head != null)
			{
				if (head.transform.position.x > player.transform.position.x)
					pyon = -1;
				else
					pyon = 1;
				if (head.transform.position.x > tailEnd.transform.position.x)
					tyon = 1;
				else
					tyon = -1;
			}

			if (Time.time >= nextUpdate)
			{
				nextUpdate = Mathf.FloorToInt(Time.time) + 5; //SECOND
				if (letsGO)
					UpdateEverySecond();
			}
		}
	}
}
