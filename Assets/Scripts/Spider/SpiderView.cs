using UnityEngine;

public class SpiderView : MonoBehaviour
{
    private SpiderController spiderController;
    private void Start()
    {
        spiderController = GetComponentInParent<SpiderController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spiderController.Saw(collision.gameObject);
        }
    }


}
