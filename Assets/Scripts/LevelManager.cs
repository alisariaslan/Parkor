
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static float inGameTimer = 0;

    public bool scoreEnabled;
    public int startScore = 0;

    public TextAsset storyAsset;
    public int storyNo = 0;

    public bool storyString = true;
    public bool storyAll = true;
    public bool tutorialPanel = true;
    public bool blackPanel = true;
    public bool pausePanel = true;
    public bool log;

    public bool gotCheckpoint;
    public bool anonimController;
    public AudioClip whoosh;

    private AudioSource audioSource;
    private GameObject player;
    public Vector2 spawnpoint;
    private CanvasManager canvasManager;
    private bool release;

    public void Score(string thing, int point)
    {
        startScore += point;
        if (scoreEnabled)
            Say(thing + "\n" + "Toplam puan: " + startScore + "\n" + point + " puan eklendi.", .5f, false);

		if (startScore == 500)
			ScoreManager.ReportScoreType(ScoreManager.scoreType.NESIL_TUKETEN);

    }

    public void NextLevel(string sceneName)
    {
        StartCoroutine(co());
        IEnumerator co()
        {
            canvasManager.Karart();
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public void PlayAudio(AudioClip audioClip, bool loop)
    {
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void PlayAudioOnce(AudioClip audioClip, float volume)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }
    public void MuteAudio()
    {
        audioSource.Stop();
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canvasManager = FindObjectOfType<CanvasManager>();

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            spawnpoint = player.transform.position;

        canvasManager.Ilistir(storyAll, tutorialPanel, blackPanel, pausePanel, log);

        if (storyString)
            canvasManager.SetStoryText(storyAsset, storyNo);

        canvasManager.Tanitim();

    }



    private void Update()
    {
        inGameTimer += Time.deltaTime;
        //print(inGameTimer);
        if (anonimController)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                canvasManager.PausePanel();
            }
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.y < Screen.height / 5)
                {

                }
                else if (touch.position.y > Screen.height / 1.2f)
                {
                    release = true;
                }
                else if (touch.position.x < Screen.width / 2)
                {

                }
                else if (touch.position.x > Screen.width / 2)
                {

                }
            }
            else
            {
                if (release)
                {
                    canvasManager.PausePanel();
                    release = false;
                }

            }
        }
    }


    public void Log(string text)
    {
        canvasManager.SetLog(text);
    }

    public void Say(string text, float normalizedTime, bool enableWooshSound)
    {
        canvasManager.PlaySay(text, normalizedTime);
        if (enableWooshSound)
            audioSource.PlayOneShot(whoosh);
    }

    public void EscapePanel()
    {

        canvasManager.PausePanel();
    }
    public void EscapePanel(bool state)
    {
        canvasManager.PausePanel(state);
    }
    public void Restart()
    {

        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            canvasManager.Karart();
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
    public void Respawn(GameObject gameObject)
    {
        RespawnEnemies();
        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            canvasManager.Karart();
            yield return new WaitForSeconds(5);
            gameObject.transform.position = spawnpoint;
            canvasManager.Aydinlat();
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.Spawn();
        }
    }

    public void RespawnEnemies()
    {
        foreach (SpiderlingController controller in FindObjectsOfType<SpiderlingController>())
        {
            controller.Respawn();
        }
        foreach (SpiderController controller in FindObjectsOfType<SpiderController>())
        {
            controller.Respawn();
        }
        foreach (KozaController controller in FindObjectsOfType<KozaController>())
        {
            controller.Respawn();
        }
        foreach (OmegaController controller in FindObjectsOfType<OmegaController>())
        {
            controller.Respawn();
        }
        foreach (CasperController controller in FindObjectsOfType<CasperController>())
        {
            controller.Respawn();
        }
        foreach (WormController controller in FindObjectsOfType<WormController>())
        {
            controller.Respawn();

        }
    }

    public void StopEnemies()
    {
        foreach (SpiderlingController controller in FindObjectsOfType<SpiderlingController>())
        {
            //controller.Stop();
        }
        foreach (SpiderController controller in FindObjectsOfType<SpiderController>())
        {
            //controller.Stop();
        }
        foreach (KozaController controller in FindObjectsOfType<KozaController>())
        {
            //controller.Stop();
        }
        foreach (OmegaController controller in FindObjectsOfType<OmegaController>())
        {
            //controller.Stop();
        }
        foreach (CasperController controller in FindObjectsOfType<CasperController>())
        {
            //controller.Stop();
        }
        foreach (WormController controller in FindObjectsOfType<WormController>())
        {
            controller.Stop();

        }
    }



}
