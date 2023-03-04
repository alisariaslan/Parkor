using UnityEngine;

public class WormBossEyes : MonoBehaviour
{
	public WormBossController wormBossController;
	public int seen = 0;

	[HideInInspector]
	public bool musicOff = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.CompareTag("Player"))
		{
			if (seen == 0)
			{
				if (!musicOff)
					GetComponent<AudioSource>().Play();
				seen = 1;
			}

			if (seen == 1)
			{
				wormBossController.Seen();
				seen = 2;
			}
		}
	}
}
