using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AnyThinkAds.Api;
using AnyThinkAds;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering;
using static GameData;

/// <summary>
/// 初始化TopOn
/// </summary>
public class ToponAdsController : MonoBehaviour {
    
    //[SerializeField]
    private string APPKEY = "4f7b9ac17decb9babec83aac078742c7"; /// 4f7b9ac17decb9babec83aac078742c7 129ea2d574a75e607407efec5f3ba7e3
    //[SerializeField]
    private string APPKID = "a5aa1f9deda26d"; //a5aa1f9deda26d a629586b846677 a629586b846677

    //Splash
    //public static  string mPlacementId_splash_all = "b5bea7cc9a4497";


    //private interstitialScenes intersScene;
    public vidoeScenes vidSce;
    public interstitialScenes intSce;
    public bannerScenes banSce;

    public AdStates adStates;

    /*public AdStates adStates;*/

    public static ToponAdsController instance;


    void Awake () {
    

        if (instance == null)
            instance = this;
        else
            Destroy(transform.gameObject);

        //Debug.Log("moneyManager.GetAdStatus()):::"+ moneyManager.GetAdStatus());

        initSDK();

        /*adStates = AdStates.None;*/
    }

    private void Start()
    {
        //Debug.Log("done adStates::====" + adStates);
        //vidSce.showVideo();
        banSce.showBannerAd();
    }

    public void ChangeState(AdStates states)
    {
        adStates = states;
        //Debug.Log("done adStates::====" + adStates);
    }

    public void OnpenBannerAds(){
        //Debug.Log("Gọi banner");
        ToponAdsController.instance.banSce.showBannerAd();
    }
    public void OpenInterstitialAds(){
        //Debug.Log("Gọi Interstitial");
        ToponAdsController.instance.intSce.showInterstitialAd();
    }
    public void OpenVideoAds(){
        //Debug.Log("Gọi video có tặng thưởng");
        ToponAdsController.instance.vidSce.showVideo();
    }

    

    private class InitListener : ATSDKInitListener
    {
    
        public void initSuccess()
        {
    
			//Debug.Log("Developer Develop callback SDK initSuccess");
        }
        public void initFail(string msg)
        {
    
			//Debug.Log("Developer callback SDK initFail:" + msg);

        }
    }

    private class GetLocationListener:ATGetUserLocationListener
    {
    
        public void didGetUserLocation(int location)
        {
    
            ////Debug.Log("Developer callback didGetUserLocation(): " + location);
            if(location == ATSDKAPI.kATUserLocationInEU && ATSDKAPI.getGDPRLevel() == ATSDKAPI.UNKNOWN)
            {
    
                ATSDKAPI.showGDPRAuth();
            }
        }
    }

	public void initSDK(){
    
         //Debug.Log ("*********init sdk"); 
        // //Debug.Log("Developer Version of the runtime: " + Application.unityVersion); 
        // //Debug.Log ("Developer init sdk"); 
        // //Debug.Log(" Developer Screen size : {" + Screen.width + ", " + Screen.height + "}"); 
        
        //ATSDKAPI.setChannel("unity3d_test_channel");
        //ATSDKAPI.setSubChannel("unity3d_test_Subchannel");
        //ATSDKAPI.initCustomMap(new Dictionary<string, string> { { "channel", "huawei" } });
        //ATSDKAPI.setCustomDataForPlacementID(new Dictionary<string, string> { { "unity3d_data_pl", "test_data_pl" } },"b5b728e7a08cd4");
        /**/
        ATSDKAPI.setLogDebug(true);
        //ATSDK.setNetworkLogDebug(true);//The application must close the log before going online

        //ATSDK.integrationChecking(applicationContext);
        //Debug.Log("Developer DataConsent: " + ATSDKAPI.getGDPRLevel());
        //Debug.Log("Developer isEUTrafic: " + ATSDKAPI.isEUTraffic());
       



        ATSDKAPI.getUserLocation(new GetLocationListener());


        #if UNITY_ANDROID
                ATSDKAPI.initSDK(APPKID, APPKEY, new InitListener());
        #endif

    }
}