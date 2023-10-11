using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject MobileControllers;
    public GameObject Tutorial;
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

    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();

        if (PlayerPrefs.GetInt("devtools") == 0)
            devmode = false;
        else
            devmode = true;

        DevTools.SetActive(devmode);
        StoryText.SetActive(levelManager.storyTexts);
        BlackPanel.SetActive(levelManager.blackPanel);
        Log.SetActive(levelManager.logs);
    }

    public void SetAmmo(int count)
    {
        Ammo.GetComponentInChildren<Text>().text = count.ToString();
    }

    public void PlayMessage(string key, float normalizedTime)
    {
        if (levelManager.storyTexts)
        {
            var text = LangHelper.GetLanguageValue(key);
            StoryText.GetComponent<Text>().text = text;
            StoryText.GetComponent<Animator>().Play("textAppear", 0, normalizedTime);
        }
    }

    public void PlayMessageWithoutLocalization(string text, float normalizedTime)
    {
        if (levelManager.storyTexts)
        {
            StoryText.GetComponent<Text>().text = text;
            StoryText.GetComponent<Animator>().Play("textAppear", 0, normalizedTime);
        }
    }

    public void StartGame()
    {
        if (levelManager.tutorials is false)
        {
            StartCoroutine(ExampleCoroutine());
            IEnumerator ExampleCoroutine()
            {
                yield return new WaitForSeconds(5);
                if (levelManager.storyTexts)
                    StoryText.GetComponent<Animator>().Play("textAppear");
                if (Application.isMobilePlatform || levelManager.forceMobile)
                    MobileControllers.SetActive(levelManager.controllers);
            }
        }
    }

    public void SetStoryText(string key)
    {
        var text = LangHelper.GetLanguageValue(key);
        StoryText.GetComponent<Text>().text = text;
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
        if (devmode)
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

    public void CloseTutorial()
    {
        Tutorial.SetActive(false);
        if (levelManager.storyTexts)
            StoryText.GetComponent<Animator>().Play("textAppear");
        if (Application.isMobilePlatform || levelManager.forceMobile)
            MobileControllers.SetActive(levelManager.controllers);
    }
}
