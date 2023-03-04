using UnityEngine;

public class WormBossAktarma : MonoBehaviour
{
    public GameObject aktarilacakOlan;
    public float health = 1000;
    public float fenerForceX = 10f;
    public float fenerForceY = 10f;
    public bool aktar = true;
    public bool tailEnd = false;
    public Animator animator;
	public float groundCheckRadius;
	public LayerMask groundLayer;
	
	private WormBossController wormBossController;
    private int tersyon = -1;
	private bool isTouchingGround;
	private Transform surfaceLight;
	private bool dead;
	private AudioSource audioSource;
	private GameObject player;
	private SpriteRenderer spriteRenderer;

	void Start()
    {
        audioSource = GetComponent<AudioSource>();
        surfaceLight = transform.GetChild(0);
        wormBossController = GetComponentInParent<WormBossController>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
  
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
        if (!isTouchingGround)
            surfaceLight.gameObject.SetActive(false);
        else
            surfaceLight.gameObject.SetActive(true);

        if (player.transform.position.x > transform.position.x)
            tersyon = 1;
        else
            tersyon = -1;
    }
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wormBossController.PlayerKilled();
        }
    }
	
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!dead)
        {
            if (collision.CompareTag("Fener"))
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
                if (tersyon == 1)
                    wormBossController.rigid_All(new Vector2(-3f, 3f));
                else
                    wormBossController.rigid_All(new Vector2(3f, 3f));
                if (tailEnd)
                {
                    health -= 10;
                    float newColorR = health / 10000 * 4;
                    spriteRenderer.color = new Color(newColorR, 0.08f, 0f, 0.8f);

                    if (health < 0)
                    {
                        GameObject.Destroy(gameObject);
                        wormBossController.partCount--;
						
                        dead = true;
                        if (wormBossController.partCount == -1)
                        {
                            wormBossController.Dead();
                        }
                        else
                        {
                            aktarilacakOlan.GetComponent<WormBossAktarma>().tailEnd = true;
                            aktarilacakOlan.GetComponent<WormBossAktarma>().fenerForceY = 0f;
                            wormBossController.tailEnd = aktarilacakOlan;
                        }
                    }
                }
            }
        }
    }
   
    public void Aktarma()
    {
        if (aktar)
            animator.Play("getBigger");
        if (tailEnd)
        {
            WormBossController wormBossController = GetComponentInParent<WormBossController>();
            wormBossController.aktarmaTamam = true;
            wormBossController.Fight();
        }
    }
	
}
