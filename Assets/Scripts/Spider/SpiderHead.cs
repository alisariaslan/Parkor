using UnityEngine;

public class SpiderHead : MonoBehaviour
{
    private Bullet bullet;
    private Animator animator;
    private SpiderController spiderController;
    private PlayerController player;
    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        spiderController = GetComponentInParent<SpiderController>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.name.Equals("PlayerHuman"))
            {
                player.Bounce();
                spiderController.Damage(player.jumpDamage, 0);
            } else if (collision.transform.name.Equals("PlayerUcak"))
            {
                spiderController.Damage(1000, 0);
            }


        }
        else if (collision.CompareTag("Bullet"))
        {

            bullet = collision.GetComponent<Bullet>();
            if (bullet.impacted == 0)
            {
                if (spiderController.seen == false)
                    spiderController.Saw(GameObject.FindGameObjectWithTag("Player"));
                spiderController.BounceBack();
                spiderController.Damage(bullet.damage * 3, 0);
                GameObject.Destroy(collision.gameObject, .1f);

				ScoreManager.AddBulletHeadshotKill();
			}
            else
            {
                GameObject.Destroy(collision.gameObject, 1f);
            }
        }
    }
}
