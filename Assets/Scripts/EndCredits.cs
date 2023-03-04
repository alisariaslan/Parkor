using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCredits : MonoBehaviour
{
    public TextAsset textAsset;
    private Text text;
    CanvasManager canvasManager;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = textAsset.text;
        canvasManager = FindObjectOfType<CanvasManager>();
        canvasManager.Ilistir(false, false, true, false, false);
        canvasManager.Aydinlat();
    }

    public void NextScene()
    {
        canvasManager.Karart();
        StartCoroutine(ExampleCoroutine());
        IEnumerator ExampleCoroutine()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

}
