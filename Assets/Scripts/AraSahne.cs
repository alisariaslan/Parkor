using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System;
public class AraSahne : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip typing;

	public bool test;
	public string adUnitId;
	public TextAsset textAsset;
	public GameObject devamText;
	public bool ready;

	private Text text1;

	private bool devam;
	private int save = 0;

	private void Update()
	{
		LevelManager.inGameTimer += Time.deltaTime;
		//print("timer: " + LevelManager.inGameTimer);
	}

	void Start()
	{
		if (test)
		{
			adUnitId = "ca-app-pub-3940256099942544/1033173712";
			LevelManager.inGameTimer = 105;
		}

		GameObject panel = GameObject.Find("Panel");
		if (panel != null)
			panel.GetComponent<Animator>().Play("Brighten");
		text1 = GameObject.Find("Text").GetComponent<Text>();
		int begin = PlayerPrefs.GetInt("save", 0);
		if (begin > 1)
			begin--;
		//print("begin: " + begin);

		begin = (begin * 10) - 10;
		int end = begin + 9;
		string assetString = textAsset.text;

		char[] chars = assetString.ToCharArray();
		int space = 0;
		StartCoroutine(co());
		IEnumerator co()
		{
			yield return new WaitForSeconds(3f);
			devam = true;
			foreach (char c in chars)
			{
				if (c.Equals('\n'))
					space++;
				if (space >= begin - 1 && space < end)
				{
					if (begin < end)
					{
						yield return new WaitForSeconds(.05f);
						text1.text += c;
						if (char.IsLetter(c))
							audioSource.PlayOneShot(typing);
						if (!devam)
						{

							Pass();
							yield break;
						}

					}
				}

			}
			Pass();
		}
	}

	// Update is called once per frame
	public void SeriYaz()
	{
		devam = false;
		if (ready)
		{
			devamText.GetComponent<Text>().text = "Devam ediliyor...";
			GameObject panel = GameObject.Find("Panel");
			if (panel != null)
				panel.GetComponent<Animator>().Play("Blackout");
			StartCoroutine(co());
			IEnumerator co()
			{
				yield return new WaitForSeconds(5f);
				save = PlayerPrefs.GetInt("save", 0);
				save += 1;
				if (save == 2)
					save++;
				//print("save:" + save);

				SceneManager.LoadScene(save);

			}
		}
	}
	public void Pass()
	{
		devamText.SetActive(true);
		ready = true;
	}
}



/*
using GoogleMobileAds.Api;
public class AraSahne : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip typing;

    public bool test;
    public string adUnitId;
    public TextAsset textAsset;
    public GameObject devamText;
    public bool ready;

    private Text text1;
    private InterstitialAd interstitial;
    private bool devam;
    private int save = 0;

    private void Update()
    {
        LevelManager.inGameTimer += Time.deltaTime;
        //print("timer: " + LevelManager.inGameTimer);
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        GameObject.Find("Text2").GetComponent<Text>().text = "HandleAdLoaded event received";
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.ToString());
        GameObject.Find("Text2").GetComponent<Text>().text = "HandleFailedToReceiveAd event received with message: "
                            + args.LoadAdError;

        LoadAdError loadAdError = args.LoadAdError;

        // Gets the domain from which the error came.
        string domain = loadAdError.GetDomain();
        // Gets the error code. See
        // https://developers.google.com/android/reference/com/google/android/gms/ads/AdRequest
        // and https://developers.google.com/admob/ios/api/reference/Enums/GADErrorCode
        // for a list of possible codes.
        int code = loadAdError.GetCode();

        // Gets an error message.
        // For example "Account not approved yet". See
        // https://support.google.com/admob/answer/9905175 for explanations of
        // common errors.
        string message = loadAdError.GetMessage();

        // Gets the cause of the error, if available.
        AdError underlyingError = loadAdError.GetCause();

        // All of this information is available via the error's toString() method.
        Debug.Log("Load error string: " + loadAdError.ToString());

        // Get response information, which may include results of mediation requests.
        ResponseInfo responseInfo = loadAdError.GetResponseInfo();
        Debug.Log("Response info: " + responseInfo.ToString());
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
        GameObject.Find("Text2").GetComponent<Text>().text = "HandleAdOpening event received";
		GameObject.Find("Sound").GetComponent<AudioSource>().Pause();
		
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        GameObject.Find("Text2").GetComponent<Text>().text = "HandleAdClosed event received";
        LevelManager.inGameTimer = 0;
        SceneManager.LoadScene(save);
		GameObject.Find("Sound").GetComponent<AudioSource>().Play();
	}

    // Start is called before the first frame update
    void Start()
    {
        if (test)
        {
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
            LevelManager.inGameTimer = 105;
        }

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        GameObject panel = GameObject.Find("Panel");
        if (panel != null)
            panel.GetComponent<Animator>().Play("Brighten");
        text1 = GameObject.Find("Text").GetComponent<Text>();
        int begin = PlayerPrefs.GetInt("save", 0);
        if (begin > 1)
            begin--;
        print("begin: "+begin);

        if (begin != 1 && !DevTools.devmod && LevelManager.inGameTimer > 100)
        {
            //Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this.interstitial.LoadAd(request);
        }

        begin = (begin * 10) - 10;
        int end = begin + 9;
        string assetString = textAsset.text;

        char[] chars = assetString.ToCharArray();
        int space = 0;
        StartCoroutine(co());
        IEnumerator co()
        {
            yield return new WaitForSeconds(3f);
            devam = true;
            foreach (char c in chars)
            {
                if (c.Equals('\n'))
                    space++;
                if (space >= begin - 1 && space < end)
                {
                    if (begin < end)
                    {
                        yield return new WaitForSeconds(.05f);
                        text1.text += c;
                        if (char.IsLetter(c))
                            audioSource.PlayOneShot(typing);
                        if (!devam)
                        {

                            Pass();
                            yield break;
                        }

                    }
                }

            }
            Pass();
        }
    }

    // Update is called once per frame
    public void SeriYaz()
    {
        devam = false;
        if (ready)
        {
            devamText.GetComponent<Text>().text = "Devam ediliyor...";
            GameObject panel = GameObject.Find("Panel");
            if (panel != null)
                panel.GetComponent<Animator>().Play("Blackout");
            StartCoroutine(co());
            IEnumerator co()
            {
                yield return new WaitForSeconds(5f);
                save = PlayerPrefs.GetInt("save", 0);
                save += 1;
                if (save == 2)
                    save++;
                print("save:" + save);

                if (this.interstitial.IsLoaded())
                {
                    this.interstitial.Show();
                }
                else
                {
                    SceneManager.LoadScene(save);
                }

            }
        }
    }
    public void Pass()
    {
        devamText.SetActive(true);
        ready = true;
    }
}
*/