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

    private void Blackout(string scenename)
    {

        blackScreen.GetComponent<Animator>().Play("Blackout");
        StartCoroutine(LoadSceneCoroutine());
        IEnumerator LoadSceneCoroutine()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(scenename);
        }
    }

    private void Blackout(int index)
    {
        blackScreen.GetComponent<Animator>().Play("Blackout");
        StartCoroutine(LoadSceneCoroutine());
        IEnumerator LoadSceneCoroutine()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(index);
        }
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("save", 1);
        PlayerPrefs.SetInt("acikBolumler", 1);
        PlayerPrefs.SetInt("Envanter", 0);
        ScoreManager.ResetScores();
        Blackout("Intro");
    }

    public void StartGameAndSave(int save)
    {
        PlayerPrefs.SetInt("save", save);
        Blackout("AraSahne");
    }

    public void ResumeGame()
    {
        Blackout("AraSahne");
    }

    public void StartGame(string sceneName)
    {
        Blackout(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void RestartGame()
    {
        int aktifIndex = SceneManager.GetActiveScene().buildIndex;
        Blackout(aktifIndex);
    }


}
