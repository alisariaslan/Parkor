using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using GooglePlayGames;

public class ScoreManager : MonoBehaviour
{
	public Text text;

	private static int spiderKills = 0;
	private static int spiderLingKills = 0;
	private static int omegaKills = 0;
	private static int bossKills = 0;
	private static int flyerKills = 0;
	private static int casperKills = 0;
	private static int wormKills = 0;
	private static int bulletMissShots = 0;
	private static int bulletHeadShots = 0;
	private static int bulletBodyShots = 0;
	private static int jumpKills = 0;
	private static int kozaKills = 0;

	void Start()
	{
		GetScores();
		SetStaticsText();
	}

	private void GetScores()
	{
		spiderKills = PlayerPrefs.GetInt("spiderKills", 0);
		spiderLingKills = PlayerPrefs.GetInt("spiderLingKills", 0);
		omegaKills = PlayerPrefs.GetInt("omegaKills", 0);
		bossKills = PlayerPrefs.GetInt("bossKills", 0);
		flyerKills = PlayerPrefs.GetInt("flyerKills", 0);
		casperKills = PlayerPrefs.GetInt("casperKills", 0);
		wormKills = PlayerPrefs.GetInt("wormKills", 0);
		bulletMissShots = PlayerPrefs.GetInt("bulletMissShots", 0);
		bulletHeadShots = PlayerPrefs.GetInt("bulletHeadShots", 0);
		bulletBodyShots = PlayerPrefs.GetInt("bulletBodyShots", 0);
		jumpKills = PlayerPrefs.GetInt("jumpKills", 0);
		kozaKills = PlayerPrefs.GetInt("kozaKills", 0);
	}

	private void SetStaticsText()
	{
		string staticsString = "";

		staticsString += "ÖLDÜRÜLEN ÖRÜMCEKLER: " + spiderKills + "\n\n";
		staticsString += "ÖLDÜRÜLEN ÖRÜMCEKCÝKLER: " + spiderLingKills + "\n\n";
		staticsString += "ÖLDÜRÜLEN OMEGALAR: " + omegaKills + "\n\n";
		staticsString += "ÖLDÜRÜLEN DEVASALAR: " + bossKills + "\n\n";
		staticsString += "ÖLDÜRÜLEN FLYER: " + flyerKills + "\n\n";
		staticsString += "ÖLDÜRÜLEN CASPER: " + casperKills + "\n\n";
		staticsString += "ÖLDÜRÜLEN SOLUCANLAR: " + wormKills + "\n\n";
		staticsString += "KAÇIRILAN MERMÝLER: " + bulletMissShots + "\n\n";
		staticsString += "KAFADAN VURUÞLAR: " + bulletHeadShots + "\n\n";
		staticsString += "VÜCUTTAN VURUÞLAR: " + bulletBodyShots + "\n\n";
		staticsString += "ZIPLAYARAK ÖLDÜRÜLENLER: " + jumpKills + "\n\n";
		staticsString += "PATLATILAN KOZALAR: " + kozaKills + "\n\n";
		text.text = staticsString;
	}

	public static void ResetScores()
	{
		PlayerPrefs.SetInt("spiderKills", 0);
		PlayerPrefs.SetInt("spiderLingKills", 0);
		PlayerPrefs.SetInt("omegaKills", 0);
		PlayerPrefs.SetInt("bossKills", 0);
		PlayerPrefs.SetInt("flyerKills", 0);
		PlayerPrefs.SetInt("casperKills", 0);
		PlayerPrefs.SetInt("wormKills", 0);
		PlayerPrefs.SetInt("bulletMissShots", 0);
		PlayerPrefs.SetInt("bulletHeadShots", 0);
		PlayerPrefs.SetInt("bulletBodyShots", 0);
		PlayerPrefs.SetInt("jumpKills", 0);
		PlayerPrefs.SetInt("kozaKills", 0);
	}

	public static void AddJumpKill()
	{
		jumpKills = PlayerPrefs.GetInt("jumpKills", 0);
		jumpKills++;
		PlayerPrefs.SetInt("jumpKills", jumpKills);
		CheckSocialProgress();
	}

	public static void AddBulletHeadshotKill()
	{
		bulletHeadShots = PlayerPrefs.GetInt("bulletHeadShots", 0);
		bulletHeadShots++;
		PlayerPrefs.SetInt("bulletHeadShots", bulletHeadShots);
		CheckSocialProgress();
	}
	public static void AddBulletBodyshotKill()
	{
		bulletBodyShots = PlayerPrefs.GetInt("bulletBodyShots", 0);
		bulletBodyShots++;
		PlayerPrefs.SetInt("bulletBodyShots", bulletBodyShots);
		CheckSocialProgress();
	}

	public static void AddSpiderKill()
	{
		spiderKills = PlayerPrefs.GetInt("spiderKills", 0);
		spiderKills++;
		PlayerPrefs.SetInt("spiderKills", spiderKills);
		CheckSocialProgress();
	}

	public static void AddFlyerKill()
	{
		flyerKills = PlayerPrefs.GetInt("flyerKills", 0);
		flyerKills++;
		PlayerPrefs.SetInt("flyerKills", flyerKills);
		CheckSocialProgress();
	}

	public static void AddWormKill()
	{
		wormKills = PlayerPrefs.GetInt("wormKills", 0);
		wormKills++;
		PlayerPrefs.SetInt("wormKills", wormKills);
		CheckSocialProgress();
	}

	public static void AddSpiderlingKill()
	{
		spiderLingKills = PlayerPrefs.GetInt("spiderLingKills", 0);
		spiderLingKills++;
		PlayerPrefs.SetInt("spiderLingKills", spiderLingKills);
		CheckSocialProgress();
	}

	public static void AddOmegaKill()
	{
		omegaKills = PlayerPrefs.GetInt("omegaKills", 0);
		omegaKills++;
		PlayerPrefs.SetInt("omegaKills", omegaKills);
		CheckSocialProgress();
	}

	public static void AddKozaKill()
	{
		kozaKills = PlayerPrefs.GetInt("kozaKills", 0);
		kozaKills++;
		PlayerPrefs.SetInt("kozaKills", kozaKills);
		CheckSocialProgress();
	}

	public static void AddBossKill()
	{
		bossKills = PlayerPrefs.GetInt("bossKills", 0);
		bossKills++;
		PlayerPrefs.SetInt("bossKills", bossKills);
		CheckSocialProgress();
	}

	public enum scoreType
	{
		NESIL_TUKETEN,
		SEZON_1,
		KACIS,
		ARENA
	}

	public static void ReportScoreType(scoreType type)
	{
		/*
		if (PlayGamesPlatform.Instance.IsAuthenticated())
		{
			switch (type)
			{
				case scoreType.NESIL_TUKETEN:
					Social.ReportProgress("CgkIwt3e_8MYEAIQCw", 100, (bool success) => { });
					break;
				case scoreType.SEZON_1:
					Social.ReportProgress("CgkIwt3e_8MYEAIQDA", 100, (bool success) => { });
					break;
				case scoreType.KACIS:
					Social.ReportProgress("CgkIwt3e_8MYEAIQBw", 100, (bool success) => { });
					break;
				case scoreType.ARENA:
					Social.ReportProgress("CgkIwt3e_8MYEAIQDQ", 100, (bool success) => { });
					break;

			}
		}
		*/
	}

	private static void CheckSocialProgress()
	{
		/*
		if (PlayGamesPlatform.Instance.IsAuthenticated())
		{
			if (spiderKills > 0)
			{
				Social.ReportProgress("CgkIwt3e_8MYEAIQBQ", 100, (bool success) => { });
				if (spiderKills > 99)
				{
					Social.ReportProgress("CgkIwt3e_8MYEAIQCQ", 100, (bool success) => { });
				}
			}
			if (bossKills > 0)
			{
				Social.ReportProgress("CgkIwt3e_8MYEAIQCA", 100, (bool success) => { });
			}
			if (omegaKills > 0)
				Social.ReportProgress("CgkIwt3e_8MYEAIQBg", 100, (bool success) => { });
			if (kozaKills > 0)
				Social.ReportProgress("CgkIwt3e_8MYEAIQCg", 100, (bool success) => { });

		}
		*/
	}

	public static int GetSpiderKills()
	{
		return PlayerPrefs.GetInt("spiderKills", 0);
	}

	public static int GetSpiderlingKills()
	{
		return PlayerPrefs.GetInt("spiderLingKills", 0);
	}

	public static int GetOmegaKills()
	{
		return PlayerPrefs.GetInt("omegaKills", 0);
	}

	public static int GetBossKills()
	{
		return PlayerPrefs.GetInt("bossKills", 0);
	}

	public static int GetFlyerKills()
	{
		return PlayerPrefs.GetInt("flyerKills", 0);
	}

	public static int GetCasperKills()
	{
		return PlayerPrefs.GetInt("casperKills", 0);
	}

	public static int GetWormKills()
	{
		return PlayerPrefs.GetInt("wormKills", 0);
	}

	public static int GetBulletMissShots()
	{
		return PlayerPrefs.GetInt("bulletMissShots", 0);
	}

	public static int GetBulletHeadShots()
	{
		return PlayerPrefs.GetInt("bulletHeadShots", 0);
	}

	public static int GetBulletBodyShots()
	{
		return PlayerPrefs.GetInt("bulletBodyShots", 0);
	}

	public static int GetJumpKills()
	{
		return PlayerPrefs.GetInt("jumpKills", 0);
	}

	public static int GetKozaKills()
	{
		return PlayerPrefs.GetInt("kozaKills", 0);
	}

}
