
using UnityEngine;
using System.Runtime.Serialization;
using System.Collections.Generic;

//Player Data Holder

[System.Serializable]public class ThemeShopData
{
    public List<int> purchaseThemeIndex = new List<int>();  
}

[System.Serializable] public class PlayerData
{
    public int coin = 0;
    public int gems = 0;
    public int selectedThemeIndex = 0;
    public int loginday = 0;
}

[System.Serializable]public class QuestListData
{
    public List<int> dailyList = new List<int>();
    public List<int> weeklyList = new List<int>();
    public List<int> monthlyList = new List<int>();
}

public enum AdStates
{
    Idle,
    Playing,
    Finish,
    Fail
}


public static class GameData
{
    static PlayerData playerData = new PlayerData();
    static ThemeShopData themeShopData = new ThemeShopData();
    static QuestListData QuestListData = new QuestListData();

    static Theme selectedTheme;    

    static AdStates adStates = AdStates.Idle;
    static bool isAdsOn = false, IsAddMoney;
    static GameData()
    {
        
        LoadPlayerData();
        LoadThemeShopData();
        LoadQuestListIndexData();
        IsAddMoney = false;
    }

        

    //Player Data Method
    
    public static int GetCoin()
    {
        return playerData.coin;
    }

    public static void AddCoin(int amount)
    {
        playerData.coin += amount;
        SavePlayerData();
    }

    public static bool CanSpendCoin(int amount)
    {
        return (playerData.coin >= amount);
    }

    public static void SpendCoin(int amount)
    {
        playerData.coin -= amount;
        SavePlayerData();
    }

    public static int GetGems()
    {
        return playerData.gems;
    }

    public static void AddGems(int amount)
    {
        playerData.gems += amount;
        SavePlayerData();
    }   

    // list index daily
    public static void addlist(List<int> listIndex,int index)
    {
        listIndex.Add(index);
        SaveQuestListIndexData();
    }

    public static List<int> getListIndex(string id)
    {
        List<int> list = new List<int>();
        switch (id)
        {
            case "D":
                list = QuestListData.dailyList;
                break;            
            case "W":
                list = QuestListData.weeklyList;
                break;
            case "M":
                list = QuestListData.monthlyList;
                break;

        }
        return list;
    }

    public static void resetListIndexByid(string id)
    {
        switch (id)
        {
            case "D":
                QuestListData.dailyList.Clear();
                break;
            case "W":
                QuestListData.weeklyList.Clear();
                break;
            case "M":
                QuestListData.monthlyList.Clear();
                break;
        }
        SaveQuestListIndexData();
    }

    // get data login
    public static int getLoginDay()
    {
        return playerData.loginday;
    }

    public static void increasLoginDay()
    {
        playerData.loginday++;
        PlayerPrefs.SetInt("Day"+playerData.loginday, 1);
        SavePlayerData();
    }

    public static void resetLoginDay()
    {
        for(int i = 1; i <= playerData.loginday; i++)
        {
            PlayerPrefs.SetInt("Day" + i, 0);
        }
        playerData.loginday = 1;
        SavePlayerData();
    }

    public static bool CanSpendGems(int amount)
    {
        return (playerData.gems >= amount);
    }

    public static void SpendGems(int amount)
    {
        playerData.gems -= amount;
        SavePlayerData();
    }

    static void LoadPlayerData()
    {
        playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
            
    }

    static void SavePlayerData()
    {
        BinarySerializer.Save(playerData,"player-data.txt");

    }
      
    static void SaveQuestListIndexData()
    {
        BinarySerializer.Save(QuestListData, "Quest_List_Index_Data.txt");

    }

    static void LoadQuestListIndexData()
    {
        QuestListData = BinarySerializer.Load<QuestListData>("Quest_List_Index_Data.txt");
    }
    

    //Theme Data Method
    #region Theme
    public static void AddPurchasedThemes(int themeIndex)
    {
        themeShopData.purchaseThemeIndex.Add(themeIndex);
        SaveThemeShopData();
    }

    public static List<int> GetAllPurchasedTheme()
    {
        return themeShopData.purchaseThemeIndex;
    }
    public static int GetPurchasedTheme(int index)
    {
        return themeShopData.purchaseThemeIndex[index];
    }

    public static int GetSelectedThemeIndex()
    {
        return playerData.selectedThemeIndex;
    }

    public static Theme GetSelectedTheme()
    {
        return selectedTheme;
    }

    public static void SetSelectedTheme(Theme theme, int index)
    {
        selectedTheme = theme;
        playerData.selectedThemeIndex = index;
        SavePlayerData();
    }

    static void LoadThemeShopData()
    {
        themeShopData = BinarySerializer.Load<ThemeShopData>("theme-shop-data.txt");

    }

    static void SaveThemeShopData()
    {
        BinarySerializer.Save(themeShopData, "theme-shop-data.txt");

    }

    #endregion

    #region Ads
    public static void ChangedAdStates(AdStates ads, bool isAddMoney)
    {
        adStates = ads;
        IsAddMoney = isAddMoney;
    }
    public static AdStates GetAdStatus()
    {
        return adStates;
    }
    public static void showVideosAds()
    {
        ToponAdsController.instance.OpenInterstitialAds();
    }
    public static void showRewardAds(bool isAddCoin)
    {
        isAdsOn = true;
        ToponAdsController.instance.OpenVideoAds();
    }

    #endregion
}
