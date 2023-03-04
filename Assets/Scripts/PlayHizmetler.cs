using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayHizmetler : MonoBehaviour
{
	public Text playText;
	public Button achievemnts;

	void Start()
	{
		PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
	}

	internal void ProcessAuthentication(SignInStatus status)
	{
		if (status == SignInStatus.Success)
		{
			// Continue with Play Games Services

			playText.text = PlayGamesPlatform.Instance.GetUserDisplayName();
			achievemnts.GetComponent<Image>().color = new Color(0.007843138f, 0.2352941f, 0f, 0.8f);

			if (ScoreManager.GetSpiderKills() > 0)
				Social.ReportProgress("CgkIwt3e_8MYEAIQBQ", 100, (bool success) => { });
			if (ScoreManager.GetSpiderKills() > 99)
				Social.ReportProgress("CgkIwt3e_8MYEAIQCQ", 100, (bool success) => { });
			if (ScoreManager.GetBossKills() > 0)
				Social.ReportProgress("CgkIwt3e_8MYEAIQCA", 100, (bool success) => { });
			if (ScoreManager.GetOmegaKills() > 0)
				Social.ReportProgress("CgkIwt3e_8MYEAIQBg", 100, (bool success) => { });
			if (ScoreManager.GetKozaKills() > 0)
				Social.ReportProgress("CgkIwt3e_8MYEAIQCg", 100, (bool success) => { });
		}
		else
		{
			// Disable your integration with Play Games Services or show a login button
			// to ask users to sign-in. Clicking it should call
			// PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
		}
	}

	public void ShowAchievemnts()
	{
		if (PlayGamesPlatform.Instance.IsAuthenticated())
		{
			PlayGamesPlatform.Instance.ShowAchievementsUI();
		}
		else
		{
			achievemnts.GetComponent<Image>().color = Color.red;
		}

	}

}
