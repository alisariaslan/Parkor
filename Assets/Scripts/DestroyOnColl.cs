using UnityEngine;


public class DestroyOnColl : MonoBehaviour
{
    public bool isPlayingControl;
    public bool onTrigger, onCollision;
    public string objectTag;
    public float destroyTime = 1f;
    public bool destroyOther;
    public bool selfAudioSource;
    public AudioSource audioSource;
    public AudioClip destroySound;
    public ulong soundDelay;
    public int lives = 0;
    public bool animate;
    private Animator animator;
    private CarController carController;
    // Start is called before the first frame update
    void Start()
    {
        if (selfAudioSource)
            audioSource = GetComponent<AudioSource>();
        if (animate)
            animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SoundPlay()
    {
        if (audioSource != null)
        {
            audioSource.clip = destroySound;
            if (!audioSource.isPlaying)
                audioSource.Play(soundDelay);
        }

    }

    private void SoundPlayWhenPlaying()
    {
        if (audioSource != null)
        {
            audioSource.clip = destroySound;
            audioSource.Play(soundDelay);
        }
    }

    private void Eliminate(Collision2D collision)
    {
        if (collision.transform.tag == objectTag)
        {
            if (isPlayingControl)
                SoundPlay();
            else SoundPlayWhenPlaying();
            if (animate)
                animator.enabled = true;
            if (lives > 0)
                lives--;
            if (lives == 0)
            {


                if (destroyOther)
                    Destroy(collision.transform.gameObject, destroyTime);
                else
                    Destroy(this.transform.gameObject, destroyTime);
            }
        }

    }

    private void Eliminate(Collider2D collision)
    {
        if (collision.tag == objectTag)
        {
            collision.GetComponentInParent<CarController>().carHealth += 30;
            SoundPlay();
            if (animate)
                animator.enabled = true;
            if (lives > 0)
                lives--;
            if (lives == 0)
            {
                if (destroyOther)
                    Destroy(collision.transform.gameObject, destroyTime);
                else
                    Destroy(this.transform.gameObject, destroyTime);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onCollision)
            Eliminate(collision);
        if (collision.transform.CompareTag("Car"))
        {
            carController = FindObjectOfType<CarController>();
            carController.BounceBack();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onTrigger)
            Eliminate(collision);
    }
}
