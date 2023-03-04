using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceWalkSound : MonoBehaviour
{
    public AudioClip sound;

    private AudioClip oldSound;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.name.Equals("PlayerHuman"))
            {
                Debug.Log("replaceSound enter");
                playerController.audioSource.Stop();
                oldSound = playerController.walkSound;
                playerController.inReplace = true;
                playerController.walkSound = sound;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.name.Equals("PlayerHuman"))
            {
                Debug.Log("replaceSound exit");
                if (!playerController.jumping)
                    playerController.audioSource.Stop();
                playerController.walkSound = oldSound;
                playerController.inReplace = false;
            }

        }
    }
}
