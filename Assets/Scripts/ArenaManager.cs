using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEditor.Localization;

public class ArenaManager : MonoBehaviour
{
    public LevelManager levelManager;
    public AudioSource audioSource;
    public AudioClip countDown;
    public int delayForCountDown = 10;
    public GameObject[] waves;
    public int[] restTime;
    public int lookForEnemies = 10;
    public bool quickStart = false;
    public bool disableRestBetweenWaves = false;
    public int startWaveNumber = 1;
    public int waitBeforeExit = 10;

    [Header("Localization")]
    public StringTableCollection dialogTableCollection;

    private int activeWave = 0;
    private bool pause;

    void Start()
    {
        if (quickStart)
        {
            NextWave(false);
            return;
        }

        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            yield return new WaitForSeconds(delayForCountDown);
            audioSource.PlayOneShot(countDown);
            StartCoroutine(ExampleCoroutine2());
            IEnumerator ExampleCoroutine2()
            {
                yield return new WaitForSeconds(3.5f);
                levelManager.SayWithoutLocalization("3", 0.5f, false);
                yield return new WaitForSeconds(3.0f);
                levelManager.SayWithoutLocalization("2", 0.5f, false);
                yield return new WaitForSeconds(3.0f);
                levelManager.SayWithoutLocalization("1", 0.5f, false);
                yield return new WaitForSeconds(3.0f);
                NextWave(false);
            }
        }
    }
    private void NextWave(bool isRestart)
    {
        activeWave++;
        if (startWaveNumber > 1)
        {
            activeWave = startWaveNumber;
            startWaveNumber = 1;
        }
        if (activeWave - 1 < waves.Length)
            StartCoroutine(Wait(waves, activeWave - 1, restTime[activeWave - 1], isRestart));
        else
            Finish();
    }

    private void Finish()
    {
        var localstring = LocalizationSettings.StringDatabase.GetLocalizedString(dialogTableCollection.name, "arena_complete");
        levelManager.SayWithoutLocalization(localstring, 0.5f, true);
        PlayerPrefs.SetInt("ArenaCompleted", 1);
        StartCoroutine(Exit());
    }

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(waitBeforeExit);
        MenuScript menuScript = FindAnyObjectByType<MenuScript>();
        menuScript.StartGame("Menu");
    }

    IEnumerator Wait(GameObject[] waves, int active, int restSec, bool isRestart)
    {
        if (disableRestBetweenWaves)
            restSec = 0;
        yield return new WaitForSeconds(restSec);
        waves[active].SetActive(true);
        if (active > 0)
            waves[active - 1].SetActive(false);
        StartCoroutine(CheckForEnemy());
        var localstring = LocalizationSettings.StringDatabase.GetLocalizedString(dialogTableCollection.name, "wave");
        levelManager.SayWithoutLocalization(localstring + " " + activeWave, 0.5f, false);
    }

    IEnumerator CheckForEnemy()
    {
        if (pause is false)
            yield return new WaitForSeconds(5);
        if (pause is false)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                NextWave(false);
            }
            else
            {
                StartCoroutine(CheckForEnemy());
            }
        }
    }

    public void RestartWave()
    {
        pause = true;
        activeWave--;
        waves[activeWave].SetActive(false);
        StartCoroutine(StartAgain());
        IEnumerator StartAgain()
        {
            yield return new WaitForSeconds(10);
            pause = false;
            NextWave(true);
        }

    }


}
