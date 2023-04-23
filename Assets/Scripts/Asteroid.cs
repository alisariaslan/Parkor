using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject booom;
    public AudioClip audio1, audio2, audio3, audio4, audio5;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
   
    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0f, 0.0f, Time.deltaTime * 100, Space.Self);
    }

    public void boom()
    {

        StartCoroutine(BOM());
        IEnumerator BOM()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(Random.Range(.25f, 1));
                Instantiate(booom, transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity);
            }
            yield return new WaitForSeconds(1);
            LevelManager levelManager = FindAnyObjectByType<LevelManager>();
            levelManager.NextLevel("AraSahne");
        }
       

    }

    public void PlaySound(int soundNo)
    {
        switch (soundNo)
        {
            case 1:
                audioSource.PlayOneShot(audio1);
                break;
            case 2:
                audioSource.PlayOneShot(audio2);
                break;
            case 3:
                audioSource.PlayOneShot(audio3);
                break;
            case 4:
                audioSource.PlayOneShot(audio4);
                break;
            case 5:
                audioSource.PlayOneShot(audio5);
                break;
        }




    }

}
