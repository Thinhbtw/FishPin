using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using static QuestScript;

namespace DailyQuestSystem
{
    public enum RewardType
    {
        Coins,
        Gems
    }
    
    public enum typeID
    {
        levelFinished,
        enemiesKilled,
        coinCollected
    }

    [Serializable] public struct Quest
    {
        public RewardType Type;
        public int Amount;
        public string title;
        public int required;        
        public typeID typeID;

    }    

    public class DailyQuest : MonoBehaviour
    {
        [Header("Main Menu UI")]
        [SerializeField] Text coinsText;
        [SerializeField] Text gemsText;

        [Space]
        [Header("Quest Ui")]
        [SerializeField] GameObject questCanvas;
        [SerializeField] GameObject questPrefab;
        [SerializeField] Button openButton;
        [SerializeField] Button closeButtonDaily;           
        [SerializeField] GameObject rewardNotification;        

        [SerializeField] List<GameObject> listDaily;        
        [SerializeField] List<GameObject> listWeekly;
        [SerializeField] List<GameObject> listMonthly;

        [SerializeField] Transform questTransform;

        /*[SerializeField] Text QuestTypeTitle;*/

        public int questCheck;

        [Space]
        [Header("Reward Image")]
        [SerializeField] Sprite iconCoinsSprite;
        [SerializeField] Sprite iconGemsSprite;

        [Space]
        [Header("Reward Database")]
        [SerializeField] RewardDatabases rewardsDBDaily;
        [SerializeField] RewardDatabases rewardsDBWeekly;
        [SerializeField] RewardDatabases rewardsDBMonthly;

        [Space]
        [Header("Timming")]
        //wait 20sec to reload the next Quest
        [SerializeField] double nextQuestDelay_Daily ;
        [SerializeField] double nextQuestDelay_Weekly ;
        [SerializeField] double nextQuestDelay_Monthly;
        //check for reward every 5 secs
        [SerializeField] float checkForQuestDelay = 5f;

        

        /*[Space]
        [Header("FX")]
        [SerializeField] ParticleSystem fxCoins;
        [SerializeField] ParticleSystem fxGems;*/

        [Space]
        [Header("Button")]
        [SerializeField] Button dailyBtn;
        [SerializeField] Button weeklyBtn;
        [SerializeField] Button monthlyBtn;


        /*[Space]
        [Header("debuging")]*/
        /*[SerializeField] Text dailyTimer;        
        [SerializeField] Text weeklyTimer;        
        [SerializeField] Text monthlyTimer;*/

        [SerializeField] Text clocker;

        /*double a, b, c;*/


        DateTime currentDatetime;

        [SerializeField] List<Quest> sumData;

        private questBtnType btnStatus;

        public enum questBtnType
        {
            daily,
            weekly,
            monthly
        }

        private void Awake()
        {
            btnStatus = questBtnType.daily;
            Initialize();
            StopAllCoroutines();
            StartCoroutine(checkForQuestReload());
        }

        // Start is called before the first frame update
        void Start()
        {
            questCanvas.SetActive(false);
            
            /*btnStatus = questBtnType.daily;*/
            
            
            /*resetQuestData(rewardsDBDaily);
            resetQuestData(rewardsDBWeekly);
            resetQuestData(rewardsDBMonthly);
            resetTimeData();*/

           /* a = nextQuestDelay_Daily;
            b = nextQuestDelay_Weekly;
            c = nextQuestDelay_Monthly;*/
        }

        void getRealTime()
        {
            //check if the game is opened for the first time then set Quest_Finish_DateTime to the current date time            
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(INSTANCE.dailyTime)))
            {
                Debug.Log("chua co time dau game daily");
                PlayerPrefs.SetString(INSTANCE.dailyTime, currentDatetime.ToString());
            }
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(INSTANCE.weeklyTime)))
            {
                Debug.Log("chua co time dau game weekly");
                PlayerPrefs.SetString(INSTANCE.weeklyTime, currentDatetime.ToString());
            }
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(INSTANCE.monthlyTime)))
            {
                Debug.Log("chua co time dau game monthly");
                PlayerPrefs.SetString(INSTANCE.monthlyTime, currentDatetime.ToString());
            }
            
        }

        void Initialize()
        {
            getRealTime();

            //load sum data
            loadSumData(sumData, rewardsDBDaily);
            loadSumData(sumData, rewardsDBWeekly);
            loadSumData(sumData, rewardsDBMonthly);

            //update mainmenu ui
            UpdateCoinsTextUi();
            UpdateGemsTextUi();

            //add click events
            openButton.onClick.RemoveAllListeners();
            openButton.onClick.AddListener(OnOpenButtonClick);

            closeButtonDaily.onClick.RemoveAllListeners();
            closeButtonDaily.onClick.AddListener(OnCloseButtonClick);

            /*closeButtonWeekly.onClick.RemoveAllListeners();
            closeButtonWeekly.onClick.AddListener(OnCloseButtonClick);*/


            dailyBtn.onClick.RemoveAllListeners();
            dailyBtn.onClick.AddListener(OnDailyClick);

            weeklyBtn.onClick.RemoveAllListeners();
            weeklyBtn.onClick.AddListener(OnWeeklyClick);
            
            monthlyBtn.onClick.RemoveAllListeners();
            monthlyBtn.onClick.AddListener(OnMonthlyClick);

                                   
        }

        void loadSumData(List<Quest> sum, RewardDatabases dataElement)
        {
            for(int i = 0; i < dataElement.rewardsCount; i++)
            {
                sum.Add(dataElement.GetQuest(i));
            }
        }

        void OnDailyClick()
        {
            onQuestBtnClick("D");
        }

        void OnWeeklyClick()
        {
            onQuestBtnClick("W");
        }

        void OnMonthlyClick()
        {
            onQuestBtnClick("M");
        }

        void onQuestBtnClick(string id)
        {
            switch(id)
            {
                case "D":
                    btnStatus = questBtnType.daily;                    
                    loadQuest(rewardsDBDaily, listDaily, questTransform, INSTANCE.dailyID);
                    break;
                case "W":
                    btnStatus = questBtnType.weekly;                                        
                    loadQuest(rewardsDBWeekly, listWeekly, questTransform, INSTANCE.weeklyID);
                    break;
                case "M":
                    btnStatus = questBtnType.monthly;                    
                    loadQuest(rewardsDBMonthly, listMonthly, questTransform, INSTANCE.monthlyID);
                    break;
            }
        }

        
        IEnumerator checkForQuestReload()
        {            
            while (true)
            {               
                //check reset daily
                resetTime(INSTANCE.dailyTime, nextQuestDelay_Daily, rewardsDBDaily);
                //check reset weekly
                resetTime(INSTANCE.weeklyTime, nextQuestDelay_Weekly, rewardsDBWeekly);
                //check reset monthly
                resetTime(INSTANCE.monthlyTime, nextQuestDelay_Monthly, rewardsDBMonthly);

                /*a--;
                b--;
                c--;
                if (a == 0)
                    a = nextQuestDelay_Daily;                                
                if (b == 0)
                    b = nextQuestDelay_Weekly;
                if (c == 0)
                    c = nextQuestDelay_Monthly;*/

                yield return new WaitForSeconds(checkForQuestDelay);

            }            
        }

        void resetTime(string timeName,double nextQuestDelay, RewardDatabases database)
        {
            /*Debug.Log(btnStatus.ToString());
            if (WorldTimeAPI.Instance.IsTimeLodaed)
            {
                currentDatetime = WorldTimeAPI.Instance.GetCurrentDateTime();
            }
            else
            {
                currentDatetime = DateTime.Now;
            } */           
            //reset time
            DateTime questDatetime = DateTime.Parse(PlayerPrefs.GetString(timeName, currentDatetime.ToString()));
            //get total sec between this 2 dates (can use total hour)
            double elapsedSecs = (currentDatetime - questDatetime).TotalSeconds;
            if (elapsedSecs >= nextQuestDelay)
            {
                resetQuestData(database);
                PlayerPrefs.SetString(timeName, currentDatetime.ToString());                
                switch (btnStatus)
                {
                    case questBtnType.daily:
                        loadQuest(rewardsDBDaily, listDaily, questTransform, INSTANCE.dailyID);
                        break;
                    case questBtnType.weekly:
                        loadQuest(rewardsDBWeekly, listWeekly, questTransform, INSTANCE.weeklyID);
                        break;
                    case questBtnType.monthly:
                        loadQuest(rewardsDBMonthly, listMonthly, questTransform, INSTANCE.monthlyID);
                        break;
                }
            }
        }

        void resetQuestData(RewardDatabases data)
        {
           /* a = nextQuestDelay_Daily;
            b = nextQuestDelay_Weekly;
            c = nextQuestDelay_Monthly;*/
            missionCounterData.LevelFinished = 0;
            missionCounterData.EnemiesKilled = 0;
            missionCounterData.CoinCollected = 0;
            for (int i = 0; i < data.rewardsCount; i++)
            {
                PlayerPrefs.SetInt(data.GetQuest(i).title.ToString(), INSTANCE.unclaimAble);
                /*Debug.Log(PlayerPrefs.GetInt(data.GetQuest(i).title));*/
            }
        }

        void resetTimeData()
        {            
            PlayerPrefs.DeleteKey(INSTANCE.dailyTime);
            PlayerPrefs.DeleteKey(INSTANCE.weeklyTime);
            PlayerPrefs.DeleteKey(INSTANCE.monthlyTime);
        }


        public void notificationOnOff(bool status)
        {
            rewardNotification.SetActive(status);
        }                
       
                
       /* void CheckQuest()
        {

            *//*Debug.Log("notification update");*//*
            
        }*/
        void loadQuest(RewardDatabases data,List<GameObject> listQuest, Transform questField, string questId)
        {            

            if (questField.childCount > 0)
            {
                for (int i = 0; i < questField.childCount; i++)
                {
                    Destroy(questField.GetChild(i).gameObject);
                }
            }

            listQuest.Clear();
            for (int i = 0; i < data.rewardsCount; i++)
            {                
                GameObject quest = Instantiate(questPrefab, questField);
                if (PlayerPrefs.GetInt(data.GetQuest(i).title) == INSTANCE.claimed)
                {
                    quest.SetActive(false);
                }
                quest.GetComponent<QuestScript>().QuestIndex = i;
                listQuest.Add(quest);
                switch (data.GetQuest(i).Type)
                {
                    case RewardType.Coins:
                        listQuest[i].GetComponent<QuestScript>().setupQuest(iconCoinsSprite, data.GetQuest(i).Amount, data.GetQuest(i).title, data.GetQuest(i), this.gameObject);
                        break;
                    case RewardType.Gems:
                        listQuest[i].GetComponent<QuestScript>().setupQuest(iconGemsSprite, data.GetQuest(i).Amount, data.GetQuest(i).title, data.GetQuest(i), this.gameObject);
                        break;
                }
            }            
        }

        //check notification------------------
        public void notificationCheck()
        {
            questCheck = rewardsDBDaily.rewardsCount + rewardsDBWeekly.rewardsCount + rewardsDBMonthly.rewardsCount;
            for (int i = 0; i < sumData.Count; i++)
            {
                if (PlayerPrefs.GetInt(sumData[i].title) == INSTANCE.claimAble)
                {
                    rewardNotification.SetActive(true);
                    return;
                }
            }
            rewardNotification.SetActive(false);
        }

        private void Update()
        {
            notificationCheck();

            //Debug.Log(btnStatus);
            /*dailyTimer.text = a.ToString();
            weeklyTimer.text = b.ToString();
            monthlyTimer.text = c.ToString();*/

            if (WorldTimeAPI.Instance.IsTimeLodaed)
            {
                currentDatetime = WorldTimeAPI.Instance.GetCurrentDateTime();
            }
            else
            {
                currentDatetime = DateTime.Now;
            }
            clocker.text = currentDatetime.ToString();
        }

        //update main menu ui--------------------------------------------
        public void UpdateCoinsTextUi()
        {
            //coinsText.text = CurrencyData.Coins.ToString();
        }
        public void UpdateGemsTextUi()
        {
            //gemsText.text = CurrencyData.Gems.ToString();
        }

        // open | close ui ------------------------------
        void OnOpenButtonClick()
        {
            questCanvas.SetActive(true);
            switch (btnStatus)
            {
                case questBtnType.daily:
                    loadQuest(rewardsDBDaily, listDaily, questTransform, INSTANCE.dailyID);
                    break;
                case questBtnType.weekly:
                    loadQuest(rewardsDBWeekly, listWeekly, questTransform, INSTANCE.weeklyID);
                    break;
                case questBtnType.monthly:
                    loadQuest(rewardsDBMonthly, listMonthly, questTransform, INSTANCE.monthlyID);
                    break;
            }
        }
        void OnCloseButtonClick()
        {
            questCanvas.SetActive(false);
        }
       

    }
}