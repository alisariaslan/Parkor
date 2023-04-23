using UnityEngine;

public class SpiderLegs : MonoBehaviour
{
 
    private PlayerController playerController;
    private CarController carController;
    private SpiderController spiderController;
    private Rigidbody2D rigidbody2DParent;
    private Animator animator;
    private Bullet bullet;
    private bool pDead;
    AudioSource audioSource;
    Hasar hasar;
    // Start is called before the first frame update
    void Start()
    {
        hasar = GetComponent<Hasar>();
        playerController = FindAnyObjectByType<PlayerController>();
        carController = FindAnyObjectByType<CarController>();

        rigidbody2DParent = GetComponentInParent<Rigidbody2D>();
        spiderController = GetComponentInParent<SpiderController>();
        animator = GetComponentInParent<Animator>();
        //audioSource.GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spiderController.BounceBack();
            if (collision.transform.name.Equals("PlayerCar"))
            {
              
                if (carController.carHealth > 0)
                    carController.Damage(hasar.hasar);
                spiderController.Damage(52, 0);
            }
            else if (collision.transform.name.Equals("PlayerUcak"))
            {
                spiderController.Damage(1000, 0);
            } else if (collision.transform.name.Equals("PlayerHuman"))
            {
                playerController.TriggerColl(gameObject);
            }
        }
        else if (collision.CompareTag("Fener"))
        {
            spiderController.BounceBack();
            spiderController.Damage(collision.GetComponent<ISIKController>().damage,1);
        }
        else if (collision.CompareTag("Bullet"))
        {
            
            bullet = collision.GetComponent<Bullet>();
            if (bullet.impacted == 0)
            {
                if (spiderController.seen == false)
                    spiderController.Saw(GameObject.FindGameObjectWithTag("Player"));
                spiderController.BounceBack();
                spiderController.Damage(bullet.damage,0);
                GameObject.Destroy(collision.gameObject,.1f);

				ScoreManager.AddBulletBodyshotKill();
			}
            else
            {
                GameObject.Destroy(collision.gameObject, 1f);
            }
        }
        else if (collision.CompareTag("explosion"))
        {
            spiderController.Damage(1000, 0);
        }

    }
}

