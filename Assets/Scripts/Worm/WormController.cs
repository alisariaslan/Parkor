using System.Collections;
using UnityEngine;

public class WormController : MonoBehaviour
{
    public GameObject bleedingEffect;
    public GameObject healthBar;
    public int maxHealth = 100;
    public int health = 100;
    public int deadScore = 2;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround;
    public AudioClip hissesSound;
    private int nextUpdate = 1;
    public int updateTime = 10;
    public GameObject wormHead;
    public GameObject wormTailEnd;
    public Rigidbody2D wormHeadRigidbody2D;
    public float speed = 3f;
    public float jumpSpeed = 10f;
    private GameObject player;
    public GameObject wormEyes, head, back, back1, back2, back3, back4;
    public float kickbackPower = 10f;
    public Animator animator;
    private AudioSource audioSource;
    public bool seen;

    public bool sayOn;
    public string sayOnSaw;
    public string sayOnDead;

    private LevelManager levelManager;
    private Vector2 a, b, c, d, e, f;

    int yon = 1;
    int seriYon;
    bool unconsions;
    int before;

    void Start()
    {
        a = head.transform.position;
        b = back.transform.position;
        c = back1.transform.position;
        d = back2.transform.position;
        e = back3.transform.position;
        f = back4.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void Update()
    {
        if (seen)
        {


            isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
            //kafa 35   // tail 20
            if (isTouchingGround && !unconsions)
            {
                if (yon > 0)
                {
                    if (wormHead.transform.position.x < wormTailEnd.transform.position.x)
                        wormHeadRigidbody2D.velocity = new Vector2(jumpSpeed, jumpSpeed);
                }
                else
                {
                    if (wormHead.transform.position.x > wormTailEnd.transform.position.x)
                        wormHeadRigidbody2D.velocity = new Vector2(-jumpSpeed, jumpSpeed);
                }
            }
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + updateTime;
                UpdateEverySecond();
            }
            if (!unconsions)
                wormHeadRigidbody2D.velocity = new Vector2(speed * yon, this.wormHeadRigidbody2D.velocity.y);
            before = yon;
            seriYon = getYon(player, head);
        }
    }

    private void UpdateEverySecond()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        yon = getYon(player, head);
        if (yon != before)
            wormHeadRigidbody2D.velocity = new Vector2(this.wormHeadRigidbody2D.velocity.x, jumpSpeed);
    }

    public void Saw(GameObject player)
    {
        
        if (sayOn)
            levelManager.Say(sayOnSaw, .5f, true);
        if (player.name.Equals("PlayerHuman"))
        {
            seen = true;
        }
        else if (player.name.Equals("PlayerCar"))
        {

        }
        else if (player.name.Equals("PlayerUcak"))
        {
            audioSource.Play();
        }

    }

    public void Stop()
    {
        seen = false;
    }

    public void Respawn()
    {
        seen = false;
        StartCoroutine(r());
        IEnumerator r()
        {
            yield return new WaitForSeconds(5);
            head.transform.position = a;
            back.transform.position = b;
            back1.transform.position = c;
            back2.transform.position = d;
            back3.transform.position = e;
            back4.transform.position = f;
        }
    }

    public void Damage(int damage)
    {
        Instantiate(bleedingEffect, head.transform.position, Quaternion.identity);
        health -= damage;
        audioSource.PlayOneShot(hissesSound);
        float oran = maxHealth - health;
        oran = maxHealth - oran;
        if (oran == 1 || oran < 1)
            oran = 0;
        Vector2 size = healthBar.transform.GetChild(0).localScale;
        healthBar.transform.GetChild(0).localScale = new Vector2(size.x / 100 * oran, size.y);
        healthBar.GetComponent<Animator>().Play("healthbarfade");
    }
    public void KickBack(int damage)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        Damage(damage);
        animator.Play("wormuncons");
        unconsions = true;
        wormHeadRigidbody2D.velocity = new Vector2(kickbackPower * seriYon * -1, jumpSpeed * 2);
        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3);
            if (health > 0)
            {
                unconsions = false;
                animator.Play("wormidle");
            }
            else
            {
                seen = false;
                audioSource.Stop();
                GameObject.Destroy(gameObject, 3f);
                if (sayOn)
                    levelManager.Say(sayOnDead, .5f, true);
                if (levelManager.scoreEnabled)
                    levelManager.Score("Solucan öldürüldü.", deadScore);
                Destroy(GetComponentInChildren<Hasar>());
				ScoreManager.AddWormKill();
			}
        }
    }

    private int getYon(GameObject object1, GameObject object2)
    {
        int yonn;
        if (object1.transform.position.x > object2.transform.position.x)
            yonn = 1;
        else
            yonn = -1;
        return yonn;
    }


}
