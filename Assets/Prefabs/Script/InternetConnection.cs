using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InternetConnection : MonoBehaviour
{
    // Start is called before the first frame update
    public static InternetConnection instance;
    public bool hasInternet, gameIsOn;
    private void Awake()
    {
        instance = this;
        gameIsOn = true;

#if UNITY_ANDROID
        string appKey = "85460dcd";
#elif UNITY_IPHONE
                string appKey = "8545d445";
#else
                string appKey = "unexpected_platform";
#endif
        IronSource.Agent.validateIntegration();

        // SDK init
        IronSource.Agent.init(appKey);
    }

    [Obsolete]
    void Start()
    {
        
        StartCoroutine(CheckInternetConnection());
    }

   

    [Obsolete]
    IEnumerator CheckInternetConnection()
    {
        while (gameIsOn)
        {
            yield return new WaitForSeconds(2f);
            const string echoServer = "http://www.example.com";

            bool result;
            using (var request = UnityWebRequest.Head(echoServer))
            {
                request.timeout = 3;
                yield return request.SendWebRequest();
                result = !request.isNetworkError && !request.isHttpError && request.responseCode == 200;
            }
            if (result)
                hasInternet = true;
            else
                hasInternet = false;
        }
    }
}
