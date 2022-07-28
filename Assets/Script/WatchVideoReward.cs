using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchVideoReward : MonoBehaviour
{
    public static WatchVideoReward instance;
    [SerializeField] Button watchAdCoin, watchAdGem;
    public bool watchAdReward, watchAdSkip;
    void Awake()
    {
        
        
        instance = this;
        watchAdReward = true;
        watchAdSkip = true;

        IronSource.Agent.loadRewardedVideo();
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        IronSource.Agent.loadInterstitial();

    }

    private void OnEnable()
    {
        if (watchAdCoin != null)
            watchAdCoin.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (watchAdReward && InternetConnection.instance.hasInternet)
                {
                    watchAdReward = false;
                    IronSource.Agent.showRewardedVideo();
                    FirebaseInit.Instance.WatchAds();
                    StartCoroutine(DelayRewardCoin());
                    StartCoroutine(DelayAdReward());
                }
            });
        if (watchAdGem != null)
            watchAdGem.onClick.AddListener(() =>
            {
                SoundManager.PlaySound("click");
                if (watchAdReward && InternetConnection.instance.hasInternet)
                {
                    watchAdReward = false;
                    IronSource.Agent.showRewardedVideo();
                    FirebaseInit.Instance.WatchAds();
                    StartCoroutine(DelayRewardGem());
                    StartCoroutine(DelayAdReward());
                }
            });
    }

    private void OnDisable()
    {
        watchAdCoin.onClick.RemoveAllListeners();
        watchAdGem.onClick.RemoveAllListeners();
    }

    IEnumerator DelayRewardCoin()
    {
        yield return new WaitForSeconds(1f);
        GameData.AddCoin(50);
    }
    public IEnumerator DelayAdReward()
    {
        yield return new WaitForSeconds(10f);
        watchAdReward = true;
    }

    public IEnumerator DelayAdSkip()
    {
        yield return new WaitForSeconds(10f);
        watchAdSkip = true;
    }


    IEnumerator DelayRewardGem()
    {
        yield return new WaitForSeconds(1f);
        GameData.AddGems(5);
    }
}
