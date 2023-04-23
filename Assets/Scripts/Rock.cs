using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Rigidbody2D rigidbody2Da;
    private Rigidbody2D rigidbody2D_parent;
    private AudioSource audioSource;
    private Animator animator;
    public AudioClip audio1, audio2, audio3;
    public bool deadly = true;
    public string sayOnColl;
    private bool contactonce;
    public Vector2 velociy;

    public void NoDeadly()
    {
        deadly = false;
    }

    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        rigidbody2Da = GetComponent<Rigidbody2D>();
        rigidbody2Da.velocity = velociy;
        rigidbody2Da.AddTorque(20f, ForceMode2D.Force);

        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3);
            animator.enabled = true;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tire")
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(audio1);
            Destroy(this.gameObject, 1f);
        }
        else if (collision.transform.tag == "Player")
        {
            rigidbody2Da.velocity = new Vector2(rigidbody2Da.velocity.x, 5);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        if (!contactonce)
        {
            contactonce = true;
            Destroy(this.gameObject, 10f);


            if (collision.transform.tag == "Car")
            {
                rigidbody2Da.velocity = new Vector2(rigidbody2Da.velocity.x, 5);
                //Debug.Log("Bom!");
                if (!audioSource.isPlaying)
                    audioSource.Play();
                CarController carController = FindAnyObjectByType<CarController>();
                if (carController.carHealth > 0)

                    carController.Damage(10);



            }
        }
    }
}
