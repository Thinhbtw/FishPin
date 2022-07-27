using DailyQuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missionCounterData
{    
    // this is for the quest
    private static int levelFinished = 0;
    private static int enemiesKilled = 0;
    private static int coinCollected = 0;
    

    public static int LevelFinished
    {       
        get { return levelFinished; }
        set { PlayerPrefs.SetInt("levelFinished", (levelFinished = value) ); }
    }
    public static int EnemiesKilled
    {
        get { return enemiesKilled; }
        set { PlayerPrefs.SetInt("enemiesKilled", (enemiesKilled = value)); }
    }
    public static int CoinCollected
    {
        get { return coinCollected; }
        set { PlayerPrefs.SetInt("coinCollected", (coinCollected = value)); }
    }
   
    //this is for the achivement
    private static int levelFinished_noErase = 0;
    private static int pinPulled = 0;
    private static int meltedIce = 0;
    private static int losedRound = 0;
    private static int enemiesKilled_noErase = 0;
    private static int loginSteak = 0;
    private static int coinCollected_noErase = 0;
    private static int unlockedItems = 0;

    public static int LevelFinished_noErase { get => levelFinished_noErase; set => PlayerPrefs.SetInt("levelFinished_noErase", (levelFinished_noErase = value)); }
    public static int PinPulled { get => pinPulled; set => PlayerPrefs.SetInt("pinPulled", (pinPulled = value)); }
    public static int MeltedIce { get => meltedIce; set => PlayerPrefs.SetInt("meltedIce", (meltedIce = value)); }
    public static int LosedRound { get => losedRound; set => PlayerPrefs.SetInt("losedRound", (losedRound = value)); }
    public static int EnemiesKilled_noErase { get => enemiesKilled_noErase; set => PlayerPrefs.SetInt("enemiesKilled_noErase", (enemiesKilled_noErase = value)); }
    public static int LoginSteak { get => loginSteak; set => PlayerPrefs.SetInt("loginSteak", (loginSteak = value)); }
    public static int CoinCollected_noErase { get => coinCollected_noErase; set => PlayerPrefs.SetInt("coinCollected_noErase", (coinCollected_noErase = value)); }
    public static int UnlockedItems { get => unlockedItems; set => PlayerPrefs.SetInt("unlockedItems", (unlockedItems = value)); }


    


    static missionCounterData()
    {
        //quest
        levelFinished = PlayerPrefs.GetInt("levelFinished", 0);
        enemiesKilled = PlayerPrefs.GetInt("enemiesKilled", 0);
        coinCollected = PlayerPrefs.GetInt("coinCollected", 0);

        //achive
        levelFinished_noErase = PlayerPrefs.GetInt("levelFinished_noErase", 0);
        pinPulled = PlayerPrefs.GetInt("pinPulled", 0);
        meltedIce = PlayerPrefs.GetInt("meltedIce", 0);
        losedRound = PlayerPrefs.GetInt("losedRound", 0);
        enemiesKilled_noErase = PlayerPrefs.GetInt("enemiesKilled_noErase", 0);
        loginSteak = PlayerPrefs.GetInt("loginSteak", 0);
        coinCollected_noErase = PlayerPrefs.GetInt("coinCollected_noErase", 0);
        unlockedItems = PlayerPrefs.GetInt("unlockedItems", 0);

    }
}
