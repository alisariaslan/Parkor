using UnityEngine;

public class FlyerBody : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    public GameObject flyer;
    public GameObject healthBar;
    private Animator animator;
    private Rigidbody2D rigidbody2Da;
    private AudioSource audioSource, mainAudioSource;
    private bool dead;


    void Start()
    {
        mainAudioSource = flyer.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        animator = flyer.GetComponent<Animator>();
        rigidbody2Da = flyer.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.transform.position = flyer.transform.position;
    }

    public void Damage(float hasar)
    {
        health -= hasar;
        healthBar.SetActive(true);
        Vector2 healthBarSize = healthBar.transform.GetChild(0).localScale;
        if (health / maxHealth > 0)
            healthBar.transform.GetChild(0).localScale = new Vector2(health / maxHealth, healthBarSize.y);
        else
            healthBar.transform.GetChild(0).localScale = new Vector2(0, healthBarSize.y);
        

        audioSource.Play();
        rigidbody2Da.velocity = new Vector2(rigidbody2Da.velocity.x, 15f);
        if (health < 0)
        {
            dead = true;
            mainAudioSource.Stop();
            animator.Play("flyerDead");
            FlyerController flyerController = flyer.GetComponent<FlyerController>();
            flyerController.enableAttack = false;
            rigidbody2Da.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(transform.parent.gameObject, 3f);

			ScoreManager.AddFlyerKill();


		}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dead)
        {

             if (collision.CompareTag("Fener"))
            {
                Damage(collision.GetComponent<ISIKController>().damage);
                

            }
        }
    }
}
