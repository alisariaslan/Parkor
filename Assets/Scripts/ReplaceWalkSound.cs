using UnityEngine;

public class ReplaceWalkSound : MonoBehaviour
{
    public AudioClip sound;
    private AudioClip oldSound;
    private PlayerController playerController;

    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.name.Equals("PlayerHuman"))
            {
                playerController.audioSource.Stop();
                oldSound = playerController.walkSound;
                playerController.inReplace = true;
                playerController.walkSound = sound;
            }
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.name.Equals("PlayerHuman"))
            {
                if (!playerController.jumping)
                    playerController.audioSource.Stop();
                playerController.walkSound = oldSound;
                playerController.inReplace = false;
            }

        }
    }
}
