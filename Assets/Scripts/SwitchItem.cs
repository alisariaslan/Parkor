using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchItem : MonoBehaviour
{
    public Button slot1, slot2, slot3;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
            CheckEnvanter();
        else
        {
            slot1.interactable = false;
            slot2.interactable = false;
            slot3.interactable = false;
        }
    }



    // Update is called once per frame
    public void CheckEnvanter()
    {

        int envanter = PlayerPrefs.GetInt("Envanter", 0);
        //print("env:" + envanter);
        if (envanter == 0)
        {
            slot2.interactable = false;
            slot2.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            slot3.interactable = false;
            slot3.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (envanter == 1)
        {
            slot2.interactable = true;
            slot2.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            slot3.interactable = false;
            slot3.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (envanter == 2)
        {
            slot2.interactable = true;
            slot2.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            slot3.interactable = true;
            slot3.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Switch(int itemNo)
    {
        FindObjectOfType<CanvasManager>().Sarjor.SetActive(false);
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
