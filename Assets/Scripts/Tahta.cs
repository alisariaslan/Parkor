using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tahta : MonoBehaviour
{
    public Sprite broke;
    public AudioClip drop, breaking;
    public void Break()
    {
        GetComponent<AudioSource>().PlayOneShot(breaking);
        GetComponent<SpriteRenderer>().sprite = broke;
        Destroy(GetComponent<BoxCollider2D>());
        GameObject.Destroy(gameObject, 10f);
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            if (transform.rotation.z > 10 | transform.rotation.z < 10)
            {
                if (GetComponent<Rigidbody2D>().velocity.y < 0)
                    if (!GetComponent<AudioSource>().isPlaying)
                        GetComponent<AudioSource>().PlayOneShot(drop);
            }
        }

    }
}
