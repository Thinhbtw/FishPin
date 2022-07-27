using DailyQuestSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dailyCheckin : MonoBehaviour
{
    /*public static dailyCheckin Instance;*/

    [SerializeField] GameObject general;
    [SerializeField] GameObject day7;
    [SerializeField] double nextLoginCheckDelay;
    [SerializeField] double loginDelayLost;
    [SerializeField] float checkTimeDelay = 1f;
    [SerializeField] loginDataReward loginDB;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject notification;

    [Space]
    [Header("Reward Image")]
    [SerializeField] Sprite iconCoinsSprite;
    [SerializeField] Sprite iconGemsSprite;

    [SerializeField] Button openButton;
    [SerializeField] Button closeButton;

    DateTime currentDatetime;

    [Serializable] public struct dailyLog
    {
        public int logDay;
        public RewardType rewardType;
        public int amount;        
    }

    /*private void Awake()
    {
        *//*Instance = (dailyCheckin)this;*//*

        
    }*/

    private void OnEnable()
    {        

        openButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        openButton.onClick.AddListener(onOpenButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);

        /*Debug.Log(general.transform.childCount);*/

        getRealTime();

        for (int i = 0; i < general.transform.childCount; i++)
        {
            switch (loginDB.getdailyLog(i).rewardType)
            {
                case RewardType.Coins:
                    general.transform.GetChild(i).GetComponent<checkin>().setupLogin(loginDB.getdailyLog(i), iconCoinsSprite);
                    break;
                case RewardType.Gems:
                    general.transform.GetChild(i).GetComponent<checkin>().setupLogin(loginDB.getdailyLog(i), iconGemsSprite);
                    break;
            }
        }
        day7.GetComponent<checkin>().setupLogin(loginDB.getdailyLog(6), iconGemsSprite);
        StopAllCoroutines();        
        StartCoroutine(logindaily());        
    }
    void getRealTime()
    {
        //check if the game is opened for the first time then set Quest_Finish_DateTime to the current date time            
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("loginday")))
        {
            /*Debug.Log("chua co time dau game daily");*/
            PlayerPrefs.SetString("loginday", currentDatetime.ToString());
        }        

    }
    IEnumerator logindaily()
    {        
        while (true)
        {            
            DateTime date = DateTime.Parse(PlayerPrefs.GetString("loginday", currentDatetime.ToString()));
            /*Debug.Log(date.ToString());*/
            double elapsedDay = (currentDatetime - date).TotalSeconds;
            if (elapsedDay > loginDelayLost)
            {
                //reset streak
                /*Debug.Log("reset day");*/                
                /*loginDB.resetStat();*/
                //reset data stat

                GameData.resetLoginDay();
                /*Debug.Log("the day after reset: "+GameData.getLoginDay());*/          
                PlayerPrefs.SetString("loginday", currentDatetime.ToString());
            }
            else
            {                
                if (elapsedDay > nextLoginCheckDelay)
                {
                    //update streak
                    /*Debug.Log("update day"); */                   
                    GameData.increasLoginDay();
                    /*Debug.Log("this is day: " + GameData.getLoginDay());*/
                    PlayerPrefs.SetString("loginday", currentDatetime.ToString());
                }
            }
            yield return new WaitForSeconds(checkTimeDelay);
        }
    }

    private void Update()
    {
        /*Debug.Log(PlayerPrefs.GetInt("Day" + 1));*/
        if (WorldTimeAPI.Instance.IsTimeLodaed)
        {
            currentDatetime = WorldTimeAPI.Instance.GetCurrentDateTime();
        }
        else
        {
            currentDatetime = DateTime.Now;
        }
        if (GameData.getLoginDay() > 7)
        {            
            GameData.resetLoginDay();            
        }
        if (checkNofti())
        {
            notification.SetActive(true);
        }
        else
        {
            notification.SetActive(false);
        }
    }    

    void OnCloseButtonClick()
    {
        SoundManager.PlaySound("click");
        canvas.SetActive(false);
    }

    void onOpenButtonClick()
    {
        SoundManager.PlaySound("click");
        canvas.SetActive(true);
    }

    bool checkNofti()
    {
        for(int i = 0; i < 7; i++)
        {
            if (PlayerPrefs.GetInt("Day" + (i + 1)) == INSTANCE.claimAble)
            {
                return true;
            }
        }
        return false;
    }

}
