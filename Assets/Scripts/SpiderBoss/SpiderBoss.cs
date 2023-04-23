using System.Collections;
using UnityEngine;

public class SpiderBoss : MonoBehaviour
{
	public AudioClip wakeUpSound;
	public AudioClip deadSound;
	public AudioClip spiderRoar;
	public float walkSpeed = 3f;
	public bool startDead = false;
	public bool startWaked = false;
	public int delayWake = 0;
	public bool say = false;
	public string sayAfterWake;
	public string sayAfterDead;
	public bool muteLevelAudioAfterDeath = false;

	[Header("1: to Right, -1 to Left")]
	public int yon = 1;

	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody2D rigidbody2Da;
	private LevelManager levelManager;
	private BossDeadGateOpen bossdeadgateopen;
	private bool walkable;
	private bool isWaked = false;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody2Da = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		levelManager = FindAnyObjectByType<LevelManager>();
		bossdeadgateopen = FindAnyObjectByType<BossDeadGateOpen>();
		if (startWaked)
			StartCoroutine(wakeee());
	}

	IEnumerator wakeee()
	{
		yield return new WaitForSeconds(delayWake);
		WakeUp();
	}

	// Update is called once per frame
	void Update()
	{
		if (walkable)
		{
			if (yon < 1) //sol
			{
				rigidbody2Da.velocity = new Vector2(-walkSpeed, rigidbody2Da.velocity.y);
				if (!audioSource.isPlaying)
					audioSource.Play();
			}
			else //sag
			{
				rigidbody2Da.velocity = new Vector2(walkSpeed, rigidbody2Da.velocity.y);
				if (!audioSource.isPlaying)
					audioSource.Play();
			}
		}
	}

	public void Dead()
	{
		animator.Play("spiderboss_dead", 0);
		walkable = false;
		audioSource.Stop();
		audioSource.PlayOneShot(deadSound);
		Destroy(this.transform.gameObject, 5f);
		Destroy(this.transform.GetChild(0).gameObject);
		Destroy(this.transform.GetChild(1).gameObject);
		Destroy(this.transform.GetChild(2).gameObject);
		
		if (bossdeadgateopen != null)
			bossdeadgateopen.Op();

		if (muteLevelAudioAfterDeath)
			levelManager.MuteAudio();
		if (say)
			levelManager.Say(sayAfterDead, .5f, true);

		ScoreManager.AddBossKill();
		ScoreManager.AddJumpKill();
	}
	public void WakeUp()
	{
		if (isWaked)
			return;

		if (startDead)
			Dead();
		else
		{
			animator.SetBool("wakeup", true);
			audioSource.PlayOneShot(wakeUpSound);
			if (say)
				levelManager.Say(sayAfterWake, .5f, true);
			StartCoroutine(Wakeup());

			IEnumerator Wakeup()
			{
				yield return new WaitForSeconds(5);
				walkable = true;
				animator.SetBool("walkable", true);
				animator.SetInteger("yon", yon);
			}
		}
		isWaked = true;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Wall")
		{
			yon *= -1;
			animator.SetInteger("yon", yon);
		}
	}
}
