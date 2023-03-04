using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject patlama;
    public float veloPower;
	
    private Rigidbody2D rigidbody2Da;

	void Start()
    {
        rigidbody2Da = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        veloPower = rigidbody2Da.velocity.y;
    }
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
            GameObject boom = Instantiate(patlama, transform.position, Quaternion.identity);
            if (veloPower < 0)
                veloPower *= -1;
            if (veloPower > 99)
                veloPower = 99;

            Vector2 transformScale = boom.transform.localScale;
            float newTransformScale = (transformScale.x * (100 + veloPower)) / 100;
            boom.transform.localScale = new Vector2(newTransformScale, newTransformScale);
            Vector2 colliderBoxScale = boom.GetComponent<BoxCollider2D>().size;
            float newColliderBoxScale = (colliderBoxScale.x * (100 + veloPower)) / 100;
            boom.GetComponent<BoxCollider2D>().size = new Vector2(newColliderBoxScale, newColliderBoxScale);
			GameObject.Destroy(gameObject);
	}
}
