using UnityEngine;

public class WormEyes : MonoBehaviour
{
    WormController wormController;
  
    // Start is called before the first frame update
    void Start()
    {
        wormController = GetComponentInParent<WormController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wormController.Saw(collision.gameObject);
           
        }
    }
}
