using UnityEngine;

public class TriggerMessage : MonoBehaviour
{
    public TextAsset textAsset;
    public string textKey;
    public AudioClip triggerSound;
    private AudioSource audioSource;
    private LevelManager levelManager;

    public float normalizedTime = 0.5f;
    public bool dontDestroy, noSound, useLevelManagerSound;


    void Start()
    {
        if (useLevelManagerSound)
            noSound = true;
        levelManager = FindAnyObjectByType<LevelManager>();
        audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelManager.Say(textKey, normalizedTime, false);
            if (!noSound)
                audioSource.PlayOneShot(triggerSound);
            if (useLevelManagerSound)
                levelManager.PlayAudioOnce(triggerSound, 1f);
            if (!dontDestroy)
                GameObject.Destroy(this);

        }
    }
}
