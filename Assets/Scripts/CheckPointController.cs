using UnityEngine;

public class CheckPointController : MonoBehaviour
{

    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelManager.gotCheckpoint = true;
            levelManager.spawnpoint = transform.position;
            print(collision.gameObject.name + " checkpoint reached.");
            GameObject.Destroy(gameObject);
        }
    }
}
