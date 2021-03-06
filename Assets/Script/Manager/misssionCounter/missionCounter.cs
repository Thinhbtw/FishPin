using DailyQuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missionCounter : MonoBehaviour
{
    #region Singleton class: misssionCounter

    public static missionCounter Instance;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion


    [Space]
    [Header("Reward Database")]
    [SerializeField] RewardDatabases rewardsDBDaily;
    [SerializeField] RewardDatabases rewardsDBWeekly;
    [SerializeField] RewardDatabases rewardsDBMonthly;

    [SerializeField] RewardDatabases Achievement;
    
    // increasing and decreasing 

    //quest

    List<Quest> questScanner(typeID idtype)
    {
        List<Quest> quests = new List<Quest>();
        for(int i = 0; i < rewardsDBDaily.rewardsCount; i++)
        {
            if(rewardsDBDaily.GetQuest(i).typeID == idtype)
            {
                quests.Add(rewardsDBDaily.GetQuest(i));
            }
        }
        for (int j = 0; j < rewardsDBWeekly.rewardsCount; j++)
        {
            if (rewardsDBWeekly.GetQuest(j).typeID == idtype)
            {
                quests.Add(rewardsDBWeekly.GetQuest(j));
            }
        }
        for (int t = 0; t < rewardsDBMonthly.rewardsCount; t++)
        {
            if (rewardsDBMonthly.GetQuest(t).typeID == idtype)
            {
                quests.Add(rewardsDBMonthly.GetQuest(t));
            }
        }
        for (int t = 0; t < Achievement.rewardsCount; t++)
        {
            if (Achievement.GetQuest(t).typeID == idtype)
            {
                quests.Add(Achievement.GetQuest(t));
            }
        }
        return quests;
    }    
    

   /* public void levelFinishedIncreasing()
    {
        List<Quest> q = questScanner(typeID.levelFinished);       
        missionCounterData.LevelFinished += 1;
        
        Debug.Log("level finished plus");
        for(int i = 0; i < q.Count; i++)
        {
            if (q[i].required <= missionCounterData.LevelFinished && PlayerPrefs.GetInt(q[i].title.ToString())==INSTANCE.unclaimAble)
            {
                PlayerPrefs.SetInt(q[i].title.ToString(), INSTANCE.claimAble);
            }
        }
        *//*questoke.GetComponent<test>().quests = q;*//*
    }*/
    public void Increasing(typeID id)
    {
        rewardsDBDaily.increaseProcess(id);
        rewardsDBWeekly.increaseProcess(id);
        rewardsDBMonthly.increaseProcess(id);
        Achievement.increaseProcess(id);
        List<Quest> q = questScanner(id);
        for (int i = 0; i < q.Count; i++)
        {
            if (q[i].required <= q[i].process && PlayerPrefs.GetInt(q[i].title.ToString()) == INSTANCE.unclaimAble)
            {                
                PlayerPrefs.SetInt(q[i].title.ToString(), INSTANCE.claimAble);
                Debug.Log(q[i].title);
            }
        }
    }    
}
