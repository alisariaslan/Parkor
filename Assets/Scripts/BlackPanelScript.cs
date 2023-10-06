using UnityEngine;

public class BlackPanelScript : MonoBehaviour
{
    public GameObject tutorials;

    async void Start()
    {
        if(tutorials is not null)
        {
            await System.Threading.Tasks.Task.Delay(2000);
            var enabled = FindObjectOfType<LevelManager>().tutorials;
            if (enabled)
                tutorials.SetActive(true);
        }
    }

}
