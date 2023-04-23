using UnityEngine;

public class SpiderBossSting : MonoBehaviour
{
	public AudioClip stingGroundSound;
	public AudioClip stingPlayerSound;
	public AudioClip rockBreakSound;
	public AudioClip brokenStingSound;
	public int lives = 3;
	public Sprite fullSting;
	public Sprite halfSting;
	public Sprite quarterSting;
	public bool say;
	public string afterBrokenSay;
	public SpiderBossHead spiderBossHead;

	private AudioSource audioSource;
	private SpriteRenderer spriteRenderer;
	private PlayerController playerController;
	private LevelManager levelManager;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		playerController = FindAnyObjectByType<PlayerController>();
		levelManager = FindAnyObjectByType<LevelManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log(collision.tag);
		if (collision.tag == "Ground")
		{
			audioSource.PlayOneShot(stingGroundSound);
		}
		else if (collision.tag == "Player")
		{
			if (collision.transform.name.Equals("PlayerHuman"))
			{
				audioSource.PlayOneShot(stingPlayerSound);
				if (!playerController.dead)
					playerController.Dead();
			}

		}
		else if (collision.tag == "Rock")
		{
			Destroy(collision.transform.gameObject);
			audioSource.PlayOneShot(rockBreakSound);
			lives--;
			if (lives == 2)
				spriteRenderer.sprite = fullSting;
			else if (lives == 1)
				spriteRenderer.sprite = halfSting;
			else if (lives == 0)
				spriteRenderer.sprite = quarterSting;
			else
			{
				audioSource.PlayOneShot(brokenStingSound);
				spriteRenderer.sprite = null;
				Destroy(this);
				if (say)
					levelManager.Say(afterBrokenSay, .7f, true);
				if (!spiderBossHead.isActiveAndEnabled)
					spiderBossHead.gameObject.SetActive(true);
				spiderBossHead.isStingBroken = true;
			}
		}
		else if (collision.tag.Equals("Bullet"))
		{
			Destroy(collision.transform.gameObject);
			lives--;
			audioSource.PlayOneShot(rockBreakSound);
			if (lives == 2)
				spriteRenderer.sprite = fullSting;
			else if (lives == 1)
				spriteRenderer.sprite = halfSting;
			else if (lives == 0)
				spriteRenderer.sprite = quarterSting;
			else if (lives < 0)
			{
				audioSource.PlayOneShot(brokenStingSound);
				spriteRenderer.sprite = null;
				Destroy(this);
				if (!spiderBossHead.isActiveAndEnabled)
					spiderBossHead.gameObject.SetActive(true);
				spiderBossHead.isStingBroken = true;

			}
		}
	}
}
