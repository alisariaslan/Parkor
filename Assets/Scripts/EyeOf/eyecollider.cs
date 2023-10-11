using UnityEngine;

public class eyecollider : MonoBehaviour
{
    eyeof eyeof;

    void Start()
    {
        eyeof = GetComponentInParent<eyeof>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fener"))
        {
            Destroy(this);
            eyeof.Dead();
        }
    }
}
