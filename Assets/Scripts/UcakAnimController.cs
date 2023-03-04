using UnityEngine;

public class UcakAnimController : MonoBehaviour
{
    public float pSpeed = 1;
    public GameObject sagP, solP;
    public Animator animator_ucak, animator_onT, animator_arkaT1, animator_arkaT2;
    public bool flying;
    private PlayerController player;
    private AudioSource audioSource;
    public AudioClip audio1, audio2, audio3;
    private CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        player = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {
        if (flying)
        {
            //player.transform.position = transform.position;
            sagP.transform.Rotate(Vector3.forward, 45 * Time.deltaTime * pSpeed);
            solP.transform.Rotate(Vector3.forward, 45 * Time.deltaTime * pSpeed);
            pSpeed += .01f;
        }
    }

    public void Stop()
    {
        flying = false;
    }

    public void GetIn()
    {
        GameObject.Destroy(player.gameObject);
        //player.ItemSet(false, false);
        cameraController.player = gameObject;
        flying = true;
        audioSource.Play();
    }

    public void TekerlekKapa()
    {
        animator_onT.Play("onTKalkis");
        animator_arkaT1.Play("arkaT1Kalkis");
        animator_arkaT2.Play("arkaT2Kalkis");
        audioSource.PlayOneShot(audio1);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            animator_ucak.Play("ucakKalkis");


        }
    }


}
