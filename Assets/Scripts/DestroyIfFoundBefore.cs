using UnityEngine;

public class DestroyIfFoundBefore : MonoBehaviour
{
    public string itemName;
    void Start()
    {
        if (PlayerPrefs.GetInt(itemName) == 1)
            Destroy(this.gameObject);
    }
}
