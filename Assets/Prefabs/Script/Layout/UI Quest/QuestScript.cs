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

    //private bool claimAble;

    [SerializeField] Quest quest;
    public GameObject parentControl;

    

    public int QuestIndex { get => questIndex; set => questIndex = value; }
    

    public void setupQuest(Sprite questIcon,int questAmount, string questTitle, Quest questByIndex, GameObject parent)
    {
        icon.sprite = questIcon;
        number.text = "+" + questAmount.ToString();
        title.text = questTitle;
        quest = questByIndex;
        parentControl = parent;           

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(claimButton);
        progressBar.GetComponent<Slider>().maxValue = quest.required;
    }
        

    private void Update()
    {        
        if(PlayerPrefs.GetInt(quest.typeID.ToString())<= quest.required)
        {
            numberProgress.text = quest.process + "/" + quest.required.ToString();
        }
        else
        {
            numberProgress.text = quest.required.ToString() + "/" + quest.required.ToString();
        }
        
        progressBar.GetComponent<Slider>().value = quest.process;        
        if (PlayerPrefs.GetInt(title.text) == INSTANCE.unclaimAble || PlayerPrefs.GetInt(title.text) == INSTANCE.claimed)
        {            
            Deactivate();
        }
        else
        {            
            Active();
        }
    }
    

    void claimButton()
    {        
        //check reward Type
        if (quest.Type == RewardType.Coins)
        {
            Debug.Log("<color=white>" + quest.Type.ToString() + " Claimed : </color>+" + quest.Amount);
            /*CurrencyData.Coins += quest.Amount;*/
            GameData.AddCoin(quest.Amount);

            //TODO FX:
            parentControl.GetComponent<DailyQuest>().UpdateCoinsTextUi();
        }
        else if (quest.Type == RewardType.Gems)
        {
            Debug.Log("<color=white>" + quest.Type.ToString() + " Claimed : </color>+" + quest.Amount);
            /*CurrencyData.Gems += quest.Amount;*/
            GameData.AddGems(quest.Amount);
                
            //TODO FX:
            parentControl.GetComponent<DailyQuest>().UpdateGemsTextUi();
        }
        PlayerPrefs.SetInt(title.text, INSTANCE.claimed);     
        gameObject.SetActive(false);
    }
    




    public void Deactivate()
    {        
        gameObject.GetComponent<Button>().enabled = false;
        //status = questStatus.claimed;               
    }
    public void Active()
    {
        /*claimedPanel.SetActive(false);*/
        gameObject.GetComponent<Button>().enabled = true;
    }
}
