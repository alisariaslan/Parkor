using System;
using UnityEngine;

public class UcakController : MonoBehaviour
{
    public GameObject bomb;
    LevelManager levelManager;
    public float xSpeed = 10f;
    public float ySpeed = 10f;
    public float yGravity = 0.1f;
    public float dropSpeed = -10f;
    public float roatationZ = 0;
    public GameObject sagP, solP, merkez;
    public bool flying;
    private AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip audio1, audio2, audio3;
    CameraController cameraController;
    private bool crashed;
    private float horizontal = 0f;
    private float vertical = 0f;
    private CanvasManager canvasManager;

    [Header("Buttons")]
    public ControllersButtonScript gotoleftButtonScript, gotorightButtonScript, jumpButtonScript;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbdy;

    void Start()
    {
        cameraController = FindAnyObjectByType<CameraController>();
        audioSource = GetComponent<AudioSource>();
        levelManager = FindAnyObjectByType<LevelManager>();
        rigidbdy = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        canvasManager = FindAnyObjectByType<CanvasManager>();
    }

    void Update()
    {
        if (!crashed)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                canvasManager.OpenMenu();
            }
            if (Input.GetKeyUp(KeyCode.BackQuote))
            {
                canvasManager.OpenConsole();
            }

            if (Application.isMobilePlatform || levelManager.forceMobile)
            {
                if (gotoleftButtonScript.buttonPressed)
                    horizontal = -1;
                else if (gotorightButtonScript.buttonPressed)
                    horizontal = 1;
                if (jumpButtonScript.buttonPressed)
                    vertical = 1;
            }

            if (vertical > 0)
            {
                ReleaseBomb();
            }
            if (horizontal < 0)
            {
                if (transform.rotation.z < .5f)
                    transform.Rotate(new Vector3(0, 0, -horizontal / 5));
            }
            else if (horizontal > 0)
            {
                if (transform.rotation.z > -.5f)
                    transform.Rotate(new Vector3(0, 0, -horizontal / 5));
            }
            else
            {
                if (transform.rotation.z > 0)
                    transform.Rotate(new Vector3(0, 0, -.2f));
                else if (transform.rotation.z < 0)
                    transform.Rotate(new Vector3(0, 0, .2f));
            }

            //rigidbody2Da.velocity = new Vector2(-xSpeed, -transform.localRotation.z * 100);

            float y = transform.localRotation.z;
            y *= -ySpeed;

            Vector3 hareket = new Vector3(xSpeed, y, 0f);
            transform.position += hareket * Time.deltaTime;

            sagP.transform.Rotate(Vector3.forward, xSpeed);
            solP.transform.Rotate(Vector3.forward, xSpeed);
        }
    }

    private void ReleaseBomb()
    {
        if (!audioSource.isPlaying)
        {
            GameObject bomba = Instantiate(bomb, merkez.transform.position, Quaternion.identity);
            bomba.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed/2, dropSpeed);
            audioSource.PlayOneShot(audio1, 5f);
        }
    }

    public void Crash()
    {
        crashed = true;
        audioSource2.Stop();
        audioSource.PlayOneShot(audio2, 5f);
        cameraController.offsetY = 0;
        levelManager.Say("Kaza yaptýn", 1f, false);
        levelManager.Restart();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(boxCollider);
            rigidbdy.isKinematic = false;
            Crash();
        }
    }
}
