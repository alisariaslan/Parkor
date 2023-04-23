
using System.Collections;
using UnityEngine;

public class KozaController : MonoBehaviour
{

    public GameObject healthbar;
    public float maxHealth = 300;
    public float health = 300;
    public int deadScore = 10;
    public GameObject spiderling;
    public SpriteRenderer spriteRenderer;
    public Sprite normal, acilmis, dead;
    private int nextUpdate = 1;
    public bool spawner;
    public AudioSource audioSource;
    public GameObject blood, kozaHead;
    private LevelManager levelManager;
    public AudioClip damaged;
    private void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }
    private void Update()
    {
        if (spawner)
        {
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + 10;
                UpdateEverySecond();
            }
        }

    }

    public void Respawn()
    {
        spawner = false;
        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            yield return new WaitForSeconds(5);
            foreach (SpiderlingController spiderlingController in GetComponentsInChildren<SpiderlingController>())
            {
                GameObject.Destroy(spiderlingController.gameObject);
            }
        }


    }


    private void UpdateEverySecond()
    {
        Spawn();
    }

    private void Spawn()
    {
        //Debug.Log("spawn");
        for (int i = 0; i < UnityEngine.Random.Range(1, 6); i++)
        {
            StartCoroutine(spawn());
            IEnumerator spawn()
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(1, 4));
                float xPos = UnityEngine.Random.Range(-3, 3);
                float yPos = UnityEngine.Random.Range(1, 5);
                Vector2 randomPos = new Vector2(transform.position.x + xPos, transform.position.y + yPos);

                spriteRenderer.sprite = acilmis;
                yield return new WaitForSeconds(.5f);
                GameObject spiderli = Instantiate(spiderling, randomPos, Quaternion.identity, transform);
                spiderli.transform.localScale = new Vector2(.5f, .5f);

                spriteRenderer.sprite = normal;

                Rigidbody2D rigidbody = spiderli.GetComponent<Rigidbody2D>();
                float xForce = UnityEngine.Random.Range(-5, 5);
                float yForce = UnityEngine.Random.Range(5, 15);
                rigidbody.AddForce(new Vector2(xForce, yForce));
                SpiderlingController spiderlingController = spiderli.GetComponent<SpiderlingController>();
                spiderlingController.Saw();
                spiderlingController.kozayabagil = true;

            }
        }
    }

    public void Damage(float hasar,string type)
    {
        health -= hasar;
        Instantiate(blood, this.transform.position, Quaternion.identity, transform.parent);
        audioSource.PlayOneShot(damaged);


        Vector2 size = healthbar.transform.GetChild(0).localScale;
        if(health/maxHealth>0)
        healthbar.transform.GetChild(0).localScale = new Vector2(health / maxHealth, size.y);
        else
            healthbar.transform.GetChild(0).localScale = new Vector2(0, size.y);
        healthbar.GetComponent<Animator>().Play("healthbarfade");


        if (health < 0)
        {
            GameObject.Destroy(kozaHead);
            spriteRenderer.sprite = dead;
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(this);
            GameObject.Destroy(gameObject, 3f);
            if (levelManager.scoreEnabled)
                levelManager.Score("Koza patlatildi.", deadScore);
           
            if (type.Equals("hop"))
			{
				ScoreManager.AddJumpKill();
			}
			ScoreManager.AddKozaKill();
               
               
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.name.Equals("PlayerHuman"))
            {
                spawner = true;
            }
            else if (collision.transform.name.Equals("PlayerCar"))
            {

            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            Bullet bullet = collision.transform.GetComponent<Bullet>();
            if (bullet.impacted == 0)
            {
                Damage(bullet.damage,"bullet");

				ScoreManager.AddBulletBodyshotKill();
			}
           
        }
        else if (collision.transform.CompareTag("explosion"))
        {
            Damage(1000,"exp");
        }
        else if (collision.transform.CompareTag("Player"))
        {
            if (collision.transform.name.Equals("PlayerUcak"))
            {
                Damage(1000,"plane");
            }

        }

    }
}
