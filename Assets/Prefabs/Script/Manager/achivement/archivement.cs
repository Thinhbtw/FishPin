using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class archivement : MonoBehaviour
{
    
    public string archivementTitle;
    public int achiveCode;
    public int achiveIndex;

    public string achiveAmount; //optional

    [SerializeField] private bool buttonState;
    [SerializeField] GameObject claimedMask;
    public archivement(string archivementTitle, int achiveCode, string achiveAmount)
    {
        this.archivementTitle = archivementTitle;
        this.achiveCode = achiveCode;
        this.achiveAmount = achiveAmount;
    }

    //gift
    /*public static int coin;
    public static int gem;*/

    public void titleSet(string title)
    {
        GetComponentInChildren<Text>().text = title;
    }
    private void OnEnable()
    {
        buttonState = false;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //click to claim the archivement
            claiming();
        });               
    }

    public void setState(bool answer)
    {
        if (answer)
        {
            buttonState = true;
        }
        else
        {
            buttonState = false;
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("achive" + achiveIndex) == 1)
        {
            buttonState = false;
            //claimed mask here
            claimedMask.SetActive(true);
        }        
        GetComponent<Button>().enabled = buttonState;
    }

    /*3 cases which:
        1 - gold
        2 - gem
        3 - others (skin,energy,etc...)*/

    public void claiming()
    {
        
        switch (achiveCode)
        {
            case 1:
                // claiming gold                
                if (achiveAmount==null)
                {
                    Debug.Log("achive number " + (achiveIndex + 1) + " missing gif state");
                    return;
                }
                goldClaim();
                break;
            case 2:
                //claiming gem                
                if (achiveAmount == null)
                {
                    Debug.Log("achive number " + (achiveIndex + 1) + " missing gif state");
                    return;
                }
                gemClaim();
                break;
            case 3:
                // other claim
                Debug.Log("claim other");
                break;
        }
    }

    /*3 state of reward: high,medium,low*/

    public void goldClaim()
    {            
        switch (achiveAmount)
        {
            case "h":                
                Debug.Log("claim gold: " + INSTANCE.gold_high);
                break;
            case "m":                
                Debug.Log("claim gold: " + INSTANCE.gold_medium);
                break;
            case "l":                
                Debug.Log("claim gold: " + INSTANCE.gold_low);
                break;
        }
        buttonState = false;
        PlayerPrefs.SetInt("achive" + achiveIndex, 1);
    }
    public void gemClaim()
    {        
        switch (achiveAmount)
        {
            case "h":
                Debug.Log("claim gem: " + INSTANCE.gem_high);
                break;
            case "m":
                Debug.Log("claim gem: " + INSTANCE.gem_medium);
                break;
            case "l":
                Debug.Log("claim gem: " + INSTANCE.gem_low);
                break;
        }
        buttonState = false;
        PlayerPrefs.SetInt("achive" + achiveIndex, 1);
    }
}
