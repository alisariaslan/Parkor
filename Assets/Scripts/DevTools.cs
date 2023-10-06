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
			case "yardim":
				LevelManager levelManager = FindAnyObjectByType<LevelManager>();
				if (levelManager != null)
				{
					levelManager.Say("yardim,doldur,tasi,isinlan,sona isinlan,silah sil,silah ekle,kapat,kayit sil,kayit ac,canver,bolum ac,bolum kilitle,bolumleri ac,bolumleri kilitle,basar", 0.5f, false);
				}
				else
					input.text = "yardim komutu sadece oyun icinde calisir.";
				break;
			case "doldur":
				if (playerController != null)
					playerController.Reload();
				else
					input.text = "doldur komutu sadece oyun icinde calisir.";
				break;
			case "tasi":
				FlyerBossHelperController flyerBossHelperController = FindAnyObjectByType<FlyerBossHelperController>();
				if (flyerBossHelperController != null)
				{
					player.transform.position = flyerBossHelperController.transform.position;
					input.text = "helper bulundu.";
				}
				else
					input.text = "helper bulunamadi!";
				break;
			case "isinlan":
				GameObject checkpoint = GameObject.Find("Checkpoint");
				if (checkpoint == null)
					input.text = "Nokta bulunamadi.";
				else
				{
					player.transform.position = checkpoint.transform.position;
					input.text = "Noktaya isinlanildi.";
				}
				break;
			case "sona isinlan":
				GameObject endpoint = GameObject.Find("Endpoint");
				if (endpoint == null)
					input.text = "Son bulunamadi.";
				else
				{
					player.transform.position = endpoint.transform.position;
					input.text = "Sona isinlanildi.";
				}
				break;
			case "silah sil":
				PlayerPrefs.SetInt("Envanter", 0);
				if (switchItem != null)
					switchItem.CheckEnvanter();
				input.text = "Silahlar sifirlandi.";
				break;
			case "silah ekle":
				int envanter = PlayerPrefs.GetInt("Envanter", 0);
				if (envanter < 3)
					envanter++;
				PlayerPrefs.SetInt("Envanter", envanter);
				if (switchItem != null)
					switchItem.CheckEnvanter();
				input.text = "Silah verildi.";
				break;
			case "exit":
				PlayerPrefs.SetInt("devtools", 0);
				SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
				break;
			case "kayit sil":
				PlayerPrefs.SetInt("save", 0);
				SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
				break;
			case "kayit ac":
				PlayerPrefs.SetInt("save", 2);
				SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
				break;
			case "canver":
				if (playerController != null)
				{
					playerController.health = playerController.maxHealth;
					input.text = "PlayerHuman can fullendi: " + playerController.maxHealth;
				}
				else
				{
					if (carController != null)
					{
						carController.carHealth = carController.maxHealth;
						input.text = "PlayerCar can fullendi: " + carController.maxHealth;
					}
					else
					{
						input.text = "Hata! PlayerCar veya PlayerHuman bulunamadi.";
					}
				}
				break;
			case "bolum ac":
				if (veritabani != null)
					input.text = veritabani.BolumAc();
				else goto default;
				break;
			case "bolum kilitle":
				if (veritabani != null)
					input.text = veritabani.BolumKapa();
				else goto default;
				break;
			case "bolumleri ac":
				if (veritabani != null)
					input.text = veritabani.BolumleriAc();
				else goto default;
				break;
			case "bolumleri kilitle":
				if (veritabani != null)
					input.text = veritabani.BolumleriKapa();
				else goto default;
				break;
			case "basar":
				Social.ReportProgress("CgkIwt3e_8MYEAIQCg", 100.0f, (bool success) =>
				{
					input.text = success.ToString();
				});
				break;
			default:
				input.text = "Ge�ersiz komut!";
				break;
		}
	}
}
