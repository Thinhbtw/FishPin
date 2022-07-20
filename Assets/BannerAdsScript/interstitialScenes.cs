using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AnyThinkAds.Api;
using UnityEngine.UI;
 
using UnityEngine.EventSystems;
using System;
/*using MiniJSON;*/

public class interstitialScenes : MonoBehaviour {


#if UNITY_ANDROID
    static string mPlacementId_interstitial_all = "b5baca54674522"; // b62958721b65f5 b5baca54674522

    static string mPlacementId_Splash = "b62a19b9927141";

    static string showingScenario = "f5e71c49060ab3";

#elif UNITY_IOS || UNITY_IPHONE
    static string mPlacementId_interstitial_all = "b5bacad26a752a";
    static string showingScenario = "f5e549727efc49";

#endif


    // Use this for initialization
    void Start () {

        LoadSlpashInter();

        /*if (Singleton<MoneyManager>.Instance == null)
            return;
        MoneyManager moneyManager = Singleton<MoneyManager>.Instance;
        if (!moneyManager.GetAdStatus())
        {
            isShowed= true;
        }*/
        //StartCoroutine(ShowSlpashInter());
        //EventManager.ShowInterstitialAd.BoradCastEvent();
    }
    // Update is called once per frame
    void Update() {
       loadInterstitialAd();
    }
	void LoadSlpashInter()
    {
        Dictionary<string, object> jsonmap = new Dictionary<string, object>();
        jsonmap.Add(AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL, AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL_NO);

        //jsonmap.Add(AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL, AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL_YES);
        try
        {
            ATInterstitialAd.Instance.loadInterstitialAd(mPlacementId_Splash, jsonmap);

        }catch (Exception)
        {
            //Debug.Log("=========== load Splash fail =======:::"+e);
        }
        //yield return new WaitForSeconds(1.5f);
        ////Debug.Log("=========== show Splash =======");
        //ATInterstitialAd.Instance.showInterstitialAd(mPlacementId_Splash);
    }

    void ShowSlpashInter()
    {
        /*if (Singleton<MoneyManager>.Instance == null)
            return;
        MoneyManager moneyManager = Singleton<MoneyManager>.Instance;
        if (!moneyManager.GetAdStatus())
            return;*/

        bool b = ATInterstitialAd.Instance.hasInterstitialAdReady(mPlacementId_Splash);
        //Debug.Log("Developer isReady interstitial...." + b);

        string adStatus = ATInterstitialAd.Instance.checkAdStatus(mPlacementId_Splash);
        //Debug.Log("Developer checkAdStatus interstitial...." + adStatus);
        if (b)
        {
            //Debug.Log("=========== show Splash =======");
            ATInterstitialAd.Instance.showInterstitialAd(mPlacementId_Splash);

        }
    }

    static InterstitalCallback callback;
    public void loadInterstitialAd() {

        /*if (Singleton<MoneyManager>.Instance == null)
            return;
        MoneyManager moneyManager = Singleton<MoneyManager>.Instance;
        if (!moneyManager.GetAdStatus())
            return;
        if (callback == null) {
    
            callback = new InterstitalCallback();
            ATInterstitialAd.Instance.setListener(callback);
        }*/
        //Debug.Log("**********loadInterstitialAd");

        Dictionary<string,object> jsonmap = new Dictionary<string,object>();
        jsonmap.Add(AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL, AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL_NO);

        //jsonmap.Add(AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL, AnyThinkAds.Api.ATConst.USE_REWARDED_VIDEO_AS_INTERSTITIAL_YES);

        ATInterstitialAd.Instance.loadInterstitialAd(mPlacementId_interstitial_all, jsonmap);
    }

    public void isReady()
    {
    
        bool b = ATInterstitialAd.Instance.hasInterstitialAdReady(mPlacementId_interstitial_all);
        ////Debug.Log("Developer isReady interstitial...." + b);

        string adStatus = ATInterstitialAd.Instance.checkAdStatus(mPlacementId_interstitial_all);
        ////Debug.Log("Developer checkAdStatus interstitial...." + adStatus);
    }

    public void showInterstitialAd()
    {
        /*if (Singleton<MoneyManager>.Instance == null)
            return;
        MoneyManager moneyManager = Singleton<MoneyManager>.Instance;
        if (!moneyManager.GetAdStatus())
            return;*/

        ATInterstitialAd.Instance.showInterstitialAd(mPlacementId_interstitial_all);
    }

    private void OnDestroy()
    {
    


    }

    class InterstitalCallback : ATInterstitialAdListener
    {
    

        public void onInterstitialAdClick(string placementId, ATCallbackInfo callbackInfo)
        {
    
            //Debug.Log("Developer callback onInterstitialAdClick :" + placementId + "->" + Json.Serialize(callbackInfo.toDictionary()));
        }

        public void onInterstitialAdClose(string placementId, ATCallbackInfo callbackInfo)
        {
    
            //Debug.Log("Developer callback onInterstitialAdClose :" + placementId + "->" + Json.Serialize(callbackInfo.toDictionary()));
        }

        public void onInterstitialAdEndPlayingVideo(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("onInterstitialAdEndPlayingVideo");
            //Debug.Log("Developer callback onInterstitialAdEndPlayingVideo :" + placementId + "->" + Json.Serialize(callbackInfo.toDictionary()));
        }

        public void onInterstitialAdFailedToPlayVideo(string placementId, string code, string message)
        {
    
            //Debug.Log("Developer callback onInterstitialAdFailedToPlayVideo :" + placementId + "--code:" + code + "--msg:" + message);
        }

        public void onInterstitialAdLoad(string placementId)
        {
    
            //Debug.Log("Developer callback onInterstitialAdLoad :" + placementId);
        }

        public void onInterstitialAdLoadFail(string placementId, string code, string message)
        {
    

            //Debug.Log("*****Developer callback onInterstitialAdLoadFail :" + placementId + "--code:" + code + "--msg:" + message);
            //loadInterstitiaAd();
        }

        public void onInterstitialAdShow(string placementId, ATCallbackInfo callbackInfo)
        {
    
            //Debug.Log("Developer callback onInterstitialAdShow :" + placementId + "->" + Json.Serialize(callbackInfo.toDictionary()));
        }

        public void onInterstitialAdStartPlayingVideo(string placementId, ATCallbackInfo callbackInfo)
        {
    
            //Debug.Log("Developer callback onInterstitialAdStartPlayingVideo :" + placementId + "->" + Json.Serialize(callbackInfo.toDictionary()));

        }

        public void onInterstitialAdFailedToShow(string placementId)
        {
    
            //Debug.Log("Developer callback onInterstitialAdFailedToShow :" + placementId);

        }

        void ATInterstitialAdListener.onInterstitialAdLoad(string placementId)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.onInterstitialAdLoadFail(string placementId, string code, string message)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.onInterstitialAdShow(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.onInterstitialAdFailedToShow(string placementId)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.onInterstitialAdClose(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.onInterstitialAdClick(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.onInterstitialAdStartPlayingVideo(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.onInterstitialAdEndPlayingVideo(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.onInterstitialAdFailedToPlayVideo(string placementId, string code, string message)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.startLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.finishLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.failToLoadADSource(string placementId, ATCallbackInfo callbackInfo, string code, string message)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.startBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.finishBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("finishBiddingADSource");
            throw new NotImplementedException();
        }

        void ATInterstitialAdListener.failBiddingADSource(string placementId, ATCallbackInfo callbackInfo, string code, string message)
        {
            throw new NotImplementedException();
        }
    }

}