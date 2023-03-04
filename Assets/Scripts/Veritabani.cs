using System.Data;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Veritabani : MonoBehaviour
{


    private Array bolumler;
    private int acikBolumSayisi = 0;
    private int toplamBolumSayisi = 0;

    private void Start()
    {
        acikBolumSayisi = PlayerPrefs.GetInt("acikBolumler", 0);
        if (acikBolumSayisi == 0)
        {
            PlayerPrefs.SetInt("acikBolumler", 1);
            acikBolumSayisi = 1;
        }
           
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
        for (int i = 0; i < toplamBolumSayisi; i++)
        {
            if (i < acikBolumSayisi)
                transform.GetChild(i).GetComponent<BolumSec>().Unlock();
            else
                transform.GetChild(i).GetComponent<BolumSec>().Lock();
        }

    }

    public string BolumleriAc()
    {
        PlayerPrefs.SetInt("acikBolumler", toplamBolumSayisi);
        acikBolumSayisi = toplamBolumSayisi;
        Check();
        return toplamBolumSayisi + " b�l�m acildi";
    }

    public string BolumleriKapa()
    {
        PlayerPrefs.SetInt("acikBolumler", 0);
        acikBolumSayisi = 0;
        Check();
        return toplamBolumSayisi + " b�l�m kilitlendi";
    }

    public string BolumKapa()
    {
        if (acikBolumSayisi > 0)
        {
            acikBolumSayisi--;
            PlayerPrefs.SetInt("acikBolumler", acikBolumSayisi);
            Check();
            return "1 B�l�m Kilitlendi.";
        }
        else
        {
            return "Hata! T�m b�l�mler kilitli!";
        }
    }

    public string BolumAc()
    {
        if (acikBolumSayisi < toplamBolumSayisi)
        {
            acikBolumSayisi++;
            PlayerPrefs.SetInt("acikBolumler", acikBolumSayisi);
            Check();
            return "1 B�l�m A��ld�.";
        }
        else
        {
            return "Hata! T�m b�l�mler a��k!";
        }
    }
}






