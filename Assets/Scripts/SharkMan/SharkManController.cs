using UnityEngine;

public class SharkManController : MonoBehaviour
{
    private bool isTouchingGround;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private Animator animator;
    private Rigidbody2D rigidbody2Da;
    public float speed = 5f;
    public int direction = 1;
    private bool ok;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2Da = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (!isTouchingGround)
        {
            if (ok)
            {
                direction = direction * -1;
                ok = false;
            }
            animator.enabled = false;
            rigidbody2Da.velocity = new Vector2(direction * speed, rigidbody2Da.velocity.y);

        }
        else
        {
            ok = true;
            rigidbody2Da.constraints = RigidbodyConstraints2D.FreezePositionY;
            rigidbody2Da.freezeRotation = true;
            animator.enabled = true;
            rigidbody2Da.velocity = new Vector2(direction * speed, rigidbody2Da.velocity.y);
        }
    }
}
