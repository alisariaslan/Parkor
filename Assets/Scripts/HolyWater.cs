using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    public float giveHealth = 10;

    private AudioSource audioSource;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Tire"))
        {
            audioSource.Play();
            collision.GetComponentInParent<CarController>().Heal(giveHealth);
            animator.Play("water");
            Destroy(GetComponent<BoxCollider2D>());
            GameObject.Destroy(gameObject, 5f);
        }
    }
}
