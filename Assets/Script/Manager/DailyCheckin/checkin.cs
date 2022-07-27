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
    

    public int currentday;
    public void setupLogin(dailyLog log,Sprite icon)
    {
        Log = log;
        rewardImage.sprite = icon;
        rewardAmount.text ="+" + log.amount.ToString();       
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(claimButton);

        if (GameData.getLoginDay() == Log.logDay)
        {            
            PlayerPrefs.SetInt("Day" + Log.logDay, INSTANCE.claimAble);
        }
    }

    private void Update()
    {
        /*Debug.Log(PlayerPrefs.GetInt("Day" + 1));*/
        currentday = GameData.getLoginDay();
        if (PlayerPrefs.GetInt("Day" + Log.logDay) == INSTANCE.claimed)
        {
            GetComponent<Button>().enabled = false;
            claimedImage.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Day" + Log.logDay) == INSTANCE.unclaimAble)
        {
            GetComponent<Button>().enabled = false;
            claimedImage.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Day" + Log.logDay) == INSTANCE.claimAble)
        {
            GetComponent<Button>().enabled = true;
            claimedImage.SetActive(false);
        }            
        if (PlayerPrefs.GetInt("Day"+Log.logDay)==INSTANCE.unclaimAble)
        {
            if (GameData.getLoginDay() == Log.logDay)
            {                
                PlayerPrefs.SetInt("Day" + Log.logDay, INSTANCE.claimAble);
            }
        }
        if (GameData.getLoginDay() == Log.logDay)
        {
            //out line on
            GetComponentInChildren<Outline>().enabled = true;
        }
        else
        {
            //out line off
            GetComponentInChildren<Outline>().enabled = false;
        }
    }

    void claimButton()
    {
        SoundManager.PlaySound("purchased");
        if(Log.rewardType == RewardType.Coins)
        {
            GameData.AddCoin(Log.amount);
        }
        else if(Log.rewardType == RewardType.Gems)
        {
            GameData.AddGems(Log.amount);
        }
        PlayerPrefs.SetInt("Day" + Log.logDay,INSTANCE.claimed);        
        claimedImage.SetActive(true);        
    }
    
}
