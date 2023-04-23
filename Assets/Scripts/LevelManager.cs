
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("General")]
	public bool forceMobile = false;

	[Header("Timer")]
	public static float inGameTimer = 0;

	[Header("Scores")]
	public bool scoreEnabled;
    public int startScore = 0;

	[Header("Story")]
	public TextAsset storyAsset;
    public int storyNo = 0;

    [Header("PcControllerText")]
    [Multiline]
    public string pcText = string.Empty;

	[Header("Others")]
	public bool gotCheckpoint;
    public bool anonimController;
    public AudioClip whoosh;
	public Vector2 spawnpoint;

	[Header("Panels")]
	public bool storyTexts = true;
	public bool logs = false;
	public bool blackPanel = true;
	public bool controllers = true;

	private AudioSource audioSource;
    private GameObject player;
    private CanvasManager canvasManager;

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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canvasManager = FindAnyObjectByType<CanvasManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            spawnpoint = player.transform.position;
            canvasManager.SetStoryText(storyAsset, storyNo);
    }

    private void Update()
    {
        inGameTimer += Time.deltaTime;
    }

    public void Say(string text, float normalizedTime, bool enableWooshSound)
    {
        canvasManager.PlaySay(text, normalizedTime);
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
            PlayerController playerController = FindAnyObjectByType<PlayerController>();
            playerController.Spawn();
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
