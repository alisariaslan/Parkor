using UnityEngine;

public class SpiderBossWakeUp : MonoBehaviour
{
    public SpiderBoss spiderBoss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(collision.transform.name.Equals("PlayerHuman"))
            {
                spiderBoss.WakeUp();
                Destroy(this);
            }
          
        }

    }
}
