using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBackground : MonoBehaviour
{

    [SerializeField]Button reset, home, pause, skip, level;
    public static UIBackground Instance;
    [SerializeField]Canvas canvas;
    public bool isPause, isSkip;
    public List<GameObject> listLevel;
    public GameObject background;
    public Text levelText;
    LevelComplete levelComplete;
    bool watchAdSkip;

    private void Awake()
    {
        watchAdSkip = true;
        Instance = (UIBackground)this;
        canvas = GetComponent<Canvas>();
        this.gameObject.SetActive(false);
        if (skip != null)
            skip.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (WatchVideoReward.instance.watchAdSkip && InternetConnection.instance.hasInternet)
                {
                    WatchVideoReward.instance.watchAdSkip = false;
                    /*ToponAdsController.instance.OpenVideoAds();*/
                    StartCoroutine(DelaySkip());
                    StartCoroutine(WatchVideoReward.instance.DelayAdSkip());
                }
            });

        

    }

    IEnumerator DelaySkip()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < UIManager.Instance.listLevel.Count / 5; i++)
        {
            int num = i;
            if (PlayerPrefs.GetInt("Progress") < num * 5 && ((num * 5) - UIGameplay.Instance.levelAt) <= 5)
            {
                UIGameplay.Instance.levelAt = num * 5;
                PlayerPrefs.SetInt("SelectedLevel", num);
                var lvl = Instantiate(UIManager.Instance.listLevel[UIGameplay.Instance.levelAt], UIGameplay.Instance.levelField.transform);
                UIGameplay.Instance.Level.Add(lvl);
                Destroy(UIGameplay.Instance.Level[UIGameplay.Instance.Level.Count - UIGameplay.Instance.Level.Count]);
                UIGameplay.Instance.Level.RemoveAt(UIGameplay.Instance.Level.Count - UIGameplay.Instance.Level.Count);
                /*                        if(num + 1 < 10)
                                        {
                                            levelText.text = "0" + (num + 1).ToString();
                                        }
                                        else
                                        {
                                            levelText.text = (num + 1).ToString();
                                        }*/

                UiEnd.Instance.pannel.SetActive(false);

                PlayerPrefs.SetInt("Progress", UIGameplay.Instance.levelAt);
                PlayerPrefs.SetInt("Progress2", UIGameplay.Instance.levelAt);
                
                PlayerPrefs.SetString("LevelSkip", PlayerPrefs.GetString("LevelSkip") + (num - 1).ToString());

                break;
            }           
        }
    }



    void ButtonInteracable(bool check)
    {
        reset.interactable = check;
        home.interactable = check;
        pause.interactable = check;
        skip.interactable = check;
        level.interactable = check;
    }

    private void Update()
    {
        if (!UIGameplay.Instance.hasNextLevel)
        {
            UIGameplay.Instance.hasNextLevel = true;
        }
        if(levelComplete == null)
            levelComplete = FindObjectOfType<LevelComplete>();
        if (levelComplete.check)
        {
            ButtonInteracable(false);
        }
        else
        {
            ButtonInteracable(true);
        }
        GameSharedUI.instance.UpdateCoinsUIText();
        GameSharedUI.instance.UpdateGemsUIText();

    }

    private void OnEnable()
    {

        listLevel = UIManager.Instance.listLevel;
        canvas.sortingOrder = 10;
        if (reset != null)
            reset.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                var lvl = Instantiate(UIManager.Instance.listLevel[UIGameplay.Instance.levelAt], UIGameplay.Instance.levelField.transform);
                UIGameplay.Instance.Level.Add(lvl);
                
                Destroy(UIGameplay.Instance.Level[UIGameplay.Instance.Level.Count - UIGameplay.Instance.Level.Count]);
                UIGameplay.Instance.Level.RemoveAt(UIGameplay.Instance.Level.Count - UIGameplay.Instance.Level.Count);
                UiEnd.Instance.pannel.SetActive(false);
            });

        if (home != null)
            home.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (UIHome.Instance == null)
                {
                    UIManager.Instance.GoBackToHome();
                    UIManager.Instance.AddToListDialog(UIManager.Instance.UIHome);
                    this.gameObject.SetActive(false);
                    
                }
                else
                {
                    UIBackground.Instance.gameObject.SetActive(false);
                    UIManager.Instance.LoadPreviousDialog();
                    UIManager.Instance.RemoveFromListDialog(UIGameplay.Instance.gameObject);
                    UIHome.Instance.gameObject.SetActive(true);
                }
            });

        if (pause != null)
            pause.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (UISettingIngame.Instance == null)
                {
                    UIManager.Instance.AddToListDialog(UIManager.Instance.UISettingIngame);
                    UIGameplay.Instance.gameObject.SetActive(true);
                    UISettingIngame.Instance.canvas.sortingOrder = 11;
                    UISettingIngame.Instance.Pause();
                }
                
            });
        
        if(level != null)
        {
            level.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (UILevel.Instance == null)
                {
                    UIManager.Instance.AddToListDialog(UIManager.Instance.UILevel);
                    UIGameplay.Instance.gameObject.SetActive(false);
                    UiEnd.Instance.gameObject.SetActive(false);
                    this.gameObject.SetActive(false);
                }
                else
                {
                    UILevel.Instance.gameObject.SetActive(true);
                    UIGameplay.Instance.gameObject.SetActive(false);
                    UiEnd.Instance.gameObject.SetActive(false);
                    this.gameObject.SetActive(false);
                }
            });
            
        }
    }

    private void OnDisable()
    {
        reset.onClick.RemoveAllListeners();
        home.onClick.RemoveAllListeners();
        level.onClick.RemoveAllListeners();
        pause.onClick.RemoveAllListeners();
    }

}
