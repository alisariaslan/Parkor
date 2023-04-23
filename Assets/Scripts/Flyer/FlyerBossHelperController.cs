using UnityEngine;

public class FlyerBossHelperController : MonoBehaviour
{
    public bool startcarry;
    public bool sayOn;
    public string sayOnSaw, sayOnDrop;
    public AudioClip audioClip1, audioClip2, audioClip3;
   
    private Animator animator;
    private PlayerController playerController;
    private LevelManager levelManager;
    private AudioSource audioSource;
    
    private bool carry;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        levelManager = FindAnyObjectByType<LevelManager>();
        playerController = FindAnyObjectByType<PlayerController>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (startcarry)
        {
            CarryNow();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (carry)
        {
            player.transform.position = this.transform.position;
        }
            
    }

    public void CarryNow()
    {
        audioSource.Play();
        playerController.pause = true;
        playerController.NoItem();
        animator.Play("flyerbosshelperidle", 0);
        animator.Play("flyerbosshelpercarry", 1);
        carry = true;
        
    }

    public void Drop()
    {
        GameObject.Destroy(gameObject, 5f);
        audioSource.Stop();
        if(sayOn)
        levelManager.Say(sayOnDrop, 0.5f, true);
        carry = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        playerController.pause = false;
    }

    public void Roar()
    {
        audioSource.PlayOneShot(audioClip2);
    }

    public void WingFlap()
    {
        audioSource.PlayOneShot(audioClip1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(sayOn)
            levelManager.Say(sayOnSaw, 0.5f, true);

            Destroy(GetComponent<BoxCollider2D>());
            CarryNow();

        }


    }
}

