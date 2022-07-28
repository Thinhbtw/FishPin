using DailyQuestSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text number;
    [SerializeField] Text title;
    [SerializeField] int questIndex;
    [SerializeField] Text numberProgress;
    [SerializeField] GameObject progressBar;
    [SerializeField] GameObject claimPanel;

    //private bool claimAble;
    bool isAchievement;

    [SerializeField] Quest quest;    

    

    public int QuestIndex { get => questIndex; set => questIndex = value; }
    

    public void setupQuest(Sprite questIcon,int questAmount, string questTitle, Quest questByIndex, bool IsAchievement)
    {
        icon.sprite = questIcon;
        number.text = "+" + questAmount.ToString();
        title.text = questTitle;
        quest = questByIndex;             

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(claimButton);
        isAchievement = IsAchievement;
        if (!IsAchievement)
        {
            progressBar.GetComponent<Slider>().maxValue = quest.required;
        }               
    }
        

    private void Update()
    {                
        
        if(quest.process<= quest.required)
        {
            numberProgress.text = quest.process + "/" + quest.required.ToString();
        }
        else
        {
            numberProgress.text = quest.required.ToString() + "/" + quest.required.ToString();
        }

        if (!isAchievement)
        {
            progressBar.GetComponent<Slider>().value = quest.process;
        }
                
        if (PlayerPrefs.GetInt(title.text) != INSTANCE.claimAble)
        {            
            Deactivate();
        }
        else if(PlayerPrefs.GetInt(title.text) == INSTANCE.claimAble)
        {            
            Active();
        }
    }
    

    void claimButton()
    {
        SoundManager.PlaySound("purchased");
        //check reward Type
        if (quest.Type == RewardType.Coins)
        {
            /*Debug.Log("<color=white>" + quest.Type.ToString() + " Claimed : </color>+" + quest.Amount);*/            
            GameData.AddCoin(quest.Amount);            
        }
        else if (quest.Type == RewardType.Gems)
        {
            /*Debug.Log("<color=white>" + quest.Type.ToString() + " Claimed : </color>+" + quest.Amount);*/            
            GameData.AddGems(quest.Amount);                            
        }
        PlayerPrefs.SetInt(title.text, INSTANCE.claimed);
        claimPanel.SetActive(true);
    }
    




    public void Deactivate()
    {        
        gameObject.GetComponent<Button>().enabled = false;
        //status = questStatus.claimed;
        if (PlayerPrefs.GetInt(title.text) == INSTANCE.claimed)
        {
            claimPanel.SetActive(true);
        }
    }
    public void Active()
    {
        /*claimedPanel.SetActive(false);*/
        gameObject.GetComponent<Button>().enabled = true;
    }
}
