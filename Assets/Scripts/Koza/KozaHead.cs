using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KozaHead : MonoBehaviour
{
    KozaController kozaController;
    // Start is called before the first frame update
    void Start()
    {
        kozaController = GetComponentInParent<KozaController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.transform.name.Equals("PlayerHuman"))
            {
                PlayerController playerController = FindAnyObjectByType<PlayerController>();
                kozaController.Damage(playerController.jumpDamage,"hop");
                playerController.Bounce();
            } 
        }
    }
}
