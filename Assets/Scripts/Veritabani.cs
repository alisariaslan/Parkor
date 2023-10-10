using System.Collections;
using UnityEngine;


public class Veritabani : MonoBehaviour
{
    private int toplamBolumSayisi = 11;

    private void Start()
    {
        toplamBolumSayisi = transform.childCount;
        StartCoroutine(co());
        IEnumerator co()
        {
            yield return new WaitForSeconds(1);
            Check();
        }
    }

    private void Check()
    {
        var acikBolumSayisi = PlayerPrefs.GetInt("acikBolumler", 1);
        for (int i = 0; i < toplamBolumSayisi; i++)
        {
            if (i < acikBolumSayisi)
                transform.GetChild(i).GetComponent<BolumSec>().Unlock();
            else
                transform.GetChild(i).GetComponent<BolumSec>().Lock();
        }
    }

    public string UnlockAll()
    {
        PlayerPrefs.SetInt("acikBolumler", 11);
        Check();
        return toplamBolumSayisi + " levels unlocked.";
    }

    public string LockAll()
    {
        PlayerPrefs.SetInt("acikBolumler", 1);
        Check();
        return toplamBolumSayisi + " levels locked.";
    }

    public string Unlock()
    {
        var acikBolumSayisi = PlayerPrefs.GetInt("acikBolumler", 1);
        if (acikBolumSayisi < toplamBolumSayisi)
        {
            acikBolumSayisi++;
            PlayerPrefs.SetInt("acikBolumler", acikBolumSayisi);
            Check();
            return "1 level is unlocked.";
        }
        else
        {
            return "All levels are unlocked.";
        }
    }

    public string Lock()
    {
        var acikBolumSayisi = PlayerPrefs.GetInt("acikBolumler", 1);
        if (acikBolumSayisi > 1)
        {
            acikBolumSayisi--;
            PlayerPrefs.SetInt("acikBolumler", acikBolumSayisi);
            Check();
            return "1 level is locked.";
        }
        else
        {
            return "All levels are locked.";
        }
    }

}