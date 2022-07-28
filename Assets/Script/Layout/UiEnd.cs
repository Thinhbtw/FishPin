using DailyQuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiEnd : MonoBehaviour
{
    public static UiEnd Instance;
    public GameObject pannel;
    [SerializeField] Button btnNext, btnResetCurLevel;
    [SerializeField] GameObject btn, winMenu, loseMenu, PlusCoin;
    [SerializeField] Text textLevel;
    public Canvas canvas;
    LevelComplete myScript;
    UIGameplay uiGameplay;
    missionCounter missionCounter;
    public bool isComplete;
    LevelProgressBar progressBar;
    public Sprite[] imgBtn;
    

    private void Awake()
    {
        Instance = (UiEnd)this;
        pannel.SetActive(false);
        canvas = GetComponent<Canvas>();
        btn.SetActive(false);
        btnResetCurLevel.gameObject.SetActive(false);
    }

    private void Start()
    {
        myScript = FindObjectOfType<LevelComplete>();
        uiGameplay = FindObjectOfType<UIGameplay>();
        progressBar = FindObjectOfType<LevelProgressBar>();
        missionCounter = FindObjectOfType<missionCounter>();
    }

    public IEnumerator WaitPanel(bool win)
    {
        Debug.Log("Selected Level " + (PlayerPrefs.GetInt("SelectedLevel") + 1));
        if (PlayerPrefs.GetInt("SelectedLevel") < 10)
        {
            textLevel.text = "Level 0" + (PlayerPrefs.GetInt("SelectedLevel") + 1).ToString();
            
        }
        else
        {
            textLevel.text = "Level " + (PlayerPrefs.GetInt("SelectedLevel") + 1).ToString();
        }
        yield return new WaitForSeconds(2);      
        if (win)
        {
            StopAllCoroutines();
            IronSource.Agent.loadInterstitial();
            winMenu.SetActive(true);
            btnNext.image.sprite = imgBtn[0];
            pannel.SetActive(true);
            SoundManager.PlaySound("success");
            yield return new WaitForSeconds(1.3f);
            /*ToponAdsController.instance.OpenInterstitialAds();*/
            if(IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial();
                FirebaseInit.Instance.WatchAds();
            }
        }
        else
        {
            StopAllCoroutines();
            IronSource.Agent.loadInterstitial();
            myScript.isComplete = true;
            myScript.check = true;
            btnNext.image.sprite = imgBtn[1];
            btnResetCurLevel.gameObject.SetActive(true);
            loseMenu.SetActive(true);
            btn.SetActive(true);
            pannel.SetActive(true);
            SoundManager.PlaySound("fail");
            yield return new WaitForSeconds(1.3f);
            /*ToponAdsController.instance.OpenInterstitialAds();*/
            if (IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial();
                FirebaseInit.Instance.WatchAds();
            }
        }
        yield break;
    }

    

    private void Update()
    {     
        if(myScript == null)
            myScript = FindObjectOfType<LevelComplete>();
        /*if(uiGameplay == null)
            uiGameplay = FindObjectOfType<UIGameplay>();*/
        if(progressBar == null)
            progressBar = FindObjectOfType<LevelProgressBar>();
        if(missionCounter == null)
        {
            missionCounter = FindObjectOfType<missionCounter>();
        }

    }

    private void OnEnable()
    {   
        isComplete = false;
        canvas.sortingOrder = 10;
        if (btnNext != null)
            btnNext.onClick.AddListener(() =>
            {
                if(myScript.isDed)
                {
                    SoundManager.PlaySound("click");
                    int i = PlayerPrefs.GetInt("SelectedLevel");
                    var lvl = Instantiate(UIManager.Instance.listLevel[i * 5], uiGameplay.levelField.transform);
                    uiGameplay.Level.Add(lvl);
                    Destroy(uiGameplay.Level[uiGameplay.Level.Count - uiGameplay.Level.Count]);
                    uiGameplay.Level.RemoveAt(uiGameplay.Level.Count - uiGameplay.Level.Count);
                    uiGameplay.levelAt = i * 5;
                    loseMenu.SetActive(false);
                    winMenu.SetActive(false);
                    PlusCoin.SetActive(false);
                    btnResetCurLevel.gameObject.SetActive(false);
                    pannel.SetActive(false);
                    myScript.isDed = false;
                }

                else if(!myScript.isDed)
                {
                    SoundManager.PlaySound("click");
                    if (UIManager.Instance.listLevel.Count % 5 == 0)
                    {
                        progressBar.progressBar.value = 0;
                    }

                    isComplete = true;
                    missionCounter.Increasing(typeID.levelFinished);
                    loseMenu.SetActive(false);
                    winMenu.SetActive(false);
                    PlusCoin.SetActive(false);
                    btnResetCurLevel.gameObject.SetActive(false);
                    pannel.SetActive(false);
                    uiGameplay.hasNextLevel = true;
                                           
                }

            });
        if (btnResetCurLevel != null)
            btnResetCurLevel.onClick.AddListener(() =>
            {
                if (myScript.isDed)
                {
                    SoundManager.PlaySound("click");
                    /*ToponAdsController.instance.OpenVideoAds();*/
                    IronSource.Agent.showRewardedVideo();
                    FirebaseInit.Instance.WatchAds();
                    var lvl = Instantiate(UIManager.Instance.listLevel[uiGameplay.levelAt], uiGameplay.levelField.transform);
                    uiGameplay.Level.Add(lvl);
                    Destroy(uiGameplay.Level[uiGameplay.Level.Count - uiGameplay.Level.Count]);
                    uiGameplay.Level.RemoveAt(uiGameplay.Level.Count - uiGameplay.Level.Count);
                    loseMenu.SetActive(false);
                    winMenu.SetActive(false);
                    PlusCoin.SetActive(false);
                    pannel.SetActive(false);
                    myScript.isDed = false;
                    btn.SetActive(false);
                }
            });
    }

    private void OnDisable()
    {
        btnResetCurLevel.onClick.RemoveAllListeners();
        btnNext.onClick.RemoveAllListeners();
    }
}
