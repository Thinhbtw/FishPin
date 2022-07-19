using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DailyQuestSystem;

public class checkin : MonoBehaviour
{
    private int index;
    private int amount;
    RewardType rewardType;
    
    private void Awake()
    {
        if (GameData.getLoginDay() == index)
        {
            PlayerPrefs.SetInt("Day" + index, 1);
        }
    }

    public void setupLogin(int LoginDay,int rewardAmount, RewardType reward)
    {
        index = LoginDay;
        amount = rewardAmount;
        rewardType = reward;

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(claimButton);

    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Day" + index) != 1)
        {
            GetComponent<Button>().enabled = false;
        }
        else
        {
            GetComponent<Button>().enabled = true;
        }
    }

    void claimButton()
    {
        if(rewardType == RewardType.Coins)
        {
            GameData.AddCoin(amount);
        }
        else if(rewardType == RewardType.Gems)
        {
            GameData.AddGems(amount);
        }
        PlayerPrefs.SetInt("Day" + index,0);
    }
}
