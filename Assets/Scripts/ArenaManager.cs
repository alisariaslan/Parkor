
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private int activeWave = 0;

	void Start()
	{
		if (quickStart)
		{
			NextWave();
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
				levelManager.Say("3", 0.5f, false);
				yield return new WaitForSeconds(3.0f);
				levelManager.Say("2", 0.5f, false);
				yield return new WaitForSeconds(3.0f);
				levelManager.Say("1", 0.5f, false);
				yield return new WaitForSeconds(3.0f);
				NextWave();
			}
		}
	}
	private void NextWave()
	{
		activeWave++;
		if (startWaveNumber > 1)
		{
			activeWave = startWaveNumber;
			startWaveNumber = 1;
		}
		if (activeWave - 1 < waves.Length)
			StartCoroutine(Wait(waves, activeWave - 1, restTime[activeWave - 1]));
		else
			Finish();
	}

	private void Finish()
	{
		levelManager.Say("Tebrikler! Arenayi basariyla tamamladin. Yakinda gelecek olan sezon 2 i�in batarya ve can degerlerin iki katina cikti..", 0.5f, true);
		PlayerPrefs.SetInt("ArenaCompleted", 1);
		StartCoroutine(Exit());
	}

	IEnumerator Exit()
	{
		yield return new WaitForSeconds(waitBeforeExit);
		MenuScript menuScript = FindAnyObjectByType<MenuScript>();
		menuScript.StartGame("Menu");
	}

	IEnumerator Wait(GameObject[] waves, int active, int restSec)
	{
		if (disableRestBetweenWaves)
			restSec = 0;
		yield return new WaitForSeconds(restSec);
		waves[active].SetActive(true);
		if (active > 0)
			waves[active - 1].SetActive(false);
		StartCoroutine(CheckForEnemy());
		levelManager.Say(activeWave+". Dalga", 0.5f, false);
	}

	IEnumerator CheckForEnemy()
	{
		yield return new WaitForSeconds(5);
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemies.Length == 0)
		{
			NextWave();
		}
		else
		{
			StartCoroutine(CheckForEnemy());
		}
	}


}
