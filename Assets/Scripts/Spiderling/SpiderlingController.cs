using System.Collections;
using UnityEngine;

public class SpiderlingController : MonoBehaviour
{
    public int deadScore = 1;
    public float speedX, jumpY, groundCheckRadius;
    public GameObject blood;
    public Rigidbody2D rigidbody2Da;
    public Transform groundCheckPoint;
    public LayerMask groundLayer;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip audio1, audio2, audio3;
    public bool seen, kozayabagil;

    private LevelManager levelManager;
    private GameObject player;
    private int yon;
    private int nextUpdate = 1;
    private bool dead, isTouchingGround, pdead;
    private Bullet bullet;
    private Vector2 spawnpoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnpoint = transform.position;
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isTouchingGround)
            animator.SetBool("onGround", true);
        else
            animator.SetBool("onGround", false);
        if (seen)
        {
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
                UpdateEverySecond();
            }

            if (yon == -1)
            {
                rigidbody2Da.velocity = new Vector2(-speedX, rigidbody2Da.velocity.y);
            }
            else
            {
                rigidbody2Da.velocity = new Vector2(speedX, rigidbody2Da.velocity.y);
            }
        }
    }
    private void UpdateEverySecond()
    {
        if (isTouchingGround)
        {
            Find();
            rigidbody2Da.velocity = new Vector2(rigidbody2Da.velocity.x, jumpY);
            audioSource.PlayOneShot(audio2);
        }
		if (transform.position.y < player.transform.position.y - 100)
			GameObject.Destroy(gameObject);
	}

    public void Respawn()
    {
        pdead = true;
        audioSource.Stop();
        seen = false;
        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            yield return new WaitForSeconds(5);
            transform.position = spawnpoint;
            pdead = false;
        }
    }
    public void BounceBack()
    {
        audioSource.Stop();
        seen = false;
        Find();
        rigidbody2Da.AddForce(new Vector2(-yon * 100, jumpY * 100f / 2));
        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            yield return new WaitForSeconds(3);
            if (!dead && !pdead)
            {
                seen = true;
                audioSource.Play();
            }
        }
    }
    public void Find()
    {
        if (player.transform.position.x < transform.position.x)
            yon = -1;
        else
            yon = 1;
    }

    public void Dead(string type)
    {
		if (dead)
			return;
        dead = true;
        int LayerIgnoreRaycast = LayerMask.NameToLayer("Spiderboss");
		gameObject.layer = LayerIgnoreRaycast;
		GameObject.Destroy(transform.GetChild(0).gameObject);
		GameObject b = Instantiate(blood, this.transform.position, Quaternion.identity, transform.parent);
        if (kozayabagil)
            b.transform.localScale = new Vector2(.5f, .5f);
        audioSource.Stop();
        audioSource.PlayOneShot(audio3);
        animator.Play("spiderlingDead");
        seen = false;
        rigidbody2Da.AddForce(new Vector2(-yon * 100, jumpY * 100f / 4));
        GameObject.Destroy(gameObject, 3f);
		GameObject.Destroy(this);
		ScoreManager.AddSpiderlingKill();
		if (type.Equals("hop"))
		{
			ScoreManager.AddJumpKill();
		}
        if (levelManager.scoreEnabled)
            levelManager.Score("Böcük", deadScore);
    }

    public void Saw()
    {
        audioSource.pitch = 3;
        audioSource.PlayOneShot(audio1);
        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            yield return new WaitForSeconds(1);
            audioSource.pitch = 2;
            if (seen && !pdead)
                audioSource.Play();
        }
        player = GameObject.FindGameObjectWithTag("Player");
        seen = true;
        Find();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !seen)
        {
            if(collision.transform.name.Equals("PlayerHuman"))
            {
                Saw();
            } 
            if (collision.transform.name.Equals("PlayerUcak"))
            {
                audioSource.pitch = 3;
                audioSource.PlayOneShot(audio1);
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            BounceBack();
        }
        else if (collision.transform.CompareTag("Bullet"))
        {
            bullet = collision.transform.GetComponent<Bullet>();
            if (bullet.impacted == 0)
            {
                Dead("bullet");
				ScoreManager.AddBulletBodyshotKill();

			} else
            {
                GameObject.Destroy(collision.gameObject, 1f);
            }
        }
    }

}
