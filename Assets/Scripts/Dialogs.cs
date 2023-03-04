using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogs : MonoBehaviour
{
   
    public static string Text(int no,TextAsset textAsset)
    {

        string aText = "Boþ -> No=0\n" + textAsset.text;
        string[] texts = aText.Split('\n');
        string bText = texts[no];
    
        return bText;
    }
}
