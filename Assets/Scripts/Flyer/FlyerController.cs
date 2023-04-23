using System.Collections;
using UnityEngine;

public class FlyerController : MonoBehaviour
{
	public int updateTime = 3;
	public int checkRadiusX = 5;
	public int checkRadiusY = 2;
	public float flyForceY = 5f;
	public float flyForceX = 5f;
	public bool enableAttack = true;
	public bool pause, dropped, seen, sayOn;
	public string sayOnFirstRockDrop;
	public GameObject rock;
	public AudioClip attack;
	public int playerOffsetY = 3;

	private int onceTimeDrop = 0;
	private int nextUpdate = 1;
	private float startposY, direction;
	private bool seenOnce, droppedd;

	private GameObject player;
	private GameObject other;
	private SpriteRenderer spriteRenderer;
	private LevelManager levelManager;
	private AudioSource audioSource;
	private Rigidbody2D rigidbody2Da;





	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		levelManager = FindAnyObjectByType<LevelManager>();
		audioSource = GetComponent<AudioSource>();
		rigidbody2Da = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

	}

	void UpdateEverySecond()
	{

		if (other != null)
		{

			dropped = false;

			if (transform.position.x - other.transform.position.x < 0)
			{
				direction = 1;
				spriteRenderer.flipX = true;
			}

			else
			{
				direction = -1;
				spriteRenderer.flipX = false;
			}

			seen = true;

		}


	}

	public Rigidbody2D GetVelocity()
	{
		return rigidbody2Da;
	}



	void Update()
	{
		if (seenOnce)
		{
			if (Time.time >= nextUpdate)
			{
				nextUpdate = Mathf.FloorToInt(Time.time) + updateTime;
				UpdateEverySecond();
			}
		}

		startposY = player.transform.position.y + playerOffsetY;

		if (transform.position.y < startposY)
		{
			rigidbody2Da.velocity = new Vector2(rigidbody2Da.velocity.x, flyForceY);
		}
		if (seen)
		{

			rigidbody2Da.velocity = new Vector2(flyForceX * direction, rigidbody2Da.velocity.y);
		}
		else
		{
			rigidbody2Da.velocity = new Vector2(0f, rigidbody2Da.velocity.y);
		}

		if (!dropped)
		{

			float playerX = player.transform.position.x;
			float playerY = player.transform.position.y;
			float thisX = transform.position.x;
			float thisY = transform.position.y;
			if (thisX - playerX < checkRadiusX & thisX - playerX > -checkRadiusX)
			{
				if (thisY - playerY > checkRadiusY)
				{
					//Debug.Log("Drop!!");
					if (enableAttack)
					{
						if (!droppedd)
						{
							GameObject heatrock = Instantiate(rock, this.transform.position, Quaternion.identity, null);
							GameObject.Destroy(heatrock, 10f);
							Rigidbody2D rigidbody2Drock = heatrock.GetComponent<Rigidbody2D>();
							rigidbody2Drock.AddTorque(20f, ForceMode2D.Force);
							rigidbody2Drock.velocity = new Vector2(GetVelocity().velocity.x / 5, 0f);
							if (onceTimeDrop == 0 && sayOn)
								levelManager.Say(sayOnFirstRockDrop, .5f, true);
							onceTimeDrop = 1;
							dropped = true;
							droppedd = true;

						}
						StartCoroutine(Wait());
						IEnumerator Wait()
						{
							yield return new WaitForSeconds(2);
							droppedd = false;
						}

					}

				}
			}
		}


	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") | collision.CompareTag("Car"))
		{
			if (!seenOnce)
			{
				audioSource.Play();
				audioSource.PlayOneShot(attack);
				other = collision.gameObject;
				seenOnce = true;

			}

		}
	}

}
