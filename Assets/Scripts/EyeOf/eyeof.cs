using UnityEngine;

public class eyeof : MonoBehaviour
{
    public string sayOnDead, sayOnSaw;
    LevelManager levelManager;
    public AudioClip flames;
    private AudioSource audioSource;
    private Animator animator;
    private bool seen;
    private Rigidbody2D leftEyeBody, rightEyeBody, playerBody;
    public GameObject leftEye, rightEye, flameLeft, flameRight;
    private bool once;

    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        animator = GetComponent<Animator>();

        leftEyeBody = leftEye.GetComponent<Rigidbody2D>();
        rightEyeBody = rightEye.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Dead()
    {
        Destroy(this);
        GameObject.Destroy(this.gameObject, 12f);
        flameLeft.SetActive(true);
        flameRight.SetActive(true);
        animator.Play("eyeofclose");
        audioSource.PlayOneShot(flames);
        if (string.IsNullOrEmpty(sayOnDead) is false)
            levelManager.Say(sayOnDead, .5f, true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            animator.Play("eyeofopen");
            audioSource.Play();
            if (!once)
                if (string.IsNullOrEmpty(sayOnSaw) is false)
                    levelManager.Say(sayOnSaw, .5f, false);
            once = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            animator.Play("eyeofclose");
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Rigidbody2D player_rigidbody2d = collision.GetComponent<Rigidbody2D>();
            leftEyeBody.velocity = new Vector2(player_rigidbody2d.velocity.x / 2, player_rigidbody2d.velocity.y / 5);
            rightEyeBody.velocity = new Vector2(player_rigidbody2d.velocity.x / 2, player_rigidbody2d.velocity.y / 5);
        }
    }
}
