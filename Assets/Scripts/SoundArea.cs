using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundArea : MonoBehaviour
{

    private AudioSource audioSource;
    private GameObject player;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void SetAudioLevel(Transform player)
    {
        float uzaklik = transform.position.x - player.position.x;
        if (uzaklik < 0)
            uzaklik *= -1;
        float yaricap = GetComponent<BoxCollider2D>().size.x / 2;
        float mesafe = yaricap - uzaklik;
        mesafe *= 4;
    
        //print(mesafe);
        audioSource.volume = mesafe/100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
                audioSource.Play();

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetAudioLevel(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.Stop();
        }
    }


}