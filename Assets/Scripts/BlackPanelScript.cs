using System.Collections;
using UnityEngine;

public class BlackPanelScript : MonoBehaviour
{
    public GameObject tutorials;

    void Start()
    {
        if(tutorials is not null)
        {
            StartCoroutine(LoadSceneCoroutine());
            IEnumerator LoadSceneCoroutine()
            {
                yield return new WaitForSeconds(2);
                var enabled = FindObjectOfType<LevelManager>().tutorials;
                if (enabled)
                    tutorials.SetActive(true);
            }
         
        }
    }

}
