using UnityEngine;

public class CloseTutorialsButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
            FindObjectOfType<CanvasManager>().CloseTutorial();
    }
}
