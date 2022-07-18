
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
}

[System.Serializable]public class QuestListData
{
    public List<int> dailyList = new List<int>();
    public List<int> weeklyList = new List<int>();
    public List<int> monthlyList = new List<int>();
}


public static class GameData
{
    static PlayerData playerData = new PlayerData();
    static ThemeShopData themeShopData = new ThemeShopData();
    static QuestListData QuestListData = new QuestListData();

    static Theme selectedTheme;

    static GameData()
    {
        
        LoadPlayerData();
        LoadThemeShopData();
        LoadQuestListIndexData();
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
        UnityEngine.Debug.Log("<color=green>[PlayerData] Loaded </color>");
            
    }

    static void SavePlayerData()
    {
        BinarySerializer.Save(playerData,"player-data.txt");
        UnityEngine.Debug.Log("<color=magenta>[PlayerData] Saved </color>");

    }
      
    static void SaveQuestListIndexData()
    {
        BinarySerializer.Save(QuestListData, "Quest_List_Index_Data.txt");
        UnityEngine.Debug.Log("<color=magenta>[QuestListData] Saved </color>");

    }

    static void LoadQuestListIndexData()
    {
        QuestListData = BinarySerializer.Load<QuestListData>("Quest_List_Index_Data.txt");
        UnityEngine.Debug.Log("<color=green>[QuestListData] Loaded </color>");
    }

    //Theme Data Method

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
        UnityEngine.Debug.Log("<color=green>[ThemeShop] Loaded </color>");

    }

    static void SaveThemeShopData()
    {
        BinarySerializer.Save(themeShopData, "theme-shop-data.txt");
        UnityEngine.Debug.Log("<color=magenta>[ThemeShop] Saved </color>");

    }
}
