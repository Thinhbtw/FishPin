using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AnyThinkAds.Api;
using UnityEngine.UI; 
using System; 
using AnyThinkAds.ThirdParty.LitJson;

public class vidoeScenes : MonoBehaviour {

#if UNITY_ANDROID
 

    static string mPlacementId_rewardvideo_all = "b5b449f025ec7c";//tét b6295885b01f2a  b5b449f025ec7c
    static string showingScenario = "f5e71c46d1a28f";
#elif UNITY_IOS || UNITY_IPHONE
    static string mPlacementId_rewardvideo_all = "b6295885b01f2a";//"b5b44a0f115321";
    static string showingScenario = "f5e54970dc84e6";

#endif

    ATRewardedVideo rewardedVideo;
	 
 
	// Use this for initialization
	void Start () {
    
        loadVideo();
        //showVideo();

    }
	
	// Update is called once per frame 


    static ATCallbackListener callbackListener;
	public void loadVideo(){ 
        if (callbackListener == null){
    
            callbackListener = new ATCallbackListener();
            //Debug.Log("Developer init video....placementId:" + mPlacementId_rewardvideo_all);
            /*ATRewardedVideo.Instance.setListener(callbackListener);*/
        }

        //Debug.Log("**********loadVideo");

        ATSDKAPI.setCustomDataForPlacementID(new Dictionary<string, string> {
     {
     "placement_custom_key", "placement_custom" } }, mPlacementId_rewardvideo_all);

		Dictionary<string,string> jsonmap = new Dictionary<string,string>();
		jsonmap.Add(ATConst.USERID_KEY, "test_user_id");
		jsonmap.Add(ATConst.USER_EXTRA_DATA, "test_user_extra_data");


        ATRewardedVideo.Instance.loadVideoAd(mPlacementId_rewardvideo_all,jsonmap);
		
	}
	public void showVideo(){
         
        bool b = ATRewardedVideo.Instance.hasAdReady(mPlacementId_rewardvideo_all);
        if (!b)
        {
    
            //Debug.Log("******没有广告缓存重新 loadVideo");
            loadVideo();
        }
        //Debug.Log (" ********************Developer show video.... ********************");
        //StartCoroutine(ChangStateAds());
        Dictionary<string, string> jsonmap = new Dictionary<string, string>();
        jsonmap.Add(AnyThinkAds.Api.ATConst.SCENARIO, showingScenario);
        ATRewardedVideo.Instance.showAd(mPlacementId_rewardvideo_all, jsonmap);
        //callbackListener.onReward(mPlacementId_rewardvideo_all, null);
        //Debug.Log("done callbackListener");

    } 
    private void OnDestroy()
    {
    

    }

    public void isReady(){
    

		// //Debug.Log ("Developer isReady ?....");
        bool b = ATRewardedVideo.Instance.hasAdReady(mPlacementId_rewardvideo_all);
		////Debug.Log("Developer isReady video...." + b);

        string adStatus = ATRewardedVideo.Instance.checkAdStatus(mPlacementId_rewardvideo_all);
        ////Debug.Log("Developer checkAdStatus video...." + adStatus);
    }
     

    class ATCallbackListener : ATRewardedVideoExListener
    { 
        public void onRewardedVideoAdLoaded(string placementId)
        {
            //Debug.Log("Developer onRewardedVideoAdLoaded------");
        }

        public void onRewardedVideoAdLoadFail(string placementId, string code, string message)
        {
            //Debug.Log("Developer onRewardedVideoAdLoadFail------:code" + code + "--message:" + message);
        }

        public void onRewardedVideoAdPlayStart(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("Developer onRewardedVideoAdPlayStart------" + "->" + JsonMapper.ToJson(callbackInfo.toDictionary()));
        }

        public void onRewardedVideoAdPlayEnd(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("Developer onRewardedVideoAdPlayEnd------" + "->" + JsonMapper.ToJson(callbackInfo.toDictionary()));
            //AdController.OnAdState(AdStates.Finish);
            //Debug.Log("Developer onRewardedVideoAdPlayEnd------ Ad State done!");
        }

        public void onRewardedVideoAdPlayFail(string placementId, string code, string message)
        {
            Debug.Log("Developer onRewardedVideoAdPlayFail");
            //Debug.Log("Developer onRewardedVideoAdPlayFail------code:" + code + "---message:" + message);
        }

        public void onRewardedVideoAdPlayClosed(string placementId, bool isReward, ATCallbackInfo callbackInfo)
        {
            Debug.Log("Developer onRewardedVideoAdPlayClosed");
            //Debug.Log("Developer onRewardedVideoAdPlayClosed------isReward:" + isReward + "->" + JsonMapper.ToJson(callbackInfo.toDictionary()));
            //UIWatch.Instance.ChangStateAds();
            //Debug.Log("Developer onRewardedVideoAdPlayClosed------ Ad State done!");
        }

        public void onRewardedVideoAdPlayClicked(string placementId, ATCallbackInfo callbackInfo)
        {
            Debug.Log("Developer onRewardedVideoAdPlayClicked");
            //Debug.Log("Developer onRewardVideoAdPlayClicked------" + "->" + JsonMapper.ToJson(callbackInfo.toDictionary()));
        }

        public void onReward(string placementId, ATCallbackInfo callbackInfo)
        {
            Debug.Log("Developer onReward");
            //Debug.Log("Developer onReward------ State" + "->" + JsonMapper.ToJson(callbackInfo.toDictionary())); 
            try
            { 
                ToponAdsController.instance.ChangeState(AdStates.Finish);
                /*UIWatch.Instance.Check_Set_OnReward();*/
            }
            catch (Exception)
            {
                //Debug.Log("Developer onReward ToponAdsController==== done :: " + e);
            } 
            //Debug.Log("Developer onReward------ Ad AdController done!");

        }

        public void startLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("Developer startLoadingADSource------" + "->" + JsonMapper.ToJson(callbackInfo.toAdsourceDictionary()));

        }

        public void finishLoadingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("Developer finishLoadingADSource------" + "->" + JsonMapper.ToJson(callbackInfo.toAdsourceDictionary()));

        }

        public void failToLoadADSource(string placementId, ATCallbackInfo callbackInfo, string code, string message)
        {
            //Debug.Log("Developer failToLoadADSource------code:" + code + "---message:" + message + "->" + JsonMapper.ToJson(callbackInfo.toAdsourceDictionary()));

        }

        public void startBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("Developer startBiddingADSource------" + "->" + JsonMapper.ToJson(callbackInfo.toAdsourceDictionary()));

        }

        public void finishBiddingADSource(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("Developer finishBiddingADSource------" + "->" + JsonMapper.ToJson(callbackInfo.toAdsourceDictionary()));

        }

        public void failBiddingADSource(string placementId, ATCallbackInfo callbackInfo, string code, string message)
        {
            //Debug.Log("Developer failBiddingADSource------code:" + code + "---message:" + message + "->" + JsonMapper.ToJson(callbackInfo.toAdsourceDictionary()));

        }

        public void onRewardedVideoAdAgainPlayStart(string placementId, ATCallbackInfo callbackInfo)
        {
            Debug.Log("Developer onRewardedVideoAdAgainPlayStart");
            //Debug.Log("Developer onRewardedVideoAdAgainPlayStart------" + "->" + JsonMapper.ToJson(callbackInfo.toDictionary()));
        }

        public void onRewardedVideoAdAgainPlayEnd(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("Developer onRewardedVideoAdAgainPlayEnd------" + "->" + JsonMapper.ToJson(callbackInfo.toDictionary()));
        }

        public void onRewardedVideoAdAgainPlayFail(string placementId, string code, string message)
        {
            //Debug.Log("Developer onRewardedVideoAdAgainPlayFail------code:" + code + "---message:" + message);
        }

        public void onRewardedVideoAdAgainPlayClicked(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("Developer onRewardedVideoAdAgainPlayClicked------" + "->" + JsonMapper.ToJson(callbackInfo.toDictionary()));

        }

        public void onAgainReward(string placementId, ATCallbackInfo callbackInfo)
        {
            //Debug.Log("Developer onAgainReward------" + "->" + JsonMapper.ToJson(callbackInfo.toDictionary()));

        }








    }
}