using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDestroyOnStart : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(GetComponent<SpriteRenderer>());
    }

   
}
