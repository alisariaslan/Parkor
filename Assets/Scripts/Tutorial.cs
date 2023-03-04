using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public string topString, bottomString, rightString, leftString;
    public Text topText, bottomText, rightText, leftText;
    public Animator topAnim, bottomAnim, rightAnim, leftAnim;
    private bool destroyed;
    // Start is called before the first frame update

    public void Destroy()
    {
        destroyed = true;
        GameObject.Destroy(topAnim.transform.gameObject);
        GameObject.Destroy(leftAnim.transform.gameObject);
        GameObject.Destroy(rightAnim.transform.gameObject);
        GameObject.Destroy(bottomAnim.transform.gameObject);
    }

    void Start()
    {




        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1);
            if (!destroyed)
            {
                topText.text = topString;
                leftText.text = leftString;
                rightText.text = rightString;
                bottomText.text = bottomString;



                if (topString != "")
                    topAnim.enabled = true;
                else
                    GameObject.Destroy(topAnim.transform.gameObject);
                if (leftString != "")
                    leftAnim.enabled = true;
                else
                    GameObject.Destroy(leftAnim.transform.gameObject);
                if (rightString != "")
                    rightAnim.enabled = true;
                else
                    GameObject.Destroy(rightAnim.transform.gameObject);
                if (bottomString != "")
                    bottomAnim.enabled = true;
                else
                    GameObject.Destroy(bottomAnim.transform.gameObject);


                yield return new WaitForSeconds(5);

            }
        }

    }

}
