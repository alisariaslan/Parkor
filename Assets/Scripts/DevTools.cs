using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DevTools : MonoBehaviour
{
	public InputField input;

	private CarController carController;
	private PlayerController playerController;
	private GameObject player;
	private SwitchItem switchItem;

	void Start()
	{
		carController = FindAnyObjectByType<CarController>();
		switchItem = FindAnyObjectByType<SwitchItem>();
		playerController = FindAnyObjectByType<PlayerController>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void InitCommand()
	{
		Veritabani veritabani = FindAnyObjectByType<Veritabani>();

		int acikBolumler = PlayerPrefs.GetInt("acikBolumler");
		switch (input.text)
		{
			case "reload":
				if (playerController != null)
					playerController.Reload();
				else
					input.text = "Reload error.";
				break;
			case "carry":
				FlyerBossHelperController flyerBossHelperController = FindAnyObjectByType<FlyerBossHelperController>();
				if (flyerBossHelperController != null)
				{
					player.transform.position = flyerBossHelperController.transform.position;
				}
				else
					input.text = "Carry error.";
				break;
			case "checkpoint":
				GameObject checkpoint = GameObject.Find("Checkpoint");
				if (checkpoint == null)
					input.text = "No checkpoint.";
				else
				{
					player.transform.position = checkpoint.transform.position;
				}
				break;
			case "end":
				GameObject endpoint = GameObject.Find("Endpoint");
				if (endpoint == null)
					input.text = "No end.";
				else
				{
					player.transform.position = endpoint.transform.position;
				}
				break;
			case "reset":
				PlayerPrefs.SetInt("pistol", 0);
                PlayerPrefs.SetInt("light", 0);
                if (switchItem != null)
					switchItem.CheckEnvanter();
				input.text = "Inventory resetted.";
				break;
			case "give pistol":
                PlayerPrefs.SetInt("pistol", 1);
                if (switchItem != null)
					switchItem.CheckEnvanter();
				input.text = "Gun added to inventory.";
				break;
            case "give light":
                PlayerPrefs.SetInt("light", 1);
                if (switchItem != null)
                    switchItem.CheckEnvanter();
                input.text = "Flashlight added to inventory.";
                break;
            case "exit":
				PlayerPrefs.SetInt("devtools", 0);
				SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
				break;
			case "heal":
				if (playerController != null)
				{
					playerController.health = playerController.maxHealth;
					input.text = "PlayerHuman healed: " + playerController.maxHealth;
				}
				else
				{
					if (carController != null)
					{
						carController.carHealth = carController.maxHealth;
						input.text = "PlayerCar healed: " + carController.maxHealth;
					}
					else
					{
						input.text = "No player found.";
					}
				}
				break;
			case "unlock":
				if (veritabani != null)
					input.text = veritabani.Unlock();
				else goto default;
				break;
			case "lock":
				if (veritabani != null)
					input.text = veritabani.Lock();
				else goto default;
				break;
			case "unlock all":
				if (veritabani != null)
					input.text = veritabani.UnlockAll();
				else goto default;
				break;
			case "lock all":
				if (veritabani != null)
					input.text = veritabani.LockAll();
				else goto default;
				break;
			default:
				input.text = "No command found.";
				break;
		}
	}
}
