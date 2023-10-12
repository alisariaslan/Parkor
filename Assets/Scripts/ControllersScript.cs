using UnityEngine;

public class ControllersScript : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject menuButton;
    public bool menuButtonIsActive = true;
    public GameObject inventoryButton;
    public bool inventoryButtonIsActive = true;
    public GameObject consoleButton;
    public bool consoleButtonIsActive = true;
    public GameObject goLeftButton;
    public bool goLeftButtonIsActive = true;
    public GameObject goRightButton;
    public bool goRightButtonIsActive = true;
    public GameObject jumpButton;
    public bool jumpButtonIsActive = true;

    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject inventoryPanel;
    public GameObject consolePanel;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform is false && FindObjectOfType<LevelManager>().forceMobile is false)
            this.gameObject.SetActive(false);
        else
        {
            menuButton.SetActive(menuButtonIsActive);
            inventoryButton.SetActive(inventoryButtonIsActive);
            var cheats = PlayerPrefs.GetInt("devtools", 0) is 1 ? true : false;
            if (consoleButtonIsActive)
                consoleButton.SetActive(cheats);
            else consoleButton.SetActive(false);
            goLeftButton.SetActive(goLeftButtonIsActive);
            goRightButton.SetActive(goRightButtonIsActive);
            jumpButton.SetActive(jumpButtonIsActive);
        }
    }

    public void OpenInventory()
    {
        if (inventoryButtonIsActive is false)
            return;
        if (inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
            if (inventoryButtonIsActive)
                inventoryButton.SetActive(true);
            if (menuButtonIsActive)
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
        if (menuButtonIsActive is false)
            return;
        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
            if (menuButtonIsActive)
                menuButton.SetActive(true);
            if (inventoryButtonIsActive)
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
        if (consoleButtonIsActive is false)
            return;
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
