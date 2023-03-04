using UnityEngine;

public class SpiderBossHead : MonoBehaviour
{
    private AudioSource audioSource;
    public SpiderBoss spiderBoss;
    private PlayerController playerController;

    public int lives = 10;
    public bool isStingBroken;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.transform.name.Equals("PlayerHuman"))
            {
                if (isStingBroken)
                {
                    lives--;
                    playerController.Bounce();
                    audioSource.Play();
                    if (lives == 0)
                        spiderBoss.Dead();
                }
            }
            else if (collision.transform.name.Equals("PlayerUcak"))
                {
                spiderBoss.Dead();

            }
            else if (collision.CompareTag("explosion"))
            {
                spiderBoss.Dead();
            }

        }
    }
}
