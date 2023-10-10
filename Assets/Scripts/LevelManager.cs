
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("General")]
    public bool forceMobile = false;
    public bool saveOnStart = true;

    [Header("Timer")]
    public static float inGameTimer = 0;

    [Header("Scores")]
    public bool scoreEnabled;
    public int totalScore = 0;

    [Header("Story")]
    public string storyTextKey;

    [Header("Others")]
    public bool arenaLevel;
    public bool gotCheckpoint;
    public bool anonimController;
    public AudioClip whoosh;
    public Vector2 spawnpoint;

    [Header("Panels")]
    public bool storyTexts = true;
    public bool logs = false;
    public bool blackPanel = true;
    public bool controllers = true;
    public bool tutorials = true;
    public bool platformCheckForTutorials = true;

    private AudioSource audioSource;
    private GameObject player;
    private CanvasManager canvasManager;

    public void Score(string thing, int point)
    {
        totalScore += point;
        if (scoreEnabled)
            SayWithoutLocalization($"+{point} -> {totalScore}", .5f, false);
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

    async void Start()
    {
        if (saveOnStart is true)
        {
            int aktifIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("save", aktifIndex);
        }
        audioSource = GetComponent<AudioSource>();
        canvasManager = FindAnyObjectByType<CanvasManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            spawnpoint = player.transform.position;
        canvasManager.SetStoryText(storyTextKey);
        if ((Application.isMobilePlatform || forceMobile) && platformCheckForTutorials)
            tutorials = false;
        await Task.Delay(100);
        canvasManager.StartGame();
    }

    private void Update()
    {
        inGameTimer += Time.deltaTime;
    }

    public void Say(string text, float normalizedTime, bool enableWooshSound)
    {
        canvasManager.PlayMessage(text, normalizedTime);
        if (enableWooshSound)
            audioSource.PlayOneShot(whoosh);
    }

    public void SayWithoutLocalization(string text, float normalizedTime, bool enableWooshSound)
    {
        canvasManager.PlayMessageWithoutLocalization(text, normalizedTime);
        if (enableWooshSound)
            audioSource.PlayOneShot(whoosh);
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
            if (arenaLevel)
                FindObjectOfType<ArenaManager>().RestartWave();
            FindObjectOfType<PlayerController>().Spawn();
        }
    }

    public void RespawnEnemies()
    {
        foreach (SpiderlingController controller in FindObjectsByType<SpiderlingController>(FindObjectsSortMode.None))
        {
            controller.Respawn();
        }
        foreach (SpiderController controller in FindObjectsByType<SpiderController>(FindObjectsSortMode.None))
        {
            controller.Respawn();
        }
        foreach (KozaController controller in FindObjectsByType<KozaController>(FindObjectsSortMode.None))
        {
            controller.Respawn();
        }
        foreach (OmegaController controller in FindObjectsByType<OmegaController>(FindObjectsSortMode.None))
        {
            controller.Respawn();
        }
        foreach (CasperController controller in FindObjectsByType<CasperController>(FindObjectsSortMode.None))
        {
            controller.Respawn();
        }
        foreach (WormController controller in FindObjectsByType<WormController>(FindObjectsSortMode.None))
        {
            controller.Respawn();
        }
    }

    public void StopEnemies()
    {
        foreach (SpiderlingController controller in FindObjectsByType<SpiderlingController>(FindObjectsSortMode.None))
        {
            //controller.Stop();
        }
        foreach (SpiderController controller in FindObjectsByType<SpiderController>(FindObjectsSortMode.None))
        {
            //controller.Stop();
        }
        foreach (KozaController controller in FindObjectsByType<KozaController>(FindObjectsSortMode.None))
        {
            //controller.Stop();
        }
        foreach (OmegaController controller in FindObjectsByType<OmegaController>(FindObjectsSortMode.None))
        {
            //controller.Stop();
        }
        foreach (CasperController controller in FindObjectsByType<CasperController>(FindObjectsSortMode.None))
        {
            //controller.Stop();
        }
        foreach (WormController controller in FindObjectsByType<WormController>(FindObjectsSortMode.None))
        {
            controller.Stop();

        }
    }



}
