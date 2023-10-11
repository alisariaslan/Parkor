using UnityEngine;

public class WormBody : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.impacted == 0)
            {
                bullet.impacted = 1;
                GetComponentInParent<WormController>().Damage(bullet.damage / 2);
                ScoreManager.AddBulletHeadshot();
            }
            GameObject.Destroy(collision.gameObject);
        }
    }
}

