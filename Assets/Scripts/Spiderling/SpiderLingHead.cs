using UnityEngine;

public class SpiderLingHead : MonoBehaviour
{
    SpiderlingController spiderlingController;

    void Start()
    {
        spiderlingController = GetComponentInParent<SpiderlingController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.transform.GetComponent<Bullet>();
            if (bullet.impacted == 0)
            {
                spiderlingController.Dead("bullet");
                ScoreManager.AddBulletHeadshot();
            }

        }
        else if (collision.CompareTag("Player"))
        {
            if (collision.transform.name.Equals("PlayerHuman"))
            {
                collision.gameObject.GetComponent<PlayerController>().Bounce();
                spiderlingController.Dead("hop");
            }
        }
        else if (collision.CompareTag("Fener"))
        {
            spiderlingController.Dead("light");
        }
    }
}
