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
        GameObject.Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Ground"))
        {
            if (impacted == 0)
            {
                audioSource.PlayOneShot(bulletDrop);
                impacted = 1;
				int bulletMissShots = PlayerPrefs.GetInt("bulletMissShots", 0);
				bulletMissShots++;
				PlayerPrefs.SetInt("bulletMissShots", bulletMissShots);
                //print(ScoreManager.bulletMissShots);

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
        }

    }


}
