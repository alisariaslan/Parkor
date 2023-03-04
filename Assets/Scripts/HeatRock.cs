using System.Collections;
using UnityEngine;

public class HeatRock : MonoBehaviour
{
	public int damage = 150;
	public AudioClip audio1, audio2, audio3;
	public GameObject parlama;
	public bool deadly = true;
	public bool destroyAfterSec = true;
	public bool destroyAfterColl = false;
	public int destroyTime = 10;
	public bool autoCoolingEnabled = true;
	public bool onlyCoolAfterHitGround = false;
	public int delayCooling = 3;
	public bool changeTagAfterColl;
	public string newTag;

	private bool contacted;
	private AudioSource audioSource;
	private Animator animator;
	private CarController carController;
	private Rigidbody2D rigidbody2Da;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
		rigidbody2Da = GetComponent<Rigidbody2D>();
		carController = FindObjectOfType<CarController>();

		if (audioSource != null)
			audioSource.Play();

		if (autoCoolingEnabled)
			StartCoroutine(StartCool());

		if (destroyAfterSec)
			Eliminate();
	}

	public void Cool()
	{
		deadly = false;
		GameObject.Destroy(parlama);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Player")
		{
			rigidbody2Da.velocity = new Vector2(rigidbody2Da.velocity.x, 5);
			if (collision.transform.name.Equals("PlayerHuman"))
			{
				if (!audioSource.isPlaying && deadly && audioSource != null)
					audioSource.PlayOneShot(audio1);
			}
			else if (collision.transform.name.Equals("PlayerCar"))
			{
				if (audioSource != null)
					audioSource.PlayOneShot(audio1);
				if (carController.carHealth > 0 && deadly)
					carController.Damage(10);
				deadly = false;
			}

		}
		else if (collision.transform.tag == "Ground")
		{
			if (!audioSource.isPlaying && deadly && !contacted && audioSource != null)
			{
				audioSource.PlayOneShot(audio1);
				StartCoroutine(StartCool());
				if (destroyAfterColl)
					Eliminate();
				if (changeTagAfterColl)
					tag = newTag;
			}
			contacted = true;
		}
	}


	IEnumerator StartCool()
	{
		yield return new WaitForSeconds(delayCooling);
		animator.enabled = true;
	}

	public void Eliminate()
	{
		GameObject.Destroy(gameObject, destroyTime);
	}

}
