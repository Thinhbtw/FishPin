using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchVideoReward : MonoBehaviour
{
    [SerializeField]Button watchAdCoin, watchAdGem;
    bool watchAdReward;
    void Awake()
    {
        watchAdReward = true;
        if (watchAdCoin != null)
            watchAdCoin.onClick.AddListener(() =>
            {
                if (watchAdReward)
                {
                    watchAdReward = false;
                    ToponAdsController.instance.OpenVideoAds();
                    StartCoroutine(DelayRewardCoin());
                }
            });
        if (watchAdGem != null)
            watchAdGem.onClick.AddListener(() =>
            {
                if (watchAdReward)
                {
                    watchAdReward = false;
                    ToponAdsController.instance.OpenVideoAds();
                    StartCoroutine(DelayRewardGem());
                }
            });
        
    }

    IEnumerator DelayRewardCoin()
    {
        yield return new WaitForSeconds(2f);
        GameData.AddCoin(500);
        watchAdReward = true;
    }

    IEnumerator DelayRewardGem()
    {
        yield return new WaitForSeconds(2f);
        GameData.AddGems(50);
        watchAdReward = true;
    }
}
