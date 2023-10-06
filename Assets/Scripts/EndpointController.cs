
using UnityEngine;

public class EndpointController : MonoBehaviour
{
    public bool isSeasonEnd;
    public string nextSceneName;
    public bool araSahne = true;
    public bool onlyAnim;
    public AudioClip endSound;
    public bool destroyCollAfterColl = true;
    public bool noEndSound;
   
    private LevelManager levelManager;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !onlyAnim)
            Finish();
    }

    public void Finish()
    {
        int save = PlayerPrefs.GetInt("save", 1);
        int acikBolumler = PlayerPrefs.GetInt("acikBolumler",1);

        save++;
        if (isSeasonEnd)
        {

        } else
        {
            PlayerPrefs.SetInt("save", save);
            if (save > acikBolumler)
            {
                PlayerPrefs.SetInt("acikBolumler", save);
            }
        }

        if (araSahne)
            levelManager.NextLevel("AraSahne");
        else
            levelManager.NextLevel(nextSceneName);
        if (!noEndSound)
            audioSource.PlayOneShot(endSound);
        if (destroyCollAfterColl)
            Destroy(GetComponent<BoxCollider2D>());
    }
}
