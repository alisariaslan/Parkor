using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGen : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1, sprite2, sprite3, sprite4, sprite5;
  
    // Start is called before the first frame update
    void Start()
    {
        int rnd = Random.Range(0, 5);
        switch (rnd)
        {
            case 0: spriteRenderer.sprite = sprite1; break;
            case 1: spriteRenderer.sprite = sprite2; break;
            case 2: spriteRenderer.sprite = sprite3; break;
            case 3: spriteRenderer.sprite = sprite4; break;
            case 4: spriteRenderer.sprite = sprite5; break;
            default: spriteRenderer.sprite = sprite1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
