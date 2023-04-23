using UnityEngine;

public class PlayEvent : MonoBehaviour
{
    public bool CarryNow;
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
        if (collision.CompareTag("Player"))
        {
            if (CarryNow)
            {
                FlyerBossHelperController flyerBossHelperController = FindAnyObjectByType<FlyerBossHelperController>();
                flyerBossHelperController.CarryNow();
            }
        }
    }
}
