
using UnityEngine;
using UnityEngine.SceneManagement;

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


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        levelManager = FindAnyObjectByType<LevelManager>();

        //print("This Scene Index: " + SceneManager.GetActiveScene().buildIndex);
        //print("Next Scene Index: " + (SceneManager.GetActiveScene().buildIndex + 1));
        //print("Save: " + PlayerPrefs.GetInt("save", 0));
        //print("Opened Scenes: " + PlayerPrefs.GetInt("acikBolumler", 0));
    }

    // Update is called once per frame
    public void Next()
    {
        int aktifIndex = SceneManager.GetActiveScene().buildIndex;
        int save = PlayerPrefs.GetInt("save", 0);
        int acikBolumler = PlayerPrefs.GetInt("acikBolumler",0);

        if (!isSeasonEnd)
        {
            PlayerPrefs.SetInt("save", aktifIndex);
            if (save > acikBolumler )
            {
                PlayerPrefs.SetInt("acikBolumler", save);
            }
        } else
        {
            PlayerPrefs.SetInt("acikBolumler", save);
			ScoreManager.ReportScoreType(ScoreManager.scoreType.SEZON_1);
            
        }

        //13 next
        //12 aktif
        //11 save
        //10 acik

        /*
        print("ThisIndex: " + SceneManager.GetActiveScene().buildIndex);
        print("NextIndex: " + (SceneManager.GetActiveScene().buildIndex + 1));
        print("save: " + PlayerPrefs.GetInt("save", 0));
        print("acikBolumler: " + PlayerPrefs.GetInt("acikBolumler", 0));
        */
        if (araSahne)
        levelManager.NextLevel("AraSahne");
        else
            levelManager.NextLevel(nextSceneName);
        if (!noEndSound)
            audioSource.PlayOneShot(endSound);
        if (destroyCollAfterColl)
            Destroy(GetComponent<BoxCollider2D>());
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !onlyAnim)
            Next();
        

    }
}
