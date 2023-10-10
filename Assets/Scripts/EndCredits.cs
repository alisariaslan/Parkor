using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    public void NextScene()
    {
        FindObjectOfType<CanvasManager>().Karart();
        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

}
