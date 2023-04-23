using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject resume;
	public GameObject blackScreen;
    void Start()
    {
        if (PlayerPrefs.GetInt("save") > 0 && resume != null)
            resume.SetActive(true);
    }

    private void Blackout(bool isString, string scenename, int index)
    {
        int waitTimer;
        if (blackScreen != null)
        {
            waitTimer = 5;
			blackScreen.GetComponent<Animator>().Play("Blackout");
        }
        else
            waitTimer = 0;
        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            yield return new WaitForSeconds(waitTimer);
            if (isString)
                SceneManager.LoadScene(scenename, LoadSceneMode.Single);
            else
                SceneManager.LoadScene(index, LoadSceneMode.Single);
        }
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("save", 1);
        PlayerPrefs.SetInt("acikBolumler", 1);
        PlayerPrefs.SetInt("Envanter", 0);
        ScoreManager.ResetScores();
        Blackout(true, "Intro", 0);
    }

    public void StartGameAndSave(int save)
    {
        PlayerPrefs.SetInt("save", save+1);
        Blackout(true, "AraSahne", 0);
    }

    public void ResumeGame()
    {
        Blackout(true, "AraSahne", 0);
    }

    public void StartGame(string sceneName)
    {
        Blackout(true, sceneName, 0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void RestartGame()
    {
        int aktifIndex = SceneManager.GetActiveScene().buildIndex;
        Blackout(false, null, aktifIndex);
    }


}
