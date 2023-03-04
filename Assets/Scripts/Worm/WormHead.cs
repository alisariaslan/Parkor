using System.Collections;
using UnityEngine;

public class WormHead : MonoBehaviour
{
   
    private ISIKController isikcontroller;
    int wait = 0;
    private Bullet bullet;
    private WormController wormController;
    private PlayerController playerController;
    void Start()
    {
        wormController = GetComponentInParent<WormController>();
        playerController = FindObjectOfType<PlayerController>();
    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fener"))
        {
            if (wait == 0)
            {
                isikcontroller = collision.GetComponent<ISIKController>();
                wormController.KickBack(isikcontroller.damage);
                wait = 1;
                StartCoroutine(Wait());
                IEnumerator Wait()
                {
                    yield return new WaitForSeconds(3);
                    wait = 0;
                }
            }

        }
        else if (collision.CompareTag("Player"))
        {
            if (collision.transform.name.Equals("PlayerHuman"))
            {
                if(playerController.dead)
                wormController.KickBack(0);
            } else if (collision.gameObject.name.Equals("PlayerCar"))
            {
                wormController.KickBack(51);
            }
            else if (collision.gameObject.name.Equals("PlayerUcak"))
            {
                wormController.KickBack(1000);
            }

        }
        else if (collision.CompareTag("Bullet"))
        {
            bullet = collision.GetComponent<Bullet>();
            if (wait == 0)
            {
                if (bullet.impacted == 0)
                {
                    bullet.impacted = 1;
                    wormController.KickBack(bullet.damage);
                    wait = 1;
                    StartCoroutine(Wait());
                    IEnumerator Wait()
                    {
                        yield return new WaitForSeconds(3);
                        wait = 0;
                    }
					ScoreManager.AddBulletBodyshotKill();
				}
                GameObject.Destroy(collision.gameObject);
              
            }
        }
        else if (collision.CompareTag("explosion"))
        {
            wormController.KickBack(1000);
        }
    }
}
