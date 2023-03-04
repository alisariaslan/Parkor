
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepOnFlower : MonoBehaviour
{
    public Sprite ezilmisCicek;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {

            GetComponent<SpriteRenderer>().sprite = ezilmisCicek;
        }
    }




}
