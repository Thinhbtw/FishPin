using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DailyQuestSystem;

public class achivementManager : MonoBehaviour
{
    public static achivementManager instance;

    [Space]
    [Header("Reward Database")]
    [SerializeField] RewardDatabases AchievementListData;

    [Space]
    [Header("Reward Image")]
    [SerializeField] Sprite iconCoinsSprite;
    [SerializeField] Sprite iconGemsSprite;

    [SerializeField] GameObject achivePref;
    [SerializeField] GameObject general;
    public List<GameObject> achivements = new List<GameObject>();
    
    [SerializeField] Button btnBack;

    [Space]
    [Header("Icon List")]
    [SerializeField] Sprite iconLevelFinished;
    [SerializeField] Sprite iconEnemyKilled;
    [SerializeField] Sprite iconCoinCollected;
    [SerializeField] Sprite iconLostTime;
    [SerializeField] Sprite iconPinPulled;
    [SerializeField] Sprite iconMeltedIce;
    [SerializeField] Sprite iconLoginDay;
    [SerializeField] Sprite iconUnlockItem;

    private void Awake()
    {
        instance = (achivementManager)this;
    }

    void Start()
    {
        /*resetQuestData(AchievementListData);*/
        for (int i = 0; i < AchievementListData.rewardsCount; i++)
        {
            GameObject achieve = Instantiate(achivePref, general.transform);            
            achivements.Add(achieve);
        }
        setAchive(AchievementListData);
    }

    void resetQuestData(RewardDatabases data)
    {
        data.resetProcess();
        for (int i = 0; i < data.rewardsCount; i++)
        {
            /*PlayerPrefs.SetInt(data.GetQuest(i).title.ToString(), INSTANCE.unclaimAble);*/
            Debug.Log(PlayerPrefs.GetInt(data.GetQuest(i).title.ToString()));
        }
        /*Debug.Log("reset data at: " + data.name);*/
    }

    public void setAchive(RewardDatabases data)
    {
        for(int i = 0; i < achivements.Count; i++)
        {
            switch (data.GetQuest(i).Type)
            {
                case RewardType.Coins:
                    achivements[i].GetComponent<QuestScript>().setupQuest(iconCoinsSprite, data.GetQuest(i).Amount, data.GetQuest(i).title, data.GetQuest(i), true);
                    generateIcon(data.GetQuest(i).typeID, achivements[i]);
                    break;
                case RewardType.Gems:
                    achivements[i].GetComponent<QuestScript>().setupQuest(iconGemsSprite, data.GetQuest(i).Amount, data.GetQuest(i).title, data.GetQuest(i), true);
                    generateIcon(data.GetQuest(i).typeID, achivements[i]);
                    break;
            }
        }
    }
         
    void generateIcon(typeID type,GameObject achievement)
    {
        switch (type)
        {
            case typeID.levelFinished:
                achievement.GetComponent<setIcon>().setAchieveIcon(iconLevelFinished);
                break;
            case typeID.enemiesKilled:
                achievement.GetComponent<setIcon>().setAchieveIcon(iconEnemyKilled);
                break;
            case typeID.coinCollected:
                achievement.GetComponent<setIcon>().setAchieveIcon(iconCoinCollected);
                break;
            case typeID.lostTime:
                achievement.GetComponent<setIcon>().setAchieveIcon(iconLostTime);
                break;
            case typeID.pinPulled:
                achievement.GetComponent<setIcon>().setAchieveIcon(iconPinPulled);
                break;
            case typeID.meltedIce:
                achievement.GetComponent<setIcon>().setAchieveIcon(iconMeltedIce);
                break;
            case typeID.loginDay:
                achievement.GetComponent<setIcon>().setAchieveIcon(iconLoginDay);
                break;
            case typeID.unlockItem:
                achievement.GetComponent<setIcon>().setAchieveIcon(iconUnlockItem);
                break;
        }        
    }

    private void OnEnable()
    {
        if (btnBack != null)
            btnBack.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (UIManager.Instance.list.Count == 2)
                {
                    UIManager.Instance.LoadPreviousDialog();
                    UIHome.Instance.gameObject.SetActive(true);
                }
            });
    }

}
