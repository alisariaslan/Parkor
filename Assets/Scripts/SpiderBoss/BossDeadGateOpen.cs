using UnityEngine;

public class BossDeadGateOpen : MonoBehaviour
{
    public bool open;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (open)
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(transform.GetChild(0).gameObject);
        }
           
    }

    public void Op()
    {
        open = true;
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(transform.GetChild(0).gameObject);
    }
      
  


}
