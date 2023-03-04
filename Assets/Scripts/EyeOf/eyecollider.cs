using UnityEngine;

public class eyecollider : MonoBehaviour
{
    eyeof eyeof;
    // Start is called before the first frame update
    void Start()
    {
        eyeof = GetComponentInParent<eyeof>();
    }

    // Update is called once per frame
    void Update()
    {

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
