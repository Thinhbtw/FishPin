using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AnyThinkAds.Api;
using UnityEngine.UI;

using System;

public class bannerScenes : MonoBehaviour
{



#if UNITY_ANDROID
    //static string mPlacementId_banner_all = "b5baca41a2536f";b629586edb5c9c
    static string mPlacementId_banner_all = "b5baca41a2536f";

    static string showingScenario = "f600e6039e152c";

#elif UNITY_IOS || UNITY_IPHONE
	static string mPlacementId_banner_all = "b5bacaccb61c29";
    //static string mPlacementId_banner_all = "b5bacaccb61c29";
    static string showingScenario = "";
#endif

    private int screenWidth;
    // Use this for initialization
    void Start()
    {

        this.screenWidth = Screen.width;
        loadBannerAd();
        showBannerAd();

    }

    // Update is called once per frame
    void Update()
    {

        //loadBannerAd();
        //showBannerAd();

    }

    private void OnDestroy()
    {


    }

    static BannerCallback bannerCallback;

    //    public void loadBannerAd() {


    //        if(bannerCallback == null){

    //            bannerCallback = new BannerCallback();
    //            ATBannerAd.Instance.setListener(bannerCallback);
    //        }

    //        Dictionary<string, object> jsonmap = new Dictionary<string,object>();


    //        #if UNITY_ANDROID

    //        ATSize bannerSize = new ATSize(screenWidth, 50* screenWidth/350, true);
    //        //ATSize bannerSize = new ATSize(1080, 154, true);
    //        //Debug.Log("bannerSize:: " + bannerSize.width + " :: " + bannerSize.height);
    //            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraBannerAdSizeStruct, bannerSize);
    //            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveWidth, bannerSize.width);
    //            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveOrientation, ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveOrientationPortrait);
    //#elif UNITY_IOS || UNITY_IPHONE
    //            ATSize bannerSize = new ATSize(320, 50, false);
    //            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraBannerAdSizeStruct, bannerSize);
    //            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveWidth, bannerSize.width);
    //            jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveOrientation, ATBannerAdLoadingExtra.kATBannerAdLoadingExtraAdaptiveOrientationPortrait);

    //#endif 
    //        ATBannerAd.Instance.loadBannerAd(mPlacementId_banner_all, jsonmap);
    //    }
    public void loadBannerAd()
    {
        /*if (Singleton<MoneyManager>.Instance == null)
            return;
        MoneyManager moneyManager = Singleton<MoneyManager>.Instance;
        if (!moneyManager.GetAdStatus())
            return;*/
        if (bannerCallback == null)
        {
            bannerCallback = new BannerCallback();
            ATBannerAd.Instance.setListener(bannerCallback);
        }

        Dictionary<string, object> jsonmap = new Dictionary<string, object>();
        // Configure the width and height of the banner to be displayed, whether to use pixel as the unit(Only valid for iOS, Android uses pixel as the unit)
        ATSize bannerSize = new ATSize(this.screenWidth, 50 * screenWidth / 350, true);
        jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraBannerAdSizeStruct, bannerSize);

        //since v5.6.5, only for Admob inline adaptive banner
        jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraInlineAdaptiveWidth, bannerSize.width);
        jsonmap.Add(ATBannerAdLoadingExtra.kATBannerAdLoadingExtraInlineAdaptiveOrientation, ATBannerAdLoadingExtra.kATBannerAdLoadingExtraInlineAdaptiveOrientationCurrent);

        ATBannerAd.Instance.loadBannerAd(mPlacementId_banner_all, jsonmap);
    }


    public void showBannerAd()
    {
        string adStatus = ATBannerAd.Instance.checkAdStatus(mPlacementId_banner_all);

        //Debug.Log("*************Developer checkAdStatus banner...." + adStatus);

        // ATRect arpuRect = new ATRect(0,50, this.screenWidth, 300, true);
        // ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all, arpuRect);
        // ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all, ATBannerAdLoadingExtra.kATBannerAdShowingPisitionBottom);
        ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all, ATBannerAdLoadingExtra.kATBannerAdShowingPisitionBottom);

        //Debug.Log("*************Developer show banner *************");
        // Dictionary<string, string> jsonmap = new Dictionary<string, string>();
        // jsonmap.Add(AnyThinkAds.Api.ATConst.SCENARIO, showingScenario);
        // //ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all, arpuRect, jsonmap);
        //ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all, ATBannerAdLoadingExtra.kATBannerAdShowingPisitionTop, jsonmap);

    }

    public void removeBannerAd()
    {

        ATBannerAd.Instance.cleanBannerAd(mPlacementId_banner_all);
    }

    public void hideBannerAd()
    {

        ATBannerAd.Instance.hideBannerAd(mPlacementId_banner_all);
    }

    /* Use this method when you want to reshow a banner that is previously hidden(by calling hideBannerAd) */
    public void reshowBannerAd()
    {

        ATBannerAd.Instance.showBannerAd(mPlacementId_banner_all);
    }

    class BannerCallback : ATBannerAdListener
    {

        public void onAdAutoRefresh(string placementId, ATCallbackInfo callbackInfo)
        {

            ////Debug.Log("Developer callback onAdAutoRefresh :" + placementId + "->" + Json.Serialize(callbackInfo.toDictionary()));
        }

        public void onAdAutoRefreshFail(string placementId, string code, string message)
        {

            ////Debug.Log("Developer callback onAdAutoRefreshFail : "+ placementId + "--code:" + code + "--msg:" + message);
        }

        public void onAdClick(string placementId, ATCallbackInfo callbackInfo)
        {

            ////Debug.Log("Developer callback onAdClick :" + placementId + "->" + Json.Serialize(callbackInfo.toDictionary()));
        }

        public void onAdClose(string placementId)
        {

            ////Debug.Log("Developer callback onAdClose :" + placementId);
        }

        public void onAdCloseButtonTapped(string placementId, ATCallbackInfo callbackInfo)
        {

            ////Debug.Log("Developer callback onAdCloseButtonTapped :" + placementId + "->" + Json.Serialize(callbackInfo.toDictionary()));
        }

        public void onAdImpress(string placementId, ATCallbackInfo callbackInfo)
        {

            ////Debug.Log("Developer callback onAdImpress :" + placementId + "->" + Json.Serialize(callbackInfo.toDictionary()));
        }

        public void onAdLoad(string placementId)
        {

            ////Debug.Log("Developer callback onAdLoad :" + placementId);
        }

        public void onAdLoadFail(string placementId, string code, string message)
        {

            ////Debug.Log("************Developer callback onAdLoadFail : : " + placementId + "--code:" + code + "--msg:" + message);
        }

        void ATBannerAdListener.failBiddingADSource(string placementId, ATCallbackInfo callbackInfo, string code, string message)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.failToLoadADSource(string placementId, ATCallbackInfo callbackInfo, string code, string message)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.finishBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.finishLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.onAdAutoRefresh(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.onAdAutoRefreshFail(string placementId, string code, string message)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.onAdClick(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.onAdClose(string placementId)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.onAdCloseButtonTapped(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.onAdImpress(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.onAdLoad(string placementId)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.onAdLoadFail(string placementId, string code, string message)
        {
            
        }

        void ATBannerAdListener.startBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }

        void ATBannerAdListener.startLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }
    }
}