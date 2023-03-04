using UnityEngine;

public class EndSound : MonoBehaviour
{
    public AudioClip newSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.MuteAudio();
        if (newSound != null)
            levelManager.PlayAudio(newSound, true);
        GameObject.Destroy(this.transform.gameObject);
    }

}
