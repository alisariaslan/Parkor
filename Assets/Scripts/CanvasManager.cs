using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject Tutorial;
    public GameObject StoryText, Log;
    public GameObject Pause;
    public GameObject DevTools;
    public GameObject Panel;
    public GameObject Sarjor;


    private bool storyAll;
    private bool tutorialPanel;
    private bool blackPanel;
    private bool pausePanel;
    private bool log;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("devtools") == 0)
            DevTools.SetActive(false);

    }
    public void Tanitim()
    {
        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            if (blackPanel)
                Aydinlat();
            if (tutorialPanel)
                Tutorial.GetComponent<Animator>().Play("tutorial");
            yield return new WaitForSeconds(5);
            if (storyAll)
                StoryText.GetComponent<Animator>().Play("textAppear");
        }
    }
    public void SetStoryText(TextAsset textAsset, int storyNo)
    {
        StoryText.GetComponent<Text>().text = Dialogs.Text(storyNo, textAsset) + "\n\n" + Dialogs.Text(storyNo + 1, textAsset);

    }
    public void Ilistir(bool storyAll, bool tutorialPanel, bool blackPanel, bool pausePanel, bool log)
    {

        this.storyAll = storyAll;
        this.tutorialPanel = tutorialPanel;
        this.blackPanel = blackPanel;
        this.pausePanel = pausePanel;
        this.log = log;
        SetActives();

    }

    public void SetActives()
    {
        Panel.SetActive(blackPanel);
        Tutorial.SetActive(tutorialPanel);
        StoryText.SetActive(storyAll);

    }
    public void SetLog(string logString)
    {
        if (log)
            Log.GetComponent<Text>().text = logString;
    }
    public void PlaySay(string text, float normalizedTime)
    {
        if (storyAll)
        {
            StoryText.GetComponent<Text>().text = text;
            StoryText.GetComponent<Animator>().Play("textAppear", 0, normalizedTime);
        }

    }
    public void PausePanel()
    {
        if (pausePanel)
        {
            if (!Pause.activeSelf)
            {
                Pause.SetActive(true);
                FindObjectOfType<SwitchItem>().CheckEnvanter();
            }

            else Pause.SetActive(false);
        }

    }

    public void PausePanel(bool state)
    {
        if (pausePanel)
        {
            Pause.SetActive(state);
            if (state)
                FindObjectOfType<SwitchItem>().CheckEnvanter();

        }

    }

    public void Aydinlat()
    {
        if (blackPanel)
            Panel.GetComponent<Animator>().Play("Brighten");
    }

    public void Karart()
    {
        if (blackPanel)
            Panel.GetComponent<Animator>().Play("Blackout");
    }


}
