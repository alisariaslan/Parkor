using UnityEngine;

public class UcakController : MonoBehaviour
{
    public GameObject bomb;
    LevelManager levelManager;
    public float xSpeed = 10;
    public float ySpeed = 10;
    public float roatationZ = 0;
    public GameObject sagP, solP, merkez;
    public bool flying;
    private AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip audio1, audio2, audio3;
    private Rigidbody2D rigidbody2Da;
    CameraController cameraController;
    private bool crashed;
	private ControllersScript controllersScript;
	private float horizontal = 0f;
	private float vertical = 0f;
	private bool release = false;

	void Start()
    {
		controllersScript = FindAnyObjectByType<ControllersScript>();
		cameraController = FindAnyObjectByType<CameraController>();
        audioSource = GetComponent<AudioSource>();
        levelManager = FindAnyObjectByType<LevelManager>();
        rigidbody2Da = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!crashed)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            if (Input.GetKeyUp(KeyCode.Escape))
            {
				controllersScript.OpenMenu();
            }
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.y < Screen.height / 5)
                {
                    vertical = Input.touches[0].maximumPossiblePressure;
                }
                else if (touch.position.y > Screen.height / 1.2f)
                {
                    release = true;
                }
                else if (touch.position.x < Screen.width / 3)
                {
                    horizontal = Input.touches[0].maximumPossiblePressure * -1;
                }
                else if (touch.position.x > Screen.width / 3)
                {
                    horizontal = Input.touches[0].maximumPossiblePressure;
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);
                if (touch2.position.y < Screen.height / 5)
                {
                    vertical = Input.touches[0].maximumPossiblePressure;
                }
                if (touch1.position.x < Screen.width / 3)
                {
                    horizontal = Input.touches[0].maximumPossiblePressure * -1;
                }
                else if (touch1.position.x > Screen.width / 3)
                {
                    horizontal = Input.touches[0].maximumPossiblePressure;
                }
            }
            else
            {
                if (release)
                {
					controllersScript.OpenMenu();
                    release = false;
                }
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
            rigidbody2Da.velocity = new Vector2(-xSpeed, -transform.localRotation.z * 100);
            sagP.transform.Rotate(Vector3.forward, xSpeed);
            solP.transform.Rotate(Vector3.forward, xSpeed);
        }
    }

    private void ReleaseBomb()
    {
        if (!audioSource.isPlaying)
        {

            GameObject bomba = Instantiate(bomb, merkez.transform.position, Quaternion.identity);
            Rigidbody2D rigidbody2DBomba = bomba.GetComponent<Rigidbody2D>();
            rigidbody2DBomba.velocity = rigidbody2Da.velocity;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!crashed)
            Crash();
    }
}
