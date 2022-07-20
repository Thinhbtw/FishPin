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
        public bool stat;
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
                Debug.Log("reset day");
                GameData.resetLoginDay();
                PlayerPrefs.SetString("loginday", currentDatetime.ToString());
            }
            else
            {                
                if (elapsedDay > nextLoginCheckDelay)
                {
                    //update streak
                    Debug.Log("update day");                    
                    GameData.increasLoginDay();
                    Debug.Log("this is day: " + GameData.getLoginDay());
                    PlayerPrefs.SetString("loginday", currentDatetime.ToString());
                }
            }
            yield return new WaitForSeconds(checkTimeDelay);
        }
    }

    private void Update()
    {
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
            resetLogDB();
        }
    }
    void resetLogDB()
    {
        GameData.resetLoginDay();
        for(int i = 0; i < loginDB.dailyLogCount; i++)
        {
            loginDB.resetStat(i);
        }
    }

    void OnCloseButtonClick()
    {
        canvas.SetActive(false);
    }

    void onOpenButtonClick()
    {
        canvas.SetActive(true);
    }

}
