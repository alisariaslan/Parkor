using UnityEngine;
using UnityEngine.UI;

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
        var spiders = LangHelper.GetLanguageValue("killed_spiders");
        var spiderlings = LangHelper.GetLanguageValue("killed_spiderlings");
        var omegas = LangHelper.GetLanguageValue("killed_omegas");
        var giants = LangHelper.GetLanguageValue("killed_giants");
        var flyers = LangHelper.GetLanguageValue("killed_flyers");
        var ghosts = LangHelper.GetLanguageValue("killed_ghosts");
        var worms = LangHelper.GetLanguageValue("killed_worms");
        var misseds = LangHelper.GetLanguageValue("missed_shots");
        var heads = LangHelper.GetLanguageValue("head_shots");
        var bodys = LangHelper.GetLanguageValue("body_shots");
        var jumps = LangHelper.GetLanguageValue("jump_kills");
        var eggs = LangHelper.GetLanguageValue("destroyed_eggs");

        staticsString += spiders + " " + spiderKills + "\n\n";
        staticsString += spiderlings + " " + spiderLingKills + "\n\n";
        staticsString += omegas + " " + omegaKills + "\n\n";
        staticsString += giants + " " + bossKills + "\n\n";
        staticsString += flyers + " " + flyerKills + "\n\n";
        staticsString += ghosts + " " + casperKills + "\n\n";
        staticsString += worms + " " + wormKills + "\n\n";
        staticsString += misseds + " " + bulletMissShots + "\n\n";
        staticsString += heads + " " + bulletHeadShots + "\n\n";
        staticsString += bodys + " " + bulletBodyShots + "\n\n";
        staticsString += jumps + " " + jumpKills + "\n\n";
        staticsString += eggs + " " + kozaKills + "\n\n";
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
    }

    public static void AddBulletHeadshotKill()
    {
        bulletHeadShots = PlayerPrefs.GetInt("bulletHeadShots", 0);
        bulletHeadShots++;
        PlayerPrefs.SetInt("bulletHeadShots", bulletHeadShots);
    }

    public static void AddBulletBodyshotKill()
    {
        bulletBodyShots = PlayerPrefs.GetInt("bulletBodyShots", 0);
        bulletBodyShots++;
        PlayerPrefs.SetInt("bulletBodyShots", bulletBodyShots);
    }

    public static void AddSpiderKill()
    {
        spiderKills = PlayerPrefs.GetInt("spiderKills", 0);
        spiderKills++;
        PlayerPrefs.SetInt("spiderKills", spiderKills);
    }

    public static void AddFlyerKill()
    {
        flyerKills = PlayerPrefs.GetInt("flyerKills", 0);
        flyerKills++;
        PlayerPrefs.SetInt("flyerKills", flyerKills);
    }

    public static void AddWormKill()
    {
        wormKills = PlayerPrefs.GetInt("wormKills", 0);
        wormKills++;
        PlayerPrefs.SetInt("wormKills", wormKills);
    }

    public static void AddSpiderlingKill()
    {
        spiderLingKills = PlayerPrefs.GetInt("spiderLingKills", 0);
        spiderLingKills++;
        PlayerPrefs.SetInt("spiderLingKills", spiderLingKills);
    }

    public static void AddOmegaKill()
    {
        omegaKills = PlayerPrefs.GetInt("omegaKills", 0);
        omegaKills++;
        PlayerPrefs.SetInt("omegaKills", omegaKills);
    }

    public static void AddKozaKill()
    {
        kozaKills = PlayerPrefs.GetInt("kozaKills", 0);
        kozaKills++;
        PlayerPrefs.SetInt("kozaKills", kozaKills);
    }

    public static void AddBossKill()
    {
        bossKills = PlayerPrefs.GetInt("bossKills", 0);
        bossKills++;
        PlayerPrefs.SetInt("bossKills", bossKills);
    }

    public enum scoreType
    {
        NESIL_TUKETEN,
        SEZON_1,
        KACIS,
        ARENA
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
