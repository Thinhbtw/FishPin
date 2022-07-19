using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InternetConnection : MonoBehaviour
{
    // Start is called before the first frame update
    public static InternetConnection instance;
    public bool hasInternet;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(CheckingInternetConnection());
    }

    IEnumerator CheckingInternetConnection()
    {
        yield return new WaitForSeconds(4f);
        UnityWebRequest www = new UnityWebRequest("http://google.com");
        yield return www.SendWebRequest();
        if (www.error != null)
            hasInternet = false;       
        else
            hasInternet = true;
    }
}
