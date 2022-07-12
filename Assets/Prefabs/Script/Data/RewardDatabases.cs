using DailyQuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardsDB",menuName = "Daily Rewards System/Reward Database")]

public class RewardDatabases : ScriptableObject
{
    public Quest[] quests;

    public int rewardsCount
    {
        get { return quests.Length; }
    }

    public Quest GetQuest(int index)
    {
        return quests[index];
    }    
}
