using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchVideoReward : MonoBehaviour
{
    public static WatchVideoReward instance;
    [SerializeField]Button watchAdCoin, watchAdGem;
    public bool watchAdReward, watchAdSkip;
    void Awake()
    {
        instance = this;
        watchAdReward = true;
        watchAdSkip = true;
        if (watchAdCoin != null)
            watchAdCoin.onClick.AddListener(() =>
            {
                if (watchAdReward && InternetConnection.instance.hasInternet)
                {
                    watchAdReward = false;
                    ToponAdsController.instance.OpenVideoAds();
                    StartCoroutine(DelayRewardCoin());
                    StartCoroutine(DelayAdReward());
                }
            });
        if (watchAdGem != null)
            watchAdGem.onClick.AddListener(() =>
            {
                if (watchAdReward && InternetConnection.instance.hasInternet)
                {
                    watchAdReward = false;
                    ToponAdsController.instance.OpenVideoAds();
                    StartCoroutine(DelayRewardGem());
                    StartCoroutine(DelayAdReward());
                }
            });
        
    }

    IEnumerator DelayRewardCoin()
    {
        yield return new WaitForSeconds(1f);
        GameData.AddCoin(500);
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
        GameData.AddGems(50);
    }
}
