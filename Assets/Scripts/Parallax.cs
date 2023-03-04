using UnityEngine;

public class Parallax : MonoBehaviour
{
    public bool x = true;
    public bool y = false;
    private float startpos;
    public GameObject cam;
    public float parallaxeffect;
    public bool yEffect;
    public float parallaxEffectY;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distX = (cam.transform.position.x * parallaxeffect);
        float distY = (cam.transform.position.y * parallaxeffect);
        if(yEffect)
            distY = (cam.transform.position.y * parallaxEffectY);
        if (x && y)
            transform.position = new Vector2(startpos + distX, startpos + distY);
        else if (x)
            transform.position = new Vector2(startpos + distX, transform.position.y);
        else if (y)
            transform.position = new Vector2(transform.position.x, startpos + distY);

    }
}
