using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LangHelper : MonoBehaviour
{
    public string key;

    void Start()
    {
        GetComponent<Text>().text = GetLanguageValue(key);
    }

    public const string noLangWithThatKey = "SPECIFIED LANGUAGE KEY IS NOT FOUND IN JSON FILE!";

    public static string GetLanguageValue(string key)
    {
        CultureInfo culture = CultureInfo.CurrentCulture;
        string defaultLanguage = culture.TwoLetterISOLanguageName;
        var textFile = Resources.Load<TextAsset>("Localization/"+defaultLanguage);
        if (textFile is not null)
            return FindWithKey(textFile, key, false);
        else
        {
            textFile = Resources.Load<TextAsset>("Localization/en");
            return FindWithKey(textFile, key, true);
        }
    }

    private static string FindWithKey(TextAsset asset, string key, bool returnDefault)
    {
        string jsonText = asset.text;
        LangItem[] list = JsonHelper.getJsonArray<LangItem>(jsonText);
        LangItem item = System.Array.Find(list, x => x.key == key);
        if (item != null)
        {
            return item.value;
        }
        else
        {
            Debug.LogError("Language key not found: " + key);
#if DEBUG
            return noLangWithThatKey;
#else
            return "";
#endif
        }
    }

    [System.Serializable]
    public class LangItem
    {
        public string key;
        public string value;
    }

    public class JsonHelper
    {
        public static T[] getJsonArray<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }

}
