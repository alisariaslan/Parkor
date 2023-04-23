using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchItem : MonoBehaviour
{
	public List<Button> buttonList;
	private PlayerController playerController;

	void Start()
	{
		playerController = FindAnyObjectByType<PlayerController>();
		if (playerController != null)
			CheckEnvanter();
		else
			ResetInventoryDiagram();
	}

	public void ResetInventoryDiagram()
	{
		foreach (Button button in buttonList)
		{
			if (button == buttonList[0])
				continue;
			button.interactable = false;
			button.gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	public void CheckEnvanter()
	{
		ResetInventoryDiagram();
		int IsPlayerHasLight = PlayerPrefs.GetInt("light", 0);
		int IsPlayerHasPistol = PlayerPrefs.GetInt("pistol", 0);
		if (IsPlayerHasLight == 1)
		{
			buttonList[1].interactable = true;
			buttonList[1].gameObject.transform.GetChild(0).gameObject.SetActive(true);
		}
		if (IsPlayerHasPistol == 1)
		{
			buttonList[2].interactable = true;
			buttonList[2].gameObject.transform.GetChild(0).gameObject.SetActive(true);
		}
	}

	public void Switch(int itemNo)
	{
		FindAnyObjectByType<CanvasManager>().Ammo.SetActive(false);
		switch (itemNo)
		{
			case 0:
				playerController.NoItem();
				break;
			case 1:
				playerController.ItemSet(playerController.fener);
				break;
			case 2:
				playerController.ItemSet(playerController.pistol);
				break;
		}
	}
}
