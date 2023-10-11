using System.Collections;
using System.Threading.Tasks;
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

    public async void NewGame()
    {
        blackScreen.GetComponent<Animator>().Play("Blackout");
        PlayerPrefs.SetInt("save", 1);
        PlayerPrefs.SetInt("acikBolumler", 1);
        PlayerPrefs.SetInt("pistol", 0);
        PlayerPrefs.SetInt("light", 0);
        ScoreManager.ResetScores();
        await Task.Delay(5000);
        SceneManager.LoadScene("Intro");
    }

    public void ResumeGame()
    {
        blackScreen.GetComponent<Animator>().Play("Blackout");
        StartCoroutine(LoadSceneCoroutine());
        IEnumerator LoadSceneCoroutine()
        {
            yield return new WaitForSeconds(5);
            if (PlayerPrefs.GetInt("save", 1) is 11)
                SceneManager.LoadScene("End");
            else
                SceneManager.LoadScene(PlayerPrefs.GetInt("save", 1));
        }
    }

    public void StartGame(string sceneName)
    {
        blackScreen.GetComponent<Animator>().Play("Blackout");
        StartCoroutine(LoadSceneCoroutine());
        IEnumerator LoadSceneCoroutine()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(sceneName);
        }
    }

    public void StartGame(int index)
    {
        blackScreen.GetComponent<Animator>().Play("Blackout");
        StartCoroutine(LoadSceneCoroutine());
        IEnumerator LoadSceneCoroutine()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(index);
        }
    }

    public void RestartGame()
    {
        blackScreen.GetComponent<Animator>().Play("Blackout");
        int aktifIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(aktifIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitted.");
        Application.Quit();
    }


}
