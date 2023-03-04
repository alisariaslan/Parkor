using UnityEngine;

public class TireColl : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip rock;
    public bool destroy;
    public float destroyDelay;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void PlaySound(AudioClip clip, ulong delay)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play(delay);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if(collision.transform.CompareTag("Rock"))
        //{

        //    PlaySound(rock, 0);
        //    if (destroy)
        //        GameObject.Destroy(collision.gameObject,destroyDelay);

        //}
    }
}
