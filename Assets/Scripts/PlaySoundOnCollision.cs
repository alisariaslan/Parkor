using System.Collections;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;
    public float volume = .5f;
    public bool loop = false;
    public int seconds = 0;
    public bool destroy = false;
    public bool isnotplaying;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AudioPlay()
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.volume = volume;

        audioSource.Play();
        if (loop == true)
            if (seconds > 0)
                StartCoroutine(Loop());
        if (destroy)
        {
            GameObject.Destroy(this);
        }


        IEnumerator Loop()
        {
            yield return new WaitForSeconds(seconds);
            audioSource.Stop();
            if (destroy == true)
                GameObject.Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioPlay();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (isnotplaying)
            {
                if (!audioSource.isPlaying)
                    AudioPlay();
            }
            else
                AudioPlay();

        }
    }
}
