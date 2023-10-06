using UnityEngine;

public class ControllersScript : MonoBehaviour
{
	public bool devtoolsCheck = true;

	[Header("Buttons")]
	public GameObject menuButton;
	public GameObject inventoryButton;
	public GameObject consoleButton;

	[Header("Panels")]
	public GameObject menuPanel;
	public GameObject inventoryPanel;
	public GameObject consolePanel;

	// Start is called before the first frame update
	void Start()
    {
		int devtools = PlayerPrefs.GetInt("devtools", 0);
		if(devtools == 0 && devtoolsCheck)
			consoleButton.SetActive(false);
		else
			consoleButton.SetActive(true);
		if(Application.isMobilePlatform is false && FindObjectOfType<LevelManager>().forceMobile is false)
			this.gameObject.SetActive(false);
	}

	public void OpenInventory()
	{
		if(inventoryPanel.activeSelf)
		{
			inventoryPanel.SetActive(false);
			inventoryButton.SetActive(true);
			menuButton.SetActive(true);
		}
		else
		{
			inventoryPanel.SetActive(true);
			inventoryButton.SetActive(false);
			menuButton.SetActive(false);
		}
	}

	public void OpenMenu()
	{
		if (menuPanel.activeSelf)
		{
			menuPanel.SetActive(false);
			menuButton.SetActive(true);
			inventoryButton.SetActive(true);
		}
		else
		{
			menuPanel.SetActive(true);
			menuButton.SetActive(false);
			inventoryButton.SetActive(false);
		}
	}

	public void OpenConsole()
	{
		if (consolePanel.activeSelf)
		{
			consolePanel.SetActive(false);
			consoleButton.SetActive(true);
		}
		else
		{
			consolePanel.SetActive(true);
			consoleButton.SetActive(false);
		}
	}
}
