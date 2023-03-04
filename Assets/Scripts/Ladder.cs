using UnityEngine;

public class Ladder : MonoBehaviour
{
    private PlayerController player;
	public bool gun_remove = true;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gun_remove)
        {
            player.ladder = true;
            player.NoItem();
            Debug.Log("Ladder enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && gun_remove)
        {
            player.ladder = false;
            player.NoItem();
            Debug.Log("Ladder exit");
        }
        
    }
}
