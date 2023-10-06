using System.Collections;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AraSahne : MonoBehaviour
{
    public StringTableCollection uiTableCollection;
    public StringTableCollection storyTableCollection;
    public AudioSource audioSource;
    public AudioClip typing;
    public bool test;
    public string adUnitId;
    public GameObject devamText;
    public bool ready;
    public GameObject blackoutPanel;
    public Text lbl_txt;
    private bool devam;

    private void Update()
    {
        LevelManager.inGameTimer += Time.deltaTime;
        //print("timer: " + LevelManager.inGameTimer);
    }

    void Start()
    {
        if (test)
        {
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
            LevelManager.inGameTimer = 105;
        }
           
        blackoutPanel.GetComponent<Animator>().Play("Brighten");
        int save = PlayerPrefs.GetInt("save", 1);
        print(save);
        var text = LocalizationSettings.StringDatabase.GetLocalizedString(storyTableCollection.name, "story_episode" + save);
        char[] chars = text.ToCharArray();
        StartCoroutine(co());
        IEnumerator co()
        {
            yield return new WaitForSeconds(3f);
            devam = true;
            foreach (char c in chars)
            {
                yield return new WaitForSeconds(.05f);
                lbl_txt.text += c;
                if (char.IsLetter(c))
                    audioSource.PlayOneShot(typing);
                if (!devam)
                {
                    Pass();
                    yield break;
                }
            }
            Pass();
        }
    }

    public void Pass()
    {
        devamText.SetActive(true);
        ready = true;
        var save = PlayerPrefs.GetInt("save", 1);
        var text = LocalizationSettings.StringDatabase.GetLocalizedString(storyTableCollection.name, "story_episode" + save);
        lbl_txt.text = text;
    }

    public void SeriYaz()
    {
        devam = false;
        if (ready)
        {
            var text = LocalizationSettings.StringDatabase.GetLocalizedString(uiTableCollection.name, "continues");
            devamText.GetComponent<Text>().text = text;
            GameObject panel = GameObject.Find("Panel");
            if (panel != null)
                panel.GetComponent<Animator>().Play("Blackout");
            StartCoroutine(co());
            IEnumerator co()
            {
                yield return new WaitForSeconds(5f);
                var save = PlayerPrefs.GetInt("save", 1);
                SceneManager.LoadScene(save++);
            }
        }
    }
}
