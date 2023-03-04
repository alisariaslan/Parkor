using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public bool followPlayer = true;
    public float offsetX = 5, offsetY = 0;
    private Vector3 playerPosition;
    public float offsetSmootthing;
    public int facing = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            //Debug.Log("Payer found. Tag:" + player.transform.tag);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            if (player != null)
            {
                playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
                if (facing > 0)
                {
                    playerPosition = new Vector3(playerPosition.x + offsetX, playerPosition.y + offsetY, playerPosition.z);
                }
                else
                {
                    playerPosition = new Vector3(playerPosition.x - offsetX, playerPosition.y + offsetY, playerPosition.z);

                }
                transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmootthing * Time.deltaTime);
            }
        }
    }
}
