using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLingHead : MonoBehaviour
{
    SpiderlingController spiderlingController;
    // Start is called before the first frame update
    void Start()
    {
        spiderlingController = GetComponentInParent<SpiderlingController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.transform.GetComponent<Bullet>();
            if (bullet.impacted == 0)
            {
                spiderlingController.Dead("bullet");

				ScoreManager.AddBulletHeadshotKill();
			}
               
        } else if(collision.CompareTag("Player"))
        {
            if(collision.transform.name.Equals("PlayerHuman"))
            {
				collision.gameObject.GetComponent<PlayerController>().Bounce();
				spiderlingController.Dead("hop");
			}
        }
    }
}
