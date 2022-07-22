using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static dailyCheckin;

[CreateAssetMenu(fileName = "LoginRewardDB", menuName = "Daily Rewards System/LoginRewardDB")]
public class loginDataReward : ScriptableObject
{
    public dailyLog[] DailyLogs;

    public int dailyLogCount
    {        
        get { return DailyLogs.Length; }
    }

    public dailyLog getdailyLog(int index)
    {
        return DailyLogs[index];
    }
    
    public void resetStat()
    {
        for(int i = 0; i < DailyLogs.Length; i++)
        {
            DailyLogs[i].stat = true;
        }
    }

}
