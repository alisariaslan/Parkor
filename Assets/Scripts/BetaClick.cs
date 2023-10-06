using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BetaClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject devtools;
    public UnityEvent onClick;
    private int a = 0;

    private void Start()
    {
        int intdevtools= PlayerPrefs.GetInt("devtools",0);
        if(intdevtools == 1)
        {
            a = 11;
            GetComponent<Text>().color = Color.red;
            devtools.SetActive(true);
            GetComponent<Text>().text = "Cheats\nACTIVE";
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        a++;
        if(a==10)
        {
            GetComponent<Text>().color = Color.red;
            devtools.SetActive(true);
			PlayerPrefs.SetInt("devtools", 1);
			GetComponent<Text>().text = "Cheats\nACTIVE";
		} else if (a==20) {
            SceneManager.LoadScene(14);
        }
        onClick.Invoke();
    }
}
