using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 35;
    public float randomYmin, randomYmax;
    public float yon = 1;
    public GameObject parlama;
    public AudioClip bulletShell, bulletDrop;
    private AudioSource audioSource;
    private Rigidbody2D rigidbody2Da;
    public float speedX = 100f;
    public int impacted = 0;
    
    void Start()
    {
        if (yon < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        rigidbody2Da = GetComponent<Rigidbody2D>();
        float random = Random.Range(randomYmin, randomYmax);
        rigidbody2Da.velocity = new Vector2(speedX * yon, random);
        audioSource = GetComponent<AudioSource>();
        GameObject pat = Instantiate(parlama, this.transform.position, Quaternion.identity, transform.parent);
        GameObject.Destroy(pat, .1f);
        StartCoroutine(ExampleCoroutine());

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Ground") && isdestroyed is false)
        {
            if (impacted == 0)
            {
                audioSource.PlayOneShot(bulletDrop);
                impacted = 1;
                ScoreManager.AddBulletMissedshot();
            }
            else if (impacted == 1)
            {
                audioSource.PlayOneShot(bulletShell);
                impacted = 2;
            }
        }
        else
        {
            GameObject.Destroy(gameObject, .1f);
            isdestroyed = true;
        }
    }

    bool isdestroyed = false;
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(10);
        isdestroyed = true;
        GameObject.Destroy(gameObject, .1f);
    }


}
