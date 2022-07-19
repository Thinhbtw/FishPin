using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dailyCheckin : MonoBehaviour
{
    public static dailyCheckin Instance;

    [SerializeField] GameObject general;
    [SerializeField] GameObject day7;

    DateTime currentDatetime;

    private void Awake()
    {
        Instance = (dailyCheckin)this;
    }
    private void OnEnable()
    {
        if (WorldTimeAPI.Instance.IsTimeLodaed)
        {
            currentDatetime = WorldTimeAPI.Instance.GetCurrentDateTime();
        }
        else
        {
            currentDatetime = DateTime.Now;
        }
        for (int i = 0; i < general.transform.childCount; i++)
        {

            /*general.transform.GetChild(i).GetComponent<>*/
        }
    }

    void checkLoginListener()
    {
        if (!string.IsNullOrEmpty(PlayerPrefs.GetInt(currentDatetime.ToString()).ToString()))
        {

        }
    }
}
