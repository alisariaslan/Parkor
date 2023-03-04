using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip audioClip;
    public bool loop;
    public bool delete;
    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelManager.PlayAudio(audioClip, loop);
            if (delete)
                Destroy(this);
        }

    }
}
