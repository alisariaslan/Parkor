using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	[Header("Panels")]
	public GameObject MobileControllers;
	public GameObject PcControllers;
	public GameObject menuPanel;
	public GameObject inventoryPanel;
	public GameObject consolePanel;
	public GameObject DevTools;
	public GameObject BlackPanel;
	public GameObject StoryText;
	public GameObject Log;
	public GameObject Ammo;
	
	private LevelManager levelManager;
	private bool devmode = false;
	private bool forceMobile = false;

	void Start()
	{
		levelManager = FindAnyObjectByType<LevelManager>();
		forceMobile = levelManager.forceMobile;

		if (PlayerPrefs.GetInt("devtools") == 0)
			devmode = false;
		else
			devmode = true;

		DevTools.SetActive(devmode);
		StoryText.SetActive(levelManager.storyTexts);
		BlackPanel.SetActive(levelManager.blackPanel);
		Log.SetActive(levelManager.logs);

		if (Application.isMobilePlatform || forceMobile)
		{
			MobileControllers.SetActive(levelManager.controllers);
		}
		else
		{
			PcControllers.SetActive(levelManager.controllers);
		}
	}

	public void PlaySay(string text, float normalizedTime)
	{
		if (levelManager.storyTexts)
		{
			StoryText.GetComponent<Text>().text = text;
			StoryText.GetComponent<Animator>().Play("textAppear", 0, normalizedTime);
		}
	}

	public void StartGame()
	{
		if (levelManager.blackPanel)
			Aydinlat();
		StartCoroutine(ExampleCoroutine());
		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(5);
			if (levelManager.storyTexts)
				StoryText.GetComponent<Animator>().Play("textAppear");
		}
	}

	public void SetStoryText(TextAsset textAsset, int storyNo)
	{
		StoryText.GetComponent<Text>().text = Dialogs.Text(storyNo, textAsset) + "\n\n" + Dialogs.Text(storyNo + 1, textAsset);
	}

	public void SetLogText(string logString)
	{
		if (levelManager.logs)
			Log.GetComponent<Text>().text = logString;
	}

	public void Aydinlat()
	{
		if (levelManager.blackPanel)
			BlackPanel.GetComponent<Animator>().Play("Brighten");
	}

	public void Karart()
	{
		if (levelManager.blackPanel)
			BlackPanel.GetComponent<Animator>().Play("Blackout");
	}

	public void OpenMenu()
	{
		if (menuPanel.activeSelf)
		{
			menuPanel.SetActive(false);
		}
		else
		{
			menuPanel.SetActive(true);
		}
	}

	public void OpenInventory()
	{
		if (inventoryPanel.activeSelf)
		{
			inventoryPanel.SetActive(false);
		}
		else
		{
			inventoryPanel.SetActive(true);
		}
	}

	public void OpenConsole()
	{
		if(devmode)
		{
			if (consolePanel.activeSelf)
			{
				consolePanel.SetActive(false);
			}
			else
			{
				consolePanel.SetActive(true);
			}
		}
		
	}
}
