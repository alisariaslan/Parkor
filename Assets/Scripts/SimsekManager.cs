using UnityEngine;

public class SimsekManager : MonoBehaviour
{
    public GameObject simsek;
    public AudioClip simsekSound;

    public int firstInt = 10;
    private int randomInt = 0;
    public int maxNextLightningDuration = 100;
    private LevelManager levelManager;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        randomInt = firstInt;
        nextUpdate = firstInt;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + randomInt;
            //Debug.Log(randomInt);
            UpdateEverySecond();
        }
    }
    private int nextUpdate;

    public void Cak(bool around, Vector3 pos)
    {
        int randomPosX = Random.Range(-100, 100);
        if (!around)
            randomPosX = 0;
        GameObject instanted = Instantiate(simsek, new Vector3(pos.x + randomPosX, pos.y + 5, 0), Quaternion.identity);
        GameObject.Destroy(instanted, .1f);
        //Debug.Log(transform.position.ToString());
        float gurultu = 1f;
        float uzaklik = 1f;
        if (randomPosX < 0)
            randomPosX *= -1;
        uzaklik = randomPosX;
        if (uzaklik < 10)
            gurultu = 10f;
        else if (uzaklik < 20)
            gurultu = 9f;
        else if (uzaklik < 30)
            gurultu = 8f;
        else if (uzaklik < 40)
            gurultu = 7f;
        else if (uzaklik < 50)
            gurultu = 6f;
        else if (uzaklik < 60)
            gurultu = 5f;
        else if (uzaklik < 70)
            gurultu = 4f;
        else if (uzaklik < 80)
            gurultu = 3f;
        else if (uzaklik < 90)
            gurultu = 2f;
        else if (uzaklik < 10)
            gurultu = 1f;
        audioSource.PlayOneShot(simsekSound, gurultu);
    }
    void UpdateEverySecond()
    {
        randomInt = Random.Range(0, maxNextLightningDuration);
        Cak(true, transform.position);
    }


}
