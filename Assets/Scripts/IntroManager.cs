using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    //public int geriSayim = 10;
    //private float sayim;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartCountdown());
        
    }
   
    //public IEnumerator StartCountdown()
    //{
        //sayim = geriSayim;
        //while (sayim > 0)
        //{
            //Debug.Log("Countdown: " + sayim);
        //    yield return new WaitForSeconds(1.0f);
        //    sayim--;
        //}
        //NextScene();
    //}

    private void NextScene()
    {
       
            SceneManager.LoadScene("AraSahne");
        
    }

}
