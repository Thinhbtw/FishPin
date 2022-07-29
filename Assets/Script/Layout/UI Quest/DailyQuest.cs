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
    
    [Serializable] public enum typeID
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
        public int process;
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
        [SerializeField] float checkForQuestDelay = 1f;

        

        /*[Space]
        [Header("FX")]
        [SerializeField] ParticleSystem fxCoins;
        [SerializeField] ParticleSystem fxGems;*/

        [Space]
        [Header("Button")]
        [SerializeField] Button dailyBtn;
        [SerializeField] Button weeklyBtn;
        [SerializeField] Button monthlyBtn;

        

        [SerializeField] Text clocker;
        


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
            rewardNotification.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {
            questCanvas.SetActive(false);
                        
        }

        void getRealTime()
        {
            //check if the game is opened for the first time then set Quest_Finish_DateTime to the current date time            
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(INSTANCE.dailyTime)))
            {
                /*Debug.Log("chua co time dau game daily");*/
                PlayerPrefs.SetString(INSTANCE.dailyTime, currentDatetime.ToString());
            }
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(INSTANCE.weeklyTime)))
            {
                /*Debug.Log("chua co time dau game weekly");*/
                PlayerPrefs.SetString(INSTANCE.weeklyTime, currentDatetime.ToString());
            }
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(INSTANCE.monthlyTime)))
            {
                /*Debug.Log("chua co time dau game monthly");*/
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
            /*UpdateCoinsTextUi();
            UpdateGemsTextUi();*/

            //add click events
            openButton.onClick.RemoveAllListeners();
            openButton.onClick.AddListener(OnOpenButtonClick);

            closeButtonDaily.onClick.RemoveAllListeners();
            closeButtonDaily.onClick.AddListener(OnCloseButtonClick);           


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
            SoundManager.PlaySound("click");
            onQuestBtnClick("D");
        }

        void OnWeeklyClick()
        {
            SoundManager.PlaySound("click");
            onQuestBtnClick("W");
        }

        void OnMonthlyClick()
        {
            SoundManager.PlaySound("click");
            onQuestBtnClick("M");
        }

        void onQuestBtnClick(string id)
        {
            SoundManager.PlaySound("click");
            switch (id)
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

                yield return new WaitForSeconds(checkForQuestDelay);

            }            
        }

        void resetTime(string timeName,double nextQuestDelay, RewardDatabases database)
        {                      
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
                        GameData.resetListIndexByid(INSTANCE.dailyID);
                        loadQuest(rewardsDBDaily, listDaily, questTransform, INSTANCE.dailyID);
                        break;
                    case questBtnType.weekly:
                        GameData.resetListIndexByid(INSTANCE.weeklyID);
                        loadQuest(rewardsDBWeekly, listWeekly, questTransform, INSTANCE.weeklyID);
                        break;
                    case questBtnType.monthly:
                        GameData.resetListIndexByid(INSTANCE.monthlyID);
                        loadQuest(rewardsDBMonthly, listMonthly, questTransform, INSTANCE.monthlyID);
                        break;
                }
            }
        }

        void resetQuestData(RewardDatabases data)
        {            
            data.resetProcess();
            for (int i = 0; i < data.rewardsCount; i++)
            {
                PlayerPrefs.SetInt(data.GetQuest(i).title.ToString(), INSTANCE.unclaimAble);                
            }
            /*Debug.Log("reset data at: " + data.name);*/
        }

        void resetTimeData()
        {            
            PlayerPrefs.DeleteKey(INSTANCE.dailyTime);
            PlayerPrefs.DeleteKey(INSTANCE.weeklyTime);
            PlayerPrefs.DeleteKey(INSTANCE.monthlyTime);
        }


        /*public void notificationOnOff(bool status)
        {
            rewardNotification.SetActive(status);
        }*/                
       
                       
        void loadQuest(RewardDatabases data,List<GameObject> listQuest, Transform questField, string questId)
        {
            List<string> questTitle = new List<string>();
            if (questField.childCount > 0)
            {
                for (int i = 0; i < questField.childCount; i++)
                {
                    Destroy(questField.GetChild(i).gameObject);
                }
            }

            listQuest.Clear();
            if (GameData.getListIndex(questId).Count == 0)
            {                
                int goldQuestCount = 0, gemQuestCount = 0, questList = 0 ;
                while (questList<3)
                {
                    int index = UnityEngine.Random.Range(0, data.rewardsCount);
                    if (GameData.getListIndex(questId) != null && GameData.getListIndex(questId).Contains(index))
                    {
                        continue;
                    }
                    switch (data.GetQuest(index).Type)
                    {
                        case RewardType.Coins:
                            if(goldQuestCount >= 2)
                            {
                                continue;
                            }
                            goldQuestCount++;
                            GameData.addlist(GameData.getListIndex(questId),index);                            
                            questList++;
                            break;
                        case RewardType.Gems:
                            if (gemQuestCount >= 1)
                            {
                                continue;
                            }
                            gemQuestCount++;
                            GameData.addlist(GameData.getListIndex(questId), index);
                            questList++;
                            break;
                    }                    
                }                
            }            
            List<int> indexList = GameData.getListIndex(questId);                        
            for (int i = 0; i < indexList.Count; i++)
            {                
                GameObject quest = Instantiate(questPrefab, questField);
                /*if (PlayerPrefs.GetInt(data.GetQuest(indexList[i]).title) == INSTANCE.claimed)
                {
                    quest.SetActive(false);
                }*/
                quest.GetComponent<QuestScript>().QuestIndex = indexList[i];
                listQuest.Add(quest);
                switch (data.GetQuest(indexList[i]).Type)
                {
                    case RewardType.Coins:
                        listQuest[i].GetComponent<QuestScript>().setupQuest(iconCoinsSprite, data.GetQuest(indexList[i]).Amount, data.GetQuest(indexList[i]).title, data.GetQuest(indexList[i]),false);
                        break;
                    case RewardType.Gems:
                        listQuest[i].GetComponent<QuestScript>().setupQuest(iconGemsSprite, data.GetQuest(indexList[i]).Amount, data.GetQuest(indexList[i]).title, data.GetQuest(indexList[i]),false);
                        break;
                }
            }

        }

        //check button glowing
        public void glowingCheck()
        {
            switch (btnStatus)
            {
                case questBtnType.daily:
                    dailyBtn.GetComponent<Outline>().enabled = true;
                    weeklyBtn.GetComponent<Outline>().enabled = false;
                    monthlyBtn.GetComponent<Outline>().enabled = false; 
                    break;
                case questBtnType.weekly:
                    weeklyBtn.GetComponent<Outline>().enabled = true;
                    dailyBtn.GetComponent<Outline>().enabled = false;
                    monthlyBtn.GetComponent<Outline>().enabled = false;
                    break;
                case questBtnType.monthly:
                    monthlyBtn.GetComponent<Outline>().enabled = true;
                    weeklyBtn.GetComponent<Outline>().enabled = false;
                    dailyBtn.GetComponent<Outline>().enabled = false;
                    break;
            }
        }




        //check notification------------------
        public void notificationCheck()
        {            
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
            glowingCheck();            

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
        /*public void UpdateCoinsTextUi()
        {
            //coinsText.text = CurrencyData.Coins.ToString();
        }
        public void UpdateGemsTextUi()
        {
            //gemsText.text = CurrencyData.Gems.ToString();
        }*/

        // open | close ui ------------------------------
        void OnOpenButtonClick()
        {
            SoundManager.PlaySound("click");
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
            SoundManager.PlaySound("click");
            questCanvas.SetActive(false);
        }
       

    }
}