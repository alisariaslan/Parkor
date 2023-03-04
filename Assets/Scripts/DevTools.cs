using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DevTools : MonoBehaviour
{
	public static bool devmod = false;

	public GameObject inputObject;

	private CarController carController;
	private PlayerController playerController;
	private GameObject player;
	private InputField input;
	private SwitchItem switchItem;
	// Start is called before the first frame update
	void Start()
	{
		carController = FindObjectOfType<CarController>();
		switchItem = FindObjectOfType<SwitchItem>();
		input = inputObject.GetComponent<InputField>();
		playerController = FindObjectOfType<PlayerController>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	public void InitCommand()
	{
		Veritabani veritabani = FindObjectOfType<Veritabani>();

		int acikBolumler = PlayerPrefs.GetInt("acikBolumler");
		switch (input.text)
		{
			case "yardim":
				LevelManager levelManager = FindObjectOfType<LevelManager>();
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
				FlyerBossHelperController flyerBossHelperController = FindObjectOfType<FlyerBossHelperController>();
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
				//print("env:" + envanter);
				if (switchItem != null)
					switchItem.CheckEnvanter();
				input.text = "Silah verildi.";
				break;
			case "kapat":
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
				input.text = "Geçersiz komut!";
				break;
		}

	}


}
