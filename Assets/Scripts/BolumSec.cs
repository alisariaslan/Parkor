using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolumSec : MonoBehaviour
{
    GameObject lockImage;
    Button thisButton;
    
   
    // Start is called before the first frame update
    void Start()
    {
        lockImage = transform.GetChild(1).gameObject;
        thisButton = GetComponent<Button>();
    }

    public void Unlock()
    {
        lockImage.SetActive(false);
        thisButton.interactable = true;
    }

    public void Lock()
    {
        lockImage.SetActive(true);
        thisButton.interactable = false;
    }
}
