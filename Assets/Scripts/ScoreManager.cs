using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text text;

    void Start()
    {
        var spiderKills = PlayerPrefs.GetInt("spiderKills", 0);
        var spiderlingKills = PlayerPrefs.GetInt("spiderLingKills", 0);
        var omegaKills = PlayerPrefs.GetInt("omegaKills", 0);
        var bossKills = PlayerPrefs.GetInt("bossKills", 0);
        var flyerKills = PlayerPrefs.GetInt("flyerKills", 0);
        var casperKills = PlayerPrefs.GetInt("casperKills", 0);
        var wormKills = PlayerPrefs.GetInt("wormKills", 0);
        var bulletMissShots = PlayerPrefs.GetInt("bulletMissShots", 0);
        var bulletHeadShots = PlayerPrefs.GetInt("bulletHeadShots", 0);
        var bulletBodyShots = PlayerPrefs.GetInt("bulletBodyShots", 0);
        var jumpKills = PlayerPrefs.GetInt("jumpKills", 0);
        var kozaKills = PlayerPrefs.GetInt("kozaKills", 0);
        var maxScore = PlayerPrefs.GetInt("maxScore", 0);

        string staticsString = "";

        var spiders = LangHelper.GetLanguageValue("spiderKills");
        var spiderlings = LangHelper.GetLanguageValue("spiderLingKills");
        var omegas = LangHelper.GetLanguageValue("omegaKills");
        var boss = LangHelper.GetLanguageValue("bossKills");
        var flyers = LangHelper.GetLanguageValue("flyerKills");
        var caspers = LangHelper.GetLanguageValue("casperKills");
        var worms = LangHelper.GetLanguageValue("wormKills");
        var missShots = LangHelper.GetLanguageValue("bulletMissShots");
        var headShots = LangHelper.GetLanguageValue("bulletHeadShots");
        var bodyShots = LangHelper.GetLanguageValue("bulletBodyShots");
        var jumps = LangHelper.GetLanguageValue("jumpKills");
        var eggs = LangHelper.GetLanguageValue("kozaKills");
        var score = LangHelper.GetLanguageValue("maxScore");

        staticsString += spiders + " " + spiderKills + "\n\n";
        staticsString += spiderlings + " " + spiderlingKills + "\n\n";
        staticsString += omegas + " " + omegaKills + "\n\n";
        staticsString += boss + " " + bossKills + "\n\n";
        staticsString += flyers + " " + flyerKills + "\n\n";
        staticsString += caspers + " " + casperKills + "\n\n";
        staticsString += worms + " " + wormKills + "\n\n";
        staticsString += missShots + " " + bulletMissShots + "\n\n";
        staticsString += headShots + " " + bulletHeadShots + "\n\n";
        staticsString += bodyShots + " " + bulletBodyShots + "\n\n";
        staticsString += jumps + " " + jumpKills + "\n\n";
        staticsString += eggs + " " + kozaKills + "\n\n";
        staticsString += score + " " + maxScore + "\n\n";

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
        PlayerPrefs.SetInt("maxScore", 0);
    }

    public static int AddSpiderKill()
    {
        var spiderKills = PlayerPrefs.GetInt("spiderKills", 0);
        spiderKills++;
        PlayerPrefs.SetInt("spiderKills", spiderKills);
        return spiderKills;
    }

    public static int AddSpiderlingKill()
    {
        var spiderlingKills = PlayerPrefs.GetInt("spiderLingKills", 0);
        spiderlingKills++;
        PlayerPrefs.SetInt("spiderLingKills", spiderlingKills);
        return spiderlingKills;
    }

    public static int AddOmegaKill()
    {
        var omegaKills = PlayerPrefs.GetInt("omegaKills", 0);
        omegaKills++;
        PlayerPrefs.SetInt("omegaKills", omegaKills);
        return omegaKills;
    }

    public static int AddBossKill()
    {
        var bossKills = PlayerPrefs.GetInt("bossKills", 0);
        bossKills++;
        PlayerPrefs.SetInt("bossKills", bossKills);
        return bossKills;
    }

    public static int AddFlyerKill()
    {
        var flyerKills = PlayerPrefs.GetInt("flyerKills", 0);
        flyerKills++;
        PlayerPrefs.SetInt("flyerKills", flyerKills);
        return flyerKills;
    }

    public static int AddCasperKill()
    {
        var casperKills = PlayerPrefs.GetInt("casperKills", 0);
        casperKills++;
        PlayerPrefs.SetInt("casperKills", casperKills);
        return casperKills;
    }

    public static int AddWormKill()
    {
        var wormKills = PlayerPrefs.GetInt("wormKills", 0);
        wormKills++;
        PlayerPrefs.SetInt("wormKills", wormKills);
        return wormKills;
    }

    public static int AddBulletMissedshot()
    {
        var bulletMissShots = PlayerPrefs.GetInt("bulletMissShots", 0);
        bulletMissShots++;
        PlayerPrefs.SetInt("bulletMissShots", bulletMissShots);
        return bulletMissShots;
    }

    public static int AddBulletHeadshot()
    {
        var bulletHeadShots = PlayerPrefs.GetInt("bulletHeadShots", 0);
        bulletHeadShots++;
        PlayerPrefs.SetInt("bulletHeadShots", bulletHeadShots);
        return bulletHeadShots;
    }

    public static int AddBulletBodyshot()
    {
        var bulletBodyShots = PlayerPrefs.GetInt("bulletBodyShots", 0);
        bulletBodyShots++;
        PlayerPrefs.SetInt("bulletBodyShots", bulletBodyShots);
        return bulletBodyShots;
    }

    public static int AddJumpKill()
    {
        var jumpKills = PlayerPrefs.GetInt("jumpKills", 0);
        jumpKills++;
        PlayerPrefs.SetInt("jumpKills", jumpKills);
        return jumpKills;
    }

    public static int AddKozaKill()
    {
        var kozaKills = PlayerPrefs.GetInt("kozaKills", 0);
        kozaKills++;
        PlayerPrefs.SetInt("kozaKills", kozaKills);
        return kozaKills;
    }

    public static int SetMaxScore(int newScore)
    {
        var maxScore = PlayerPrefs.GetInt("maxScore", 0);
        if (newScore > maxScore)
            PlayerPrefs.SetInt("maxScore", newScore);
        maxScore = PlayerPrefs.GetInt("maxScore", 0);
        return maxScore;
    }

}
