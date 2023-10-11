using UnityEngine;

public class KozaHead : MonoBehaviour
{
    KozaController kozaController;

    void Start()
    {
        kozaController = GetComponentInParent<KozaController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.name.Equals("PlayerHuman"))
            {
                PlayerController playerController = FindAnyObjectByType<PlayerController>();
                kozaController.Damage(playerController.jumpDamage, "hop");
                playerController.Bounce();
            }
        }
    }
}
