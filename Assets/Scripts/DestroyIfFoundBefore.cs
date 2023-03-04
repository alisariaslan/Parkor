using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfFoundBefore : MonoBehaviour
{

    public int itemNo = 0;
    // Start is called before the first frame update
    void Start()
    {
       
        if (PlayerPrefs.GetInt("Envanter") > itemNo)
            GameObject.Destroy(gameObject);
    }


}
