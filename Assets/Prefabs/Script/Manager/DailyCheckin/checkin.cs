using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DailyQuestSystem;
using static dailyCheckin;

public class checkin : MonoBehaviour
{    
    [SerializeField] dailyLog Log;
    [SerializeField] Image rewardImage;
    [SerializeField] Text rewardAmount;
    [SerializeField] GameObject claimedImage;
    private void Awake()
    {
        if (GameData.getLoginDay() == Log.logDay)
        {
            PlayerPrefs.SetInt("Day" + Log.logDay, 1);
        }
    }

    public void setupLogin(dailyLog log,Sprite icon)
    {
        Log = log;
        rewardImage.sprite = icon;
        rewardAmount.text ="+" + log.amount.ToString();       
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(claimButton);
    }

    private void Update()
    {        
        if (PlayerPrefs.GetInt("Day" + Log.logDay) != 1)
        {
            GetComponent<Button>().enabled = false;            
        }
        else
        {
            GetComponent<Button>().enabled = true;            
        }
        if (Log.stat)
        {
            if (GameData.getLoginDay() == Log.logDay)
            {
                PlayerPrefs.SetInt("Day" + Log.logDay, 1);
            }
        }
        
    }

    void claimButton()
    {
        if(Log.rewardType == RewardType.Coins)
        {
            GameData.AddCoin(Log.amount);
        }
        else if(Log.rewardType == RewardType.Gems)
        {
            GameData.AddGems(Log.amount);
        }
        PlayerPrefs.SetInt("Day" + Log.logDay,0);
        Log.stat = false;
        claimedImage.SetActive(true);
    }
}
