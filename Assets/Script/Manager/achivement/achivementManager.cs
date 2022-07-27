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
                    break;
                case RewardType.Gems:
                    achivements[i].GetComponent<QuestScript>().setupQuest(iconGemsSprite, data.GetQuest(i).Amount, data.GetQuest(i).title, data.GetQuest(i), true);
                    break;
            }
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
