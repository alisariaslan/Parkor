using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public StringTableCollection arasahnetable;
    private Text text;
    public Animator animator;
    string[] texts;

    int a = 0;
    int posY = 0;

    void Start()
    {
        text = GetComponent<Text>();
        string introText = LocalizationSettings.StringDatabase.GetLocalizedString(arasahnetable.name, "ep3_credits");
        texts = introText.Split('\n');
        text.text = texts[a];
        RandomPositionY();
    }

    private void RandomPositionY()
    {
        posY = Random.Range(-150, 150);
        text.transform.localPosition = new Vector2(text.transform.position.x, posY);
    }

    public void nextString()
    {
        a++;
        if (texts.Length > a)
        {
            text.text = texts[a];
            RandomPositionY();
        }
        else
        {
            text.text = null;
        }
    }
}
